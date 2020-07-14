using System;
using System.Net;

namespace BlockchainApi.Net.Exceptions
{
	/// <summary>
	/// The exception that is thrown when an error happens in the code for the Blockchain Api
	/// library, not the on the server that the code calls
	/// </summary>
	public class ClientApiException : ApiExceptionBase
	{
		public ClientApiException(string message) : base(message)
		{
		}
	}
}
