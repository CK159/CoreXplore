using System.Collections.Generic;
using Db;
using Microsoft.AspNetCore.Mvc;

namespace App.MVCPages.TestMvcPage
{
	public class TestMvcPageController : Controller
	{
		public IActionResult Index()
		{
			List<TestMvcPageModel> data = TestMvcPageModel.GetSampleData();
			return View(data);
		}
	}
}
