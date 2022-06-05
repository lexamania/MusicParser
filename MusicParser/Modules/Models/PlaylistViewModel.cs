using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MusicParser.Modules.Models
{
	public class PlaylistViewModel : INotifyPropertyChanged
	{
		private string _url;
		private string? _htmlPage;
		private PlaylistModel _playlists;

		public string? HtmlPage { 
			get => _htmlPage;
			set
			{	
				_htmlPage = value;
				OnPropertyChanged();
			}
		}
		public string Url
		{
			get => _url;
			set
			{
				_url = value;
				OnPropertyChanged();
			}
		}
		public PlaylistModel Playlists 
		{ 
			get => _playlists;
			set
			{
				_playlists = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public virtual void OnPropertyChanged([CallerMemberName]string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}