using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicParser.Modules.Interfaces;
using MusicParser.Modules.Models;

namespace MusicParser.Modules.Services
{
	public class DefaultHtmlParser : IMusicPageHtmlParser
	{
		public async Task<ExecutionResult<PlaylistModel>> ParseHtml(string html)
		{
			return new("Program doesn't support this page.");
		}

		public async Task<ExecutionResult<PlaylistModel>> ParseUrl(string url)
		{
			return await ParseHtml("");
		}
	}
}