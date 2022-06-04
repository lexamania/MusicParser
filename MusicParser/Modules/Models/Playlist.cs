using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicParser.Modules.Models
{
	public class Playlist
	{
		public string Name { get; set; }
		public Uri? AvatarUrl { get; set; }
		public DateTime? ReliseDate { get; set; }
		public string? Genre { get; set; }
		public List<Song> Songs { get; set; }
	}
}