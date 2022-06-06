using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using MusicParser.Modules.Common;
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
			if (mainSection == null) return new("Page doesn't consist playlist!");

			var titleNode = mainSection.SelectSingleNode("//div[@class='x-column x-sm x-1-3']");
			var imageNode = mainSection.SelectSingleNode("//div[@class='x-column x-sm x-2-3']/img");

			var playlist = new PlaylistModel()
			{
				Title = titleNode?.ChildNodes.FindFirst("h2")?.InnerText.Replace("\n", "") + " - "
					+ titleNode?.ChildNodes.FindFirst("h3")?.InnerText,
				AvatarUrl = imageNode?.Attributes["data-lazy-src"].Value,
				Genre = null,
				ReliseDate = null,
				Songs = parseSongs(mainSection)
			};

			return new() { Result = playlist };
		}

		public async Task<ExecutionResult<PlaylistModel>> ParseUrl(string url)
		{
			var htmlResult = await HtmlReader.ReadHtmlAsText(url);
			if (!htmlResult.Status.Success)
			{
				return new(htmlResult.Status.Message);
			}
			return await ParseHtml(htmlResult.Result);
		}

		private List<SongModel> parseSongs(HtmlNode songsNode)
		{
			var songsNodeBase = songsNode.QuerySelector("h3.h-custom-headline.h3 + *");
			if (songsNodeBase == null) return new List<SongModel>();

			return parseSongsFromElement(songsNodeBase);
		}

		private List<SongModel> parseSongsFromElement(HtmlNode songsBaseNode)
		{
			var songs = new List<SongModel>();
			var children = songsBaseNode.ChildNodes;
			var endChildren = children.SelectMany(x => x.ChildNodes);
			var stringChildren = new List<string>();

			if (children.All(x => x.ChildNodes.Count == 1 && x.ChildNodes[0].Name == "#text"))
			{
				var regex = new Regex(GlobalConstants.NumberRegex);
				foreach (var endChild in endChildren)
				{
					stringChildren.AddRange(regex.Split(endChild.InnerText));
				}
			}
			else
			{
				stringChildren.AddRange(endChildren.Where(x => x.Name == "#text").Select(x => x.InnerText));
			}

			return parseSongTitle(stringChildren);
		}

		private List<SongModel> parseSongTitle(IEnumerable<string> titles)
		{
			var songs = new List<SongModel>();
			foreach (var title in titles)
			{
				var startNumbersRegex = new Regex(@$"^{GlobalConstants.NumberRegex}");
				var songTitle = startNumbersRegex.Replace(title.TrimStart(), "");
				var songData = songTitle.Split(" - ");

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
	}
}
