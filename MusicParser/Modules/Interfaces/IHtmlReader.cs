using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicParser.Modules.Interfaces
{
	public interface IHtmlReader
	{
		string ReadHtml(string url);
	}
}