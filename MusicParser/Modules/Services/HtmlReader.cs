using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MusicParser.Modules.Interfaces;
using MusicParser.Modules.Models;

namespace MusicParser.Modules.Services
{
	public class HtmlReader : IHtmlReader
	{
		public async Task<ExecutionResult<HtmlDocument>> ReadHtmlAsDocument(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return new("Enter url!");
			}

			try
			{
				var web = new HtmlWeb();
				return new(null) { Result = web.LoadFromWebAsync(url).Result};
			}
			catch (Exception ex)
			{
				return new("Url doesn't exist.");
			}
		}

		public async Task<ExecutionResult<Stream>> ReadHtmlAsStream(string url)
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
							var data = content.ReadAsStreamAsync().Result;
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

		public async Task<ExecutionResult<string>> ReadHtmlAsText(string url)
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
	}
}