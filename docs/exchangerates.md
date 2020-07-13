# `Info.Blockchain.API.ExchangeRates` namespace

The `ExchangeRates` namespace contains the `ExchangeRateExplorer` class that reflects the functionality documented at https://blockchain.info/api/exchange_rates_api. It allows users to get price tickers for most major currencies and directly convert fiat amounts to BTC.

## Methods

### GetTicker

```csharp
Task<Dictionary<string, Currency>> GetTickerAsync()
```

   Get a dictionary of currencies and their Bitcoin exchange rate from https://blockchain.info/ticker.

### ToBtc

```csharp
Task<double> ToBtcAsync(string currency, double value)
```

   Convert a value in a provided currency to Bitcoin.

Parameters:

* `string currency` - the currency code you are converting from (USD if invalid code)
* `double value` - the value you are converting

### FromBtc

```csharp
Task<double> FromBtcAsync(BitcoinValue btc, string currency = "")
```

   Convert a bitcoin value to a provided currency (default USD)

Parameters:

* `BitcoinValue btc` - a BitcoinValue object ([docs][bitcoinvalue.md])

Optional parameters:

* `string currency` - the currency code you want to convert to (default USD)

## Response Object Properties

A description of the objects returned by the methods in this class

### Currency Object

* `Buy`: *double*
* `Sell`: *double*
* `Last`: *double*
* `Price15M`: *double*
* `Symbol`: *string*


## Example usage:

```csharp
using System;
using Info.Blockchain.API.Client;
using Info.Blockchain.API.ExchangeRates;

namespace TestApp
{
    class Program
    {
        private static ExchangeRateExplorer explorer;

        static void Main(string[] args)
        {
            // create a new Exchange Rate Explorer
            explorer = new ExchangeRateExplorer();

            try
            {
                var ticker = explorer.GetTickerAsync().Result;
                foreach (var key in ticker.Keys)
                {
                    Console.WriteLine("The last price of BTC in {0} is {1}", key, ticker[key].Last);
                }

                double btcAmount = explorer.ToBtcAsync("USD", 1500).Result;
                Console.WriteLine("1500 USD equals {0} BTC", btcAmount);
            }
            catch (ClientApiException e)
            {
                Console.WriteLine("Blockchain exception: " + e.Message);
            }
        }
    }
}
```
