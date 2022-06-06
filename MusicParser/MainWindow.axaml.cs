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

		private IMusicPageHtmlParser getHtmlParser(string url)
		{
			return new GlobalUndergroundHtmlParser();
		}
	}
}
