using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Parsers;
using MusicParser.Modules.Interfaces;
using MusicParser.Modules.Models;
using MusicParser.Modules.Services;

namespace MusicParser
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new PlaylistViewModel();
		}

		public async Task ButtonParseClick(object sender, RoutedEventArgs args)
		{
			if (this.DataContext is not PlaylistViewModel) return;
			if (sender is not Button) return;

			var vm = this.DataContext as PlaylistViewModel;
			var htmlParser = getHtmlParser(vm.Url);
			await htmlParser.ParseUrl(vm.Url).ContinueWith(t =>
			{
				if (t.IsCompletedSuccessfully && t.Result.Success)
				{
					vm.Playlists = t.Result.Result.FirstOrDefault();
				}
				else
				{
					vm.HtmlPage = "Some error";
				}
			});
		}

		private IMusicPageHtmlParser getHtmlParser(string url)
		{
			return new GlobalUndergroundHtmlParser();
		}
	}
}
