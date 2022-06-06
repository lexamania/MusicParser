using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MusicParser.Modules.Common;
using MusicParser.Modules.Interfaces;
using MusicParser.Modules.Services;

namespace MusicParser.Modules.Models
{
	public class PlaylistViewModel : INotifyPropertyChanged
	{
		private string _url;
		private PlaylistModel _playlist;
		private ExecutionSuccess _error;

		public string Url
		{
			get => _url;
			set
			{
				_url = value?.Trim();
				OnPropertyChanged();
			}
		}
		public PlaylistModel Playlist 
		{ 
			get => _playlist;
			set
			{
				_playlist = value;
				OnPropertyChanged();
			}
		}
		public ExecutionSuccess Error 
		{ 
			get => _error; 
			set 
			{
				_error = value;
				OnPropertyChanged();
			}
		}

		public PlaylistViewModel()
		{
			Url = GlobalConstants.DefaultUrlForTest;
			setFields();
		}

		public void ButtonParseClick()
		{
			var htmlParser = getHtmlParser(Url);

			var result = htmlParser.ParseUrl(Url).GetAwaiter().GetResult();
			if (result.Status.Success)
			{
				setFields(playlist: result.Result);
			}
			else
			{
				setFields(error: result.Status);
			}
		}

		private void setFields(PlaylistModel playlist = null, ExecutionSuccess error = null)
		{
			Playlist = playlist;
			Error = error;
		}
	
		private IMusicPageHtmlParser getHtmlParser(string url)
		{
			//TODO For every parsed link need to return own parser
			var regexGU = new Regex(@$"^{GlobalConstants.GlobalUndergroundMusicUrl}\S+");

			if (regexGU.IsMatch(url)) return new GlobalUndergroundHtmlParser();

			return new DefaultHtmlParser();
		}
		
		public event PropertyChangedEventHandler? PropertyChanged;
		public virtual void OnPropertyChanged([CallerMemberName]string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}