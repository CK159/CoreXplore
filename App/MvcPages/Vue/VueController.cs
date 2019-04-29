using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.MvcPages.Vue
{
	public class VueController : Controller
	{
		private readonly IHostingEnvironment _env;
		
		public VueController(IHostingEnvironment env)
		{
			_env = env;
		}
		
		public IActionResult Index()
		{
			return View();
		}
		
		public IActionResult Sample()
		{
			return View();
		}
		
		public IActionResult PreviewDetail()
		{
			return View();
		}
		
		public IActionResult AsyncSelect()
		{
			return View();
		}
		
		public IActionResult Product()
		{
			return View();
		}
		
		[HttpPost]
		public IActionResult GetRecordPreview()
		{
			return new JsonResult(LoadFromFile());
		}
		
		[HttpPost]
		public IActionResult GetRecordDetail(int id)
		{
			List<RecordDetail> records = LoadFromFile();
			RecordDetail record = records.SingleOrDefault(r => r.id == id);

			return new JsonResult(record);
		}
		
		[HttpPost]
		public IActionResult SaveRecordDetail(string json)
		{
			RecordDetail record = JsonConvert.DeserializeObject<RecordDetail>(json);
			List<RecordDetail> records = LoadFromFile();

			if (record.id <= 0)
			{
				//New record
				//Not sure if I need all this....
				int maxId = records.OrderByDescending(i => i.id).FirstOrDefault()?.id ?? 0;
				record.id = maxId + 1;
				
				records.Add(record);
			}
			else
			{
				//Existing record. Find old entry and replace it with the new one
				int index = records.FindIndex(r => r.id == record.id);

				if (index >= 0)
				{
					records[index] = record;
				}
			}

			SaveToFile(records);
			return new JsonResult(new {id = record.id});
		}
		
		[HttpPost]
		public IActionResult DeleteRecordDetail(int id)
		{
			List<RecordDetail> records = LoadFromFile();
			
			records.RemoveAll(r => r.id == id);
			
			SaveToFile(records);
			return new OkResult();
		}
		
		private List<RecordDetail> LoadFromFile()
		{
			string path = GetPath();

			if (System.IO.File.Exists(path))
			{
				return JsonConvert.DeserializeObject<List<RecordDetail>>(System.IO.File.ReadAllText(path));
			}

			return new List<RecordDetail>();
		}

		private void SaveToFile(List<RecordDetail> records)
		{
			string path = GetPath();

			System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(records));
		}

		private string GetPath()
		{
			return _env.ContentRootPath + "\\nosql-db\\records.json";
		}
	}
	
	public class RecordDetail
	{
		public int id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public bool active { get; set; }
		public string date { get; set; }
		public List<RecordPrice> prices { get; set; }
	}

	public class RecordPrice
	{
		public int id { get; set; }
		public int type { get; set; }
		public decimal amount { get; set; }
		public string name { get; set; }
	}
}
