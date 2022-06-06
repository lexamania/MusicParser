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
		Task<ExecutionResult<PlaylistModel>> ParseHtml(string html);
		Task<ExecutionResult<PlaylistModel>> ParseUrl(string url);
	}
}
