using Octokit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace SigmoidColorFastAviUtlDownloader
{
	class AviUtlVersionManager
	{
		static readonly DateTime aviutl_1_00_date = new DateTime(2013, 4, 1, 0, 0, 0);
		public string AviutlPath;
		public string AviutlExecPath
		{
			get { return System.IO.Path.Combine(AviutlPath, "aviutl.exe"); }
		}
		public AviUtlVersionManager(string AviutlPath)
		{
			this.AviutlPath = AviutlPath;
		}
		public bool IsLatest()
		{
			try
			{
				System.IO.File.GetLastWriteTime(AviutlExecPath);
				return true;
			}
			catch (Exception){
			}
			return false;
		}
		public async Task<bool> Download()
		{
			return true;
		}
	}

}
