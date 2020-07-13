# `Info.Blockchain.API.Models` namespace

The `Models` namespace contains all the objects used for requests sent to and responses received from the API.

## Bitcoin Value Object

A `BitcoinValue` object is created with a decimal value representing amount in bitcoins:

```csharp
var btcVal = new BitcoinValue(decimal btc)
```

A `BitcoinValue` object can also be created from Satoshis, Bits and MilliBits. `BitcoinValue` objects can also be used with the `+` and `-` arithmetic operators, and compared using the `Equals` method.

## Methods

```csharp
static BitcoinValue FromSatoshis(long satoshis)
```
Get a `BitcoinValue` object from amount in Satoshis.

```csharp
static BitcoinValue FromBits(decimal bits)
```
Get a `BitcoinValue` object from amount in bits.

```csharp
static BitcoinValue FromMilliBits(decimal milliBits)
```
Get a `BitcoinValue` object from amount in milliBits.