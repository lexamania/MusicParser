using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using MusicParser.Modules.Interfaces;
using MusicParser.Modules.Models;

namespace MusicParser.Modules.Services
{
	internal class GlobalUndergroundHtmlParser : IMusicPageHtmlParser
	{
		public async Task<ExecutionResult<PlaylistModel>> ParseHtml(string html)
		{
			if (string.IsNullOrEmpty(html))
			{
				return new ("Empty html");
			}
			
			var document = new HtmlDocument();
			document.LoadHtml(html);

			var mainSection = document.DocumentNode.SelectSingleNode("//div[@id='x-section-1']");
			var titleNode = mainSection.SelectSingleNode("//div[@class='x-column x-sm x-1-3']");
			var imageNode = mainSection.SelectSingleNode("//div[@class='x-column x-sm x-2-3']/img");
			var songsNode = mainSection;


			var playlist = new PlaylistModel()
			{
				Title = titleNode.ChildNodes.FindFirst("h2")?.InnerText.Replace("\n", "") + " - "
					+ titleNode.ChildNodes.FindFirst("h3")?.InnerText,
				AvatarUrl = imageNode?.Attributes["data-lazy-src"].Value,
				Genre = null,
				ReliseDate = null,
				Songs = parseSongs(mainSection)
			};

			return new(null) { Result = playlist };
		}

		public async Task<ExecutionResult<PlaylistModel>> ParseUrl(string url)
		{
			var htmlResult = await HtmlReader.ReadHtmlAsText(url);
			if (!htmlResult.Success)
			{
				return new(htmlResult.Message);
			}
			return await ParseHtml(htmlResult.Result);
		}

		private List<SongModel> parseSongs(HtmlNode songsNode)
		{
			var songsNodeBase = songsNode.QuerySelector("h3.h-custom-headline.h3 + *");
			if (songsNodeBase == null) return new List<SongModel>();

			switch(songsNodeBase.Name)
			{
				case "ul":
					return parseSongsFromUl(songsNodeBase);
				case "div":
					return parseSongsFromDiv(songsNodeBase);
				default:
					return new List<SongModel>();
			}
		}

		private List<SongModel> parseSongsFromUl(HtmlNode songsBaseNode)
		{
			var songs = new List<SongModel>();
			
			foreach (var songNode in songsBaseNode.ChildNodes.SelectMany(x => x.ChildNodes).Where(x => x.Name == "#text"))
			{
				var startNumbersRegex = new Regex(@"^(\s*\d+(\.|\))?\s*)");
				var songTitle = startNumbersRegex.Replace(songNode.InnerText.TrimStart(), "");
				var songData = songTitle.Split(" - ");//.Split(" \u2013 ");
				
				if (songData.Length < 2) continue;

				var song = new SongModel()
				{
					Title = songData[1].Trim(),
					Artist = songData[0].Trim(),
					Album = null,
					Duration = null,
					Genre = null
				};
				songs.Add(song);
			}
			return songs;
		}

		private List<SongModel> parseSongsFromDiv(HtmlNode songsBaseNode)
		{
			var songs = new List<SongModel>();

			return songs;
		}
	}
}
