using System;

namespace BlockchainApi.Net.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var exp = new BlockExplorer.BlockExplorer();
            var latestBlock = exp.GetLatestBlockAsync().Result;


            Console.ReadLine();
        }
    }
}
