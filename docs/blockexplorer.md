# `Info.Blockchain.API.BlockExplorer` namespace

The `BlockExplorer` namespace contains the `BlockExplorer` class that reflects the functionality documented at  https://blockchain.info/api/blockchain_api. It can be used to query the block chain, fetch block, transaction and address data, get unspent outputs for an address etc.

## Methods

### GetTransaction

```csharp
Task<Transaction> GetTransactionByIndexAsync(long index)
```

   Get a single transaction based on a transaction index. (Deprecated)

```csharp
Task<Transaction> GetTransactionByHashAsync(string hash)
```

   Get a single transaction based on a transaction hash.

### GetBlock

```csharp
Task<Block> GetBlockByIndexAsync(long index)
```

   Get a single block based on a block index. (Deprecated)

```csharp
Task<Block> GetBlockByHashAsync(string hash)
```

   Get a single block based on a block hash.

### GetAddress

The implementations of these methods are currently interchangeable, but this is liable to change in the future. All methods accept the following optional parameters:

* `int limit` - the number of transactions to limit the response to (max. 50 (100 for xPub), default 50 (100 for xPub))
* `int offset` - skip the first n transactions (default 0)
* `FilterType filter` - type of filter to use for the query (default FilterType.RemoveUnspendable)

```csharp
Task<Address> GetBase58AddressAsync(string address)
```

   Get data for a single Base58Check address and its transactions.

```csharp
Task<Address> GetHash160AddressAsync(string address)
```

   Get data for a single Hash160 address and its transactions.

```csharp
Task<Xpub> GetXpub(string xpub)
```

    Get xPub summary for a given xPub, with its overall balance and transactions.

### GetMultiAddress

```csharp
Task<MultiAddress> GetMultiAddressAsync(IEnumerable<string> addressList)
```

   Get data for multiple Base58Check and / or xPub addresses.

Optional parameters:

* `int limit` - the number of transactions to limit the response to (max. 100, default 100)
* `int offset` - skip the first n transactions (default 0)
* `FilterType filter` - type of filter to use for the query (default FilterType.RemoveUnspendable)

### GetBlocksAtHeight

```csharp
Task<ReadOnlyCollection<Block>> GetBlocksAtHeightAsync(long height)
```

   Get a list of blocks at a specified height.

### GetUnspentOutputs

```csharp
Task<ReadOnlyCollection<UnspentOutput>> GetUnspentOutputsAsync(IEnumerable<string> addressList)
```

   Get unspent outputs for one or more Base58Check and / or xPub addresses.

Optional parameters:

* `int limit` - the number of transactions to limit the response to (max. 1000, default 250)
* `int confirmations` - minimum number of confirmations to show (default 0)

### GetLatestBlock

```csharp
Task<LatestBlock> GetLatestBlockAsync()
```

   Get the latest block on the main chain.

### GetUnconfirmedTransactions

```csharp
Task<ReadOnlyCollection<Transaction>> GetUnconfirmedTransactionAsync()
```

   Get a list of currently unconfirmed transactions.

### GetBlocks

```csharp
Task<ReadOnlyCollection<SimpleBlock>> GetBlocksByDateTimeAsync(DateTime dateTime)
```

   Get a list of blocks mined on a specific day using a `DateTime` object.

```csharp
Task<ReadOnlyCollection<SimpleBlock>> GetBlocksByTimestampAsync(long unixMillis)
```

   Get a list of blocks mined on a specific day using a unix timestamp.

```csharp
Task<ReadOnlyCollection<SimpleBlock>> GetBlocksByPoolNameAsync(string poolName = "")
```

   Get a list of blocks (max. 101) mined by a specific mining pool. `poolName` is an optional parameter - if it is not passed, the method returns a list of all blocks mined since midnight today.

## Response Object Properties

A description of the objects returned by the methods in this class.

### Transaction Object

* `DoubleSpend`: *bool*
* `BlockHeight`: *long*
* `Time`: *DateTime*
* `RelayedBy`: *string*
* `Hash`: *string*
* `Index`: *long*
* `Version`: *int*
* `Size`: *long*
* `Inputs`: *ReadOnlyCollection(Input)*
* `Outputs`: *ReadOnlyCollection(Output)*

### Block Object

* `Version`: *int*
* `PreviousBlockHash`: *string*
* `MerkleRoot`: *string*
* `Bits`: *long*
* `Fees`: *BitcoinValue*
* `Nonce`: *long*
* `Size`: *long*
* `Index`: *long*
* `ReceivedTime`: *DateTime*
* `RelayedBy`: *string*
* `Transactions`: *ReadOnlyCollection(Transaction)*

### Address Object

* `Hash160`: *string*
* `Base58Check`: *string*
* `TotalReceived`: *BitcoinValue*
* `TotalSent`: *BitcoinValue*
* `FinalBalance`: *BitcoinValue*
* `TransactionCount`: *long*
* `Transactions`: *IEnumerable(Transaction)*

### MultiAddress Object

* `Addresses`: *IEnumerable(Address)*
* `Transactions`: *IEnumerable(Transaction)*

### Unspent Output Object

* `N`: *int*
* `TransactionHash`: *string*
* `TransactionIndex`: *long*
* `Script`: *string*
* `Value`: *BitcoinValue*
* `Confirmations`: *long*

### Simple Block Object

* `Height`: *long*
* `Hash`: *string*
* `Time`: *DateTime*
* `MainChain`: *bool*

### Latest Block Object

* `Index`: *long*
* `TransactionIndexes`: *ReadOnlyCollection(long)*

### Filter Type Enum

* `All`: *4*
* `ConfirmedOnly`: *5*
* `RemoveUnspendable`: *6*

### Bitcoin Value Object

([docs](bitcoinvalue.md))

## Example usage:

```csharp
using System;
using System.Linq;
using Info.Blockchain.API.Client;
using Info.Blockchain.API.BlockExplorer;

namespace TestApp
{
    class Program
    {
        private static BlockExplorer explorer;

        static void Main(string[] args)
        {
            try
            {
                // instantiate a block explorer with no api code
                // pointing to https://blockchain.info
                explorer = new BlockExplorer();

                // get a transaction by hash and list the value of all its inputs
                var tx = explorer.GetTransactionByHashAsync("df67414652722d38b43dcbcac6927c97626a65bd4e76a2e2787e22948a7c5c47").Result;
                foreach (Input i in tx.Inputs)
                {
                    Console.WriteLine(i.PreviousOutput.Value);
                }

                // get a block by hash and read the number of transactions in the block
                var block = explorer.GetBlockByHashAsync("0000000000000000050fe18c9b961fc7c275f02630309226b15625276c714bf1").Result;
                int numberOfTxsInBlock = block.Transactions.Count;

                // get an address by Hash160...
                var address = explorer.GetHash160AddressAsync("1e15be27e4763513af36364674eebdba5a047323").Result;

                // or by Base58Check address...
                address = explorer.GetBase58AddressAsync("13k5KUK2vswXRdjgjxgCorGoY2EFGMFTnu").Result;

                // and print its final balance
                var finalBalance = address.FinalBalance;

                // get a list of currently unconfirmed transactions...
                var unconfirmedTxs = explorer.GetUnconfirmedTransactionsAsync().Result;

                // and get the relay IP address for each
                var relayIPs = unconfirmedTxs.Select(v => v.RelayedBy);

                // calculate the balanace of an address by fetching a list of all its unspent outputs
                var outs = explorer.GetUnspentOutputsAsync("13k5KUK2vswXRdjgjxgCorGoY2EFGMFTnu").Result;
                var totalUnspentValue = outs.Sum(v => v.Value.GetBtc());

                // get the latest block on the main chain and read its height
                var latestBlock = explorer.GetLatestBlockAsync().Result;
                long latestBlockHeight = latestBlock.Height;

                // use the previous block height to get a list of blocks at that height
                // and detect a potential chain fork
                var blocksAtHeight = explorer.GetBlocksAtHeightAsync(latestBlockHeight).Result;
                if (blocksAtHeight.Count > 1)
                    Console.WriteLine("The chain has forked!");
                else
                    Console.WriteLine("The chain is still in one piece :)");

                // get a list of all blocks that were mined yesterday using DateTime
                var yesterdaysBlocks = explorer.GetBlocksByDateTimeAsync(DateTime.Now.AddDays(-1)).Result;

                // get a list of all blocks mined on a particular day using a unix timestamp...
                var someDaysBlocks = explorer.GetBlocksByTimestampAsync(1490000210396).Result;

                // you can also get a particular mining pool's recent blocks
                // note: this approach is case-sensitive
                var minePoolBlocks = explorer.GetBlocksByPoolNameAsync("BTC.com").Result;
            }
            catch (ClientApiException e)
            {
                Console.WriteLine("Blockchain exception: " + e.Message);
            }
        }
    }
}

```