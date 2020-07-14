using BlockchainApi.Net.Core;
using System;
using System.Threading.Tasks;

namespace BlockchainApi.Net.Core
{
	public interface IHttpClient : IDisposable
	{
		string ApiCode { get; set; }
		Task<T> GetAsync<T>(string route, QueryString queryString = null, Func<string, T> customDeserialization = null);
		Task<TResponse> PostAsync<TPost, TResponse>(string route, TPost postObject, Func<string, TResponse> customDeserialization = null, bool multiPartContent = false, string contentType = null);
	}
}