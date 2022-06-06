using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicParser.Modules.Models
{
	public class SongModel
	{
		public string Title { get; set; }
		public string? Artist { get; set; }
		public string? Album { get; set; }
		public string? Duration { get; set; }
		public string? Genre { get; set; }
		
		public override string? ToString()
		{
			return $"{Artist} - {Title}";
		}
	}
}