using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MusicParser.Modules.Interfaces;
using MusicParser.Modules.Services;

namespace MusicParser.Modules.Models
{
	public class PlaylistViewModel : INotifyPropertyChanged
	{
		private string _url;
		private string? _htmlPage;
		private PlaylistModel _playlist;

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
		public PlaylistModel Playlist 
		{ 
			get => _playlist;
			set
			{
				_playlist = value;
				OnPropertyChanged();
			}
		}

		public void ButtonParseClick()
		{
			var htmlParser = getHtmlParser(Url);

			var result = htmlParser.ParseUrl(Url).GetAwaiter().GetResult();
			if (result.Success)
			{
				Playlist = result.Result;
				HtmlPage = Playlist.ToString();
			}
			else
			{
				HtmlPage = "Some error";
			}
		}

		private IMusicPageHtmlParser getHtmlParser(string url)
		{
			//TODO For every parsed link need to return own parser
			return new GlobalUndergroundHtmlParser();
		}
		
		public event PropertyChangedEventHandler? PropertyChanged;
		public virtual void OnPropertyChanged([CallerMemberName]string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}