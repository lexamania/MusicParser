using System.Collections.Generic;
using MusicParser.Modules.Interfaces;
using MusicParser.Modules.Models;

namespace MusicParser.Modules.Services
{
	internal class GlobalUndergroundHtmlParser : IMusicPageHtmlParser
	{
		public IEnumerable<Playlist> ParsePage(string html)
		{
			throw new System.NotImplementedException();
		}
	}
}
