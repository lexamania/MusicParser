using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MusicParser.Modules.Models;

namespace MusicParser.Modules.Interfaces
{
	public interface IHtmlReader
	{
		Task<ExecutionResult<string>> ReadHtmlAsText(string url);
		Task<ExecutionResult<Stream>> ReadHtmlAsStream(string url);
		Task<ExecutionResult<HtmlDocument>> ReadHtmlAsDocument(string url);
	}
}