# `Info.Blockchain.API.Statistics` namespace

The `Statistics` namespace contains the `StatisticsExplorer` class that reflects the functionality documented at https://blockchain.info/api/charts_api. It makes various network statistics available, such as the total number of blocks in existence, next difficulty retarget block, total BTC mined in the past 24 hours etc.

## Methods

### GetStats

```csharp
Task<StatisticsResponse> GetStatsAsync()
```

Get the network statistics.

### GetChart

```csharp
Task<ChartResponse> GetChartAsync(string chartType, string timespan = null, string rollingAverage = null)
```

Get a specified chart and a list of its values.

Parameters:
* `string chartType` - the name of the chart you want to get, e.g. "transactions-per-second"

Optional Parameters:
* `string timespan` - interval for which to fetch data, can be set to "all" or a period of time, e.g. "2years" or "14d"
* `string rollingAverage` - duration over which data should be averaged, e.g. "8hours"

### GetPools

```csharp
Task<Dictionary<string, int>> GetPoolsAsync(int timespan = null)
```

Get a dictionary of mining pools and the total blocks they mined in the last 4 days.

Optional Parameters:
* `int timespan` - number of days to get data for (default 4, maximum 10)

## Response Object Properties

### Statistics Response Object

* `TradeVolumeBtc`: *double*
* `TradeVolumeUsd`: *double*
* `MinersRevenueBtc`: *double*
* `MinersRevenueUsd`: *double*
* `MarketPriceUsd`: *double*
* `EstimatedTransactionVolumeUsd`: *double*
* `TotalFeesBtc`: *BitcoinValue*
* `TotalBtcSent`: *BitcoinValue*
* `EstimatedBtcSent`; *BitcoinValue*
* `BtcMined`: *BitcoinValue*
* `Difficulty`: *double*
* `MinutesBetweenBlocks`: *double*
* `NumberOfTransactions`: *long*
* `HashRate`: *double*
* `Timestamp`: *long*
* `MinedBlocks`: *long*
* `BlocksSize`: *long*
* `TotalBtc`: *BitcoinValue*
* `TotalBlocks`: *long*
* `NextRetarget`: *long*

### Chart Response Object

* `ChartName`: *string*
* `Unit`: *string*
* `Timespan`: *string*
* `Description`: *string*
* `Values`: *IEnumerable(ChartValue)*

### Chart Value Object

* `X`: *double*
* `Y`: *double*

## Example usage:

```csharp
using System;
using Info.Blockchain.API.Client;
using Info.Blockchain.API.Statistics;

namespace TestApp
{
    class Program
    {
        private static StatisticsExplorer explorer;

        static void Main(string[] args)
        {
            try
            {
                explorer = new StatisticsExplorer();
                var stats = explorer.GetStatsAsync().Result;

                Console.WriteLine("The current difficulty is {0}. The next retarget will happen in {1} hours",
                    stats.Difficulty,
                    (int)((stats.NextRetarget - stats.TotalBlocks) * stats.MinutesBetweenBlocks / 60));

                // get the chart containing last week's average block size data
                var averageBlockSizeChart = explorer.GetChartAsync("avg-block-size", "1w");
            }
            catch (ClientApiException e)
            {
                Console.WriteLine("Blockchain exception: " + e.Message);
            }
        }
    }
}
```