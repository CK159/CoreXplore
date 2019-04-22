using System;
using App.Util;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
	[ApiErrorHandler]
	public class TestController
	{
		public IActionResult TestError(int? a)
		{
			if (a == null || a != 1)
			{
				throw new NotImplementedException();
			}

			Exception inner = new Exception("some inner exception");
			Exception in2 = new ApplicationException("Middle exception", inner);
			throw new Exception("Outer Exception", in2);
		}
	}
}
