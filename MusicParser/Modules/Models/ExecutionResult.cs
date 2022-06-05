using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicParser.Modules.Models
{
	public class ExecutionResult<T>
	{
		public bool Success { get; private set; }
		public string Message { get; private set; }
		public T? Result { get; set; }

		public ExecutionResult(string? message)
		{
			Success = message == null;
			Message = message ?? string.Empty;
		}

	}
}