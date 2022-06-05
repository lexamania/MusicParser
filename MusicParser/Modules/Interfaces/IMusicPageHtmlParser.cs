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
		Task<ExecutionResult<IEnumerable<PlaylistModel>>> ParseHtml(string html);
		Task<ExecutionResult<IEnumerable<PlaylistModel>>> ParseUrl(string url);
	}
}
