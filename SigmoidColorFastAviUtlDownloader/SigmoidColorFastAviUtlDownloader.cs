using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;
using System.IO.Compression;

namespace SigmoidColorFastAviUtlDownloader
{
	class SigmoidColorFastAviUtlDownloader
	{
		public string AviutlPath;
		public string PluginPath
		{
			get
			{
				var re = Path.Combine(AviutlPath, "SigColorFastAviUtl.auf");
				if (File.Exists(re)){
					return re;
				}
				return Path.Combine(AviutlPath, @"plugins\SigColorFastAviUtl.auf");
			}
		}
		public SigmoidColorFastAviUtlDownloader(string AviutlPath)
		{
			this.AviutlPath = AviutlPath;
		}
		public async Task<bool> Download()
		{
			try
			{
				MainWindow.WriteOutMessage(new RichTextItem
				{
					Text = "SigmoidColorFastAviUtlのURL取得中..."
				});
				//User-Agentがないと403エラーが帰る
				MainWindow.httpClient.DefaultRequestHeaders.Add(
					"User-Agent",
					"Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36"
				);
				string json_str = await MainWindow.httpClient.GetStringAsync(
					"https://api.github.com/repos/yumetodo/SigContrastFastAviUtl/releases"
				);
				var result = JsonConvert.DeserializeObject<List<GithubReleasesResponse>>(json_str);
				var url = result[0].assets[0].browser_download_url;
				MainWindow.WriteOutMessage(new RichTextItem
				{
					Text = url
				});
				var response_zip = await MainWindow.httpClient.GetByteArrayAsync(url);
				var response_zip_stream = new MemoryStream(response_zip);
				var zip = new ZipArchive(response_zip_stream);
				System.IO.Compression.FileSystem.ZipFileExtensions.ExtractToDirectory(zip, PluginPath);
				return true;
			}
			catch (HttpRequestException e)
			{
				// 404エラーや、名前解決失敗など
				MainWindow.WriteOutMessage(new RichTextItem
				{
					Text = "例外発生!"
				});
				// InnerExceptionも含めて、再帰的に例外メッセージを表示する
				Exception ex = e;
				while (ex != null)
				{
					MainWindow.WriteOutMessage(new RichTextItem
					{
						Text = "例外メッセージ: " + ex.Message
					});
					ex = ex.InnerException;
				}
				return false;
			}
			catch (TaskCanceledException e)
			{
				// タスクがキャンセルされたとき（一般的にタイムアウト）
				MainWindow.WriteOutMessage(new RichTextItem
				{
					Text = "タイムアウト!"
				});
				MainWindow.WriteOutMessage(new RichTextItem
				{
					Text = "例外メッセージ: " + e.Message
				});
				return false;
			}
		}
	}
	public class GithubReleasesResponse
	{
		public class Parson
		{
			public string login { get; set; }
			public int id { get; set; }
			public string avatar_url { get; set; }
			public string gravatar_id { get; set; }
			public string url { get; set; }
			public string html_url { get; set; }
			public string followers_url { get; set; }
			public string following_url { get; set; }
			public string gists_url { get; set; }
			public string starred_url { get; set; }
			public string subscriptions_url { get; set; }
			public string organizations_url { get; set; }
			public string repos_url { get; set; }
			public string events_url { get; set; }
			public string received_events_url { get; set; }
			public string type { get; set; }
			public bool site_admin { get; set; }
		}
		public class Asset
		{
			public string url { get; set; }
			public int id { get; set; }
			public string name { get; set; }
			public object label { get; set; }
			public Parson uploader { get; set; }
			public string content_type { get; set; }
			public string state { get; set; }
			public int size { get; set; }
			public int download_count { get; set; }
			public string created_at { get; set; }
			public string updated_at { get; set; }
			public string browser_download_url { get; set; }
		}
		public string url { get; set; }
		public string assets_url { get; set; }
		public string upload_url { get; set; }
		public string html_url { get; set; }
		public int id { get; set; }
		public string tag_name { get; set; }
		public string target_commitish { get; set; }
		public string name { get; set; }
		public bool draft { get; set; }
		public Parson author { get; set; }
		public bool prerelease { get; set; }
		public string created_at { get; set; }
		public string published_at { get; set; }
		public List<Asset> assets { get; set; }
		public string tarball_url { get; set; }
		public string zipball_url { get; set; }
		public string body { get; set; }
	}
}
