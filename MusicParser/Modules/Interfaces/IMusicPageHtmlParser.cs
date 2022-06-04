using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicParser.Modules.Models;

namespace MusicParser.Modules.Interfaces
{
	internal interface IMusicPageHtmlParser
	{
		IEnumerable<Playlist> ParsePage(string html);
	}
}
