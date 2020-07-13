# `Info.Blockchain.API.PushTx` namespace

The `PushTx` namespace contains the `TransactionPusher` class that reflects the functionality provided at https://blockchain.info/pushtx. It allows users to broadcast hex encoded transactions to the bitcoin network.

## Methods

### PushTransaction

```csharp
Task PushTransactionAsync(string transactionString)
```

   Broadcast a hex encoded transaction to the network

## Example usage:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Info.Blockchain.API.Client;
using Info.Blockchain.API.PushTx;

namespace TestApp
{
    class Program
    {
        private static TransactionPusher txPusher;

        static void Main(string[] args)
        {
            try
            {
                txPusher = new TransactionPusher();
				txPusher.PushTransactionAsync( "0100000001fd468e431cf5797b108e4d22724e1e055b3ecec59af4ef17b063afd36d3c5cf6010000008c4930460221009918eee8be186035be8ca573b7a4ef7bc672c59430785e5390cc375329a2099702210085b86387e3e15d68c847a1bdf786ed0fdbc87ab3b7c224f3c5490ac19ff4e756014104fe2cfcf0733e559cbf28d7b1489a673c0d7d6de8470d7ff3b272e7221afb051b777b5f879dd6a8908f459f950650319f0e83a5cf1d7c1dfadf6458f09a84ba80ffffffff01185d2033000000001976a9144be9a6a5f6fb75765145d9c54f1a4929e407d2ec88ac00000000").Wait();
            }
            catch (ClientApiException e)
            {
                Console.WriteLine("Blockchain exception: " + e.Message);
            }
        }
    }
}

```
