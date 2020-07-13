# `Info.Blockchain.API.Receive` namespace

The `Receive` namespace contains the `Receive` class that reflects the functionality documented at https://blockchain.info/api/api_receive. It allows users to generate a new address for their extended public key (xPub), and receive payment notifications to that address at a callback URL of their choosing.

You must obtain an Receive Payments V2 API key and an xPub in order to use this API. Please refer to the documentation linked above to learn more.

## Methods

### Generate Address

```csharp
Task<ReceivePaymentResponse> GenerateAddressAsync(string xpub, string callback, string key)
```

Generate a new unique address for receiving payments.

Parameters:

* `string xpub` - a BIP32 extended public key for generating bitcoin addresses
* `string callback` - the url you want to receive payment notifications to
* `string key` - the API key

### Check Address Gap

```csharp
Task<XpubGap> CheckAddressGapAsync(string xpub, string key)
```

Check the index gap between last address paid to and the last address generated.

Paramteters:

* `string xpub`
* `string key`

### Get Callback Logs

```csharp
Task<IEnumerable<CallbackLog>> GetCallbackLogsAsync(string callback, string key)
```

See logs related to callback attempts to your callback url.

Parameters:

* `string callback`
* `string key`

## Response Object Properties

### Receive Payment Response Object

* `Address`: *string* (The new unique address for receiving payments)
* `Index`: *int* (The number of addresses generated from your xPub so far)
* `Callback`: *string*

### Xpub Gap Object

* `Gap`: *int*

### Callback Log Object

* `CallbackUrl`: *string*
* `CallDateString`: *string* (The date and time of the call in UTC)
* `RawResponse`: *string*
* `ResponseCode`: *int*

