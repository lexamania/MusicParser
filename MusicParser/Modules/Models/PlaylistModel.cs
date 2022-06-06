using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using MusicParser.Modules.Services;

namespace MusicParser.Modules.Models
{
	public class PlaylistModel
	{
		public string Title { get; set; }
		private string? _avatarUrl = null;
		public string? AvatarUrl 
		{ 
			get => _avatarUrl;
			set
			{
				_avatarUrl = value;
				_avatar = null;
			}
		}
		private Bitmap? _avatar = null;
		public Bitmap Avatar 
		{ 
			get
			{
				if (_avatar == null)
				{
					_avatar = HtmlReader.DownloadImg(AvatarUrl);
				}
				return _avatar;
			}
		}
		public string? Genre { get; set; }
		public DateTime? ReliseDate { get; set; }
		public List<SongModel> Songs { get; set; }

		public PlaylistModel()
		{
			Songs = new List<SongModel>();
		}
	}
}