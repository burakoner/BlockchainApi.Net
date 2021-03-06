using System;
using BlockchainApi.Net.Wallet;
using BlockchainApi.Net.Explorers;
using BlockchainApi.Net.PushTx;
using BlockchainApi.Net.Core;
using BlockchainApi.Net.Exceptions;

namespace BlockchainApi.Net.Core
{
    public class ApiHelper : IDisposable
    {
        private readonly IHttpClient baseHttpClient;
        private readonly IHttpClient serviceHttpClient;
        public readonly BlockExplorer blockExplorer;
        public readonly WalletCreator walletCreator;
        public readonly TransactionPusher transactionBroadcaster;
        public readonly ExchangeRateExplorer exchangeRateExplorer;
        public readonly StatisticsExplorer statisticsExplorer;

        public ApiHelper(string apiCode = null, IHttpClient baseHttpClient = null, string serviceUrl = null, IHttpClient serviceHttpClient = null)
        {
            if (baseHttpClient == null)
            {
                baseHttpClient = new ApiHttpClient(apiCode);
            }
            else
            {
                this.baseHttpClient = baseHttpClient;
                if (apiCode != null)
                {
                    baseHttpClient.ApiCode = apiCode;
                }
            }

            if (serviceHttpClient == null && serviceUrl != null)
            {
                serviceHttpClient = new ApiHttpClient(apiCode, serviceUrl);
            }
            else if (serviceHttpClient != null)
            {
                this.serviceHttpClient = serviceHttpClient;
                if (apiCode != null)
                {
                    serviceHttpClient.ApiCode = apiCode;
                }
            }
            else
            {
                serviceHttpClient = null;
            }

            this.blockExplorer = new BlockExplorer(baseHttpClient);
            this.transactionBroadcaster = new TransactionPusher(baseHttpClient);
            this.exchangeRateExplorer = new ExchangeRateExplorer(baseHttpClient);
            this.statisticsExplorer = new StatisticsExplorer(new ApiHttpClient("https://api.blockchain.info"));

            if (serviceHttpClient != null)
            {
                walletCreator = new WalletCreator(serviceHttpClient);
            }
            else
            {
                walletCreator = null;
            }

        }

        /// <summary>
        /// Creates an instance of 'WalletHelper' based on the identifier allowing the use
        /// of that wallet
        /// </summary>
        /// <param name="identifier">Wallet identifier (GUID)</param>
        /// <param name="password">Decryption password</param>
        /// <param name="secondPassword">Second password</param>
        public Wallet.WalletManager InitializeWallet(string identifier, string password, string secondPassword = null)
        {
            if (serviceHttpClient == null)
            {
                throw new ClientApiException("In order to create wallets, you must provide a valid service_url to BlockchainApiHelper");
            }
            return new Wallet.WalletManager(serviceHttpClient, identifier, password, secondPassword);
        }

        public WalletCreator CreateWalletCreator()
        {
            return new WalletCreator(serviceHttpClient);
        }

        public void Dispose()
        {
            if (baseHttpClient != null)
            {
                baseHttpClient.Dispose();
            }
            if (serviceHttpClient != null)
            {
                serviceHttpClient.Dispose();
            }
        }
    }

}