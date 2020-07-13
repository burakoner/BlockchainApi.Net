## `Info.Blockchain.API.Client` namespace

The `Client` namespace contains the `BlockchainHttpClient` class that interacts with Blockchain's API using HTTP calls.

A client pointing to `https://blockchain.info` is created by default in the constructor of all classes in this API client (except for the wallet classes, that connect to a `service-my-wallet-v3` running locally). You can create a Blockchain HTTP Client pointing to a different URL, or using an API code to prevent hitting request limits, as follows:

```csharp
var customClient = new BlockchainHttpClient("api_code", "https://some.other.url");
```

Both parameters are optional, i.e. a client can be created using only one of the two parameters.