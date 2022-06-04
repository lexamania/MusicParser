using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicParser.Modules.Models
{
	public class Song
	{
		public string Name { get; set; }
		public string? ArtistName { get; set; }
		public string? AlbumName { get; set; }
		public DateTime? Duration { get; set; }
		public string? Genre { get; set; }
	}
}