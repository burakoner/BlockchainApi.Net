using System;
using System.Net;

namespace BlockchainApi.Net.Exceptions
{
	/// <summary>
	/// The class `ApiException` represents a failed call to the Blockchain API. Whenever
	/// the server is unable to process a request (usually due to parameter validation errors),
	/// an instance of this class is thrown.
	/// </summary>
	public class ServerApiException : ApiExceptionBase
	{
		public HttpStatusCode StatusCode { get; }
		public ServerApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
		{
			this.StatusCode = statusCode;
		}
	}
}
