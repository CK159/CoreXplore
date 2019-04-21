using System;
using System.Collections.Generic;

namespace App.MVCPages.TestMvcPage
{
	public class TestMvcPageModel
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }

		public static List<TestMvcPageModel> GetSampleData()
		{
			return new List<TestMvcPageModel>
			{
				new TestMvcPageModel
				{
					Id = 1,
					Date = new DateTime(2019, 01, 01),
					Name = "The First One",
					Value = "N/A"
				},
				new TestMvcPageModel
				{
					Id = 2,
					Date = DateTime.Now,
					Name = "Key2",
					Value = "something"
				},
				new TestMvcPageModel
				{
					Id = 3,
					Date = DateTime.Now.AddDays(30),
					Name = "Last-1",
					Value = "123-4567"
				}
			};
		}
	}
}
