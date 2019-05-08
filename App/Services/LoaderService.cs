using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace App.Util
{
	public class LoaderService
	{
		private readonly IHostingEnvironment _env;
		
		public LoaderService(IHostingEnvironment env)
		{
			_env = env;
		}
		
		private string GetPath(string filename)
		{
			return Path.Combine(_env.ContentRootPath, $"nosql-db\\{filename}.json");
		}

		public T LoadFromFile<T>(string file) where T : new()
		{
			string path = GetPath(file);

			if (File.Exists(path))
			{
				return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
			}

			return new T();
		}

		public void SaveToFile(object items, string filename)
		{
			//https://stackoverflow.com/a/39037146
			using (FileStream fs = File.Create(GetPath(filename)))
			using (StreamWriter sw = new StreamWriter(fs))
			using (JsonTextWriter jtw = new JsonTextWriter(sw)
			{
				Formatting = Formatting.Indented,
				Indentation = 1,
				IndentChar = '\t'
			})
			{
				new JsonSerializer().Serialize(jtw, items);
			}
		}
	}
}
