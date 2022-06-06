using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using HtmlAgilityPack;
using MusicParser.Modules.Interfaces;
using MusicParser.Modules.Models;

namespace MusicParser.Modules.Services
{
	public static class HtmlReader
	{
		public static async Task<ExecutionResult<string>> ReadHtmlAsText(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return new("Enter url!");
			}

			try
			{
				using (HttpClient client = new HttpClient())
				{
					using (HttpResponseMessage response = client.GetAsync(url).Result)
					{
						using (HttpContent content = response.Content)
						{
							var data = content.ReadAsStringAsync().Result;
							data = System.Web.HttpUtility.HtmlDecode(data);
							data = fixEncoding(data);

							return new(null) { Result = data };
						}
					}
				}
			}
			catch (Exception ex)
			{
				return new("Url doesn't exist.");
			}
		}

		public static Bitmap? DownloadImg(string? url)
		{
			if (string.IsNullOrEmpty(url))
			{
				url = "https://img5.goodfon.ru/wallpaper/nbig/3/73/abstraktsiia-antisfera-vodovorot-krasok-kartinka-chernyi-fon.jpg";
			}
			try
			{
				using (HttpClient client = new HttpClient())
				{
					using (HttpResponseMessage response = client.GetAsync(url).Result)
					{
						using (HttpContent content = response.Content)
						{
							var data = content.ReadAsStreamAsync().Result;
							return new Bitmap(data);
						}
					}
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		private static string fixEncoding(string text)
		{
			return text.Replace('\u2013', '-')
				.Replace('\u2014', '-')
				.Replace('\u2015', '-');
		}
	}
}