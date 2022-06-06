using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicParser.Modules.Models
{
	public class ExecutionResult<T>
	{
		public ExecutionSuccess Status { get; private set; }
		public T? Result { get; set; }

		public ExecutionResult(string message = null)
		{
			Status = new ExecutionSuccess(message);
		}

	}

	public class ExecutionSuccess
	{
		public ExecutionSuccess(string message)
		{
			Success = string.IsNullOrEmpty(message);
			Message = message;
		}

		public bool Success { get; private set; }
		public string Message { get; private set; }
	}
}