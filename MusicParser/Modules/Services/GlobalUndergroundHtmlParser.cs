using System.Collections.Generic;
using System.Threading.Tasks;
using MusicParser.Modules.Interfaces;
using MusicParser.Modules.Models;

namespace MusicParser.Modules.Services
{
	internal class GlobalUndergroundHtmlParser : IMusicPageHtmlParser
	{
		private readonly IHtmlReader _htmlReader = new HtmlReader();

		public async Task<ExecutionResult<IEnumerable<PlaylistModel>>> ParseHtml(string html)
		{
			throw new System.NotImplementedException();
		}

		public async Task<ExecutionResult<IEnumerable<PlaylistModel>>> ParseUrl(string url)
		{
			var htmlResult = await _htmlReader.ReadHtmlAsText(url);
			if (!htmlResult.Success)
			{
				return new(htmlResult.Message);
			}
			return await ParseHtml(htmlResult.Result);
		}
	}
}
