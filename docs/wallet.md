# `Info.Blockchain.API.Wallet` namespace


The `Wallet` namespace contains the `Wallet` class that reflects the functionality documented at https://github.com/blockchain/service-my-wallet-v3. It allows users to directly interact with their existing Blockchain.info wallet, send funds, manage addresses etc.

Additionally, this namespace contains the `WalletCreator` class that reflects the functionality documented at https://blockchain.info/api/create_wallet. It allows users to create new wallets as long as their API code has the 'generate wallet' permission.

The `Wallet` namespace was designed to interact with a `service-my-wallet-v3` instance running locally.

# WalletCreator Class

Using the default constructor will create a Wallet Creator with a BlockchainHttpClient pointing to localhost (where you should already be running an instance of service-my-wallet-v3):

```csharp
var walletCreator = new WalletCreator();
```

However, an API code is required to create a new wallet, so you should set the API code either using the BlockchainApiHelper (see example in bottom of page) or as follows:

```csharp
var httpClient = new BlockchainHttpClient("api-code", "http://127.0.0.1:3000");
var walletCreator = new WalletCreator(httpClient);
```

## Methods

### Create

```csharp
Task<CreateWalletResponse> CreateAsync(string password, string privateKey = null, string label = null, string email = null)
```

Create a new wallet.

Parameters:
* `string password` - a password containing a minimum of 10 characters

Optional parameters:
* `string privateKey` - a pre-generated private key, which will be generated for you if not provided
* `string label` - a label for the first address in the wallet
* `email` - email address to associate with the new wallet

# Wallet Class

An instance of a wallet needs to be initialized before it can be used:

```csharp
var wallet = new Wallet(BlockchainHttpClient client, string identifier, string password, string secondPassowrd = null);
```

Or, if using BlockchainApiHelper (see usage examples below):

```csharp
Wallet wallet = apiHelper.InitializeWallet(identifier, password, secondPassword);
```

* `secondPassword` is an optional parameter.

## Methods


###  Send

```csharp
Task<PaymentResponse> SendAsync(string toAddress, BitcoinValue amount, string fromAddress = null, BitcoinValue fee = null)
```

Send bitcoin from the initialized wallet to a single address.

Parameters:
* `string toAddress` - recipient bitcoin address
* `BitcoinValue amount` - BitcoinValue object representing amount to send

Optional parameters:
* `string fromAddress` - specific address to send from
* `BitcoinValue fee` - BitcoinValue object representing transaction fee

### SendMany

```csharp
Task<PaymentResponse> SendManyAsync(Dictionary<string, BitcoinValue> recipients, string fromAddress = null, BitcoinValue fee = null)
```

Send bitcoin from the initialized wallet to multiple addresses.

Parameters:
* `Dictionary<string, BitcoinValue> recipients` - Dictionary of address:amount pairs

Optional parameters:
* `string fromAddress` - specific address to send from
* `BitcoinValue fee` - BitcoinValue object representing transaction fee

### GetBalance

```csharp
Task<BitcoinValue> GetBalanceAsync()
```

Get the wallet balance, including unconfirmed transactions and possibly double spends.

### ListAddresses

```csharp
Task<List<WalletAddress>> ListAddressesAsync()
```

List all active addresses in the wallet.

### GetAddress

```csharp
Task<WalletAddress> GetAddressAsync(string address)
```

Retrieves the specified address from the wallet.

### NewAddress

```csharp
Task<WalletAddress> NewAddressAsync(string label = null)
```

Generate a new address and add it to the wallet.

Optional parameter:
* `string label` - label to attach to the new address

### ArchiveAddress

```csharp
Task<string> ArchiveAddressAsync(string address)
```

Archive an address.

### UnarchiveAddress

```csharp
Task<string> UmarchiveAddressAsync(string address)
```

Unarchive an address.

## Response Object Properties

### Create Wallet Response Object

* `Identifier`: *string*
* `Address`: *string*
* `Label`: *string*

### Payment Response Object

* `Message`: *string*
* `TxHash`: *string*
* `Notice`: *string*


### Wallet Address Object

* `Balance`: *BitcoinValue*
* `AddressStr`: *string*
* `Label`: *string*
* `TotalReceived`: *BitcoinValue*

### Bitcoin Value Object

([docs](bitcoinvalue.md))

## Example usage:

```csharp
using System;
using System.Collections.Generic;
using Info.Blockchain.API.Client;
using Info.Blockchain.API.Wallet;
using Info.Blockchain.API.Models;

namespace TestApp
{
    class Program
    {
        private static Wallet wallet;
        private static WalletCreator walletCreator;

        static void Main(string[] args)
        {
            using (BlockchainApiHelper apiHelper = new BlockchainApiHelper(apiCode: "1770d5d9-bcea-4d28-ad21-6cbd5be018a8", serviceUrl: "http://127.0.0.1:3000/"))
            {
                try
                {
                    // create a new wallet
                    walletCreator = apiHelper.CreateWalletCreator();

                    var newWallet = walletCreator.Create("someComplicated123Password", label: "some-optional-label").Result;
                    Console.WriteLine("The new wallet identifier is: {0}", newWallet.Identifier);

                    // create an instance of an existing wallet
                    wallet = apiHelper.InitializeWallet("wallet-identifier", "someComplicated123Password");

                    // create a new address and attach a label to it
                    WalletAddress newAddr = wallet.NewAddress("test label 123").Result;
                    Console.WriteLine("The new address is {0}", newAddr.AddressStr);

                    // get an address from your wallet (in this example we use the address just created)
                    WalletAddress addr = wallet.GetAddressAsync(newAddr.AddressStr).Result;
                    Console.WriteLine("The balance is {0}", addr.Balance);

                    // list the wallet balance
                    BitcoinValue totalBalance = wallet.GetBalanceAsync().Result;
                    Console.WriteLine("The total wallet balance is {0} BTC", totalBalance.GetBtc());

                    // send 0.2 bitcoins with a custom fee of 100,000 satoshis
                    // this will throw an error if insufficient wallet funds
                    BitcoinValue fee = BitcoinValue.FromSatoshis(10000);
                    BitcoinValue amount = BitcoinValue.FromSatoshis(20000000);
                    PaymentResponse payment = wallet.SendAsync("1dice6YgEVBf88erBFra9BHf6ZMoyvG88", amount, fee: fee).Result;
                    Console.WriteLine("The payment TX hash is {0}", payment.TxHash);

                    // list all addresses and their balances
                    List<WalletAddress> addresses = wallet.ListAddressesAsync(0).Result;
                    foreach (var a in addresses)
                    {
                        Console.WriteLine("The address {0} has a balance of {1}", a.AddressStr, a.Balance);
                    }

                    // archive an old address
                    wallet.ArchiveAddress(addr.AddressStr).Wait();
                }
                catch (ClientApiException e)
                {
                    Console.WriteLine("Blockchain exception: " + e.Message);
                }
            }
        }
    }
}
```
