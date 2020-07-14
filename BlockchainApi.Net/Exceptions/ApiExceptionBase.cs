using System;
using System.Net;

namespace BlockchainApi.Net.Exceptions
{
	/// <summary>
	/// The base exception for the BlockChain Api. Its only use is to detect if the
	/// exception came from the api rather that another source
	/// </summary>
	public abstract class ApiExceptionBase : Exception
	{
		protected ApiExceptionBase(string message)
			: base(message)
		{
		}
	}
}
