using BlockchainApi.Net.Core;
using BlockchainApi.Net.Explorers;
using System;

namespace BlockchainApi.Net.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var exp = new BlockExplorer();
            var latestBlock = exp.GetLatestBlockAsync().Result;

            var apiCode = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
            var httpClient = new ApiHttpClient(apiCode, "http://127.0.0.1:3000");
            using (ApiHelper apiHelper = new ApiHelper(apiCode: apiCode, serviceHttpClient: httpClient, serviceUrl: "http://127.0.0.1:3000/"))
            {
                var walletCreator = new Wallet.WalletCreator(httpClient);
                var walletCreated = walletCreator.CreateAsync("password").Result;

                var wallet = apiHelper.InitializeWallet(walletCreated.Identifier, "password");
                var addressList = wallet.ListAddressesAsync().Result;
                var address = wallet.GetAddressAsync(walletCreated.Address).Result;
                var balance = wallet.GetBalanceAsync().Result;

                for (var i = 0; i < 3; i++)
                {
                    var addr = wallet.NewAddressAsync("Label " + i).Result;
                }
                var addressListFinal = wallet.ListAddressesAsync().Result;
            }

            Console.ReadLine();
        }
    }
}
