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

			try
			{
				Exception inner = new Exception("some inner exception");
				throw inner;
			}
			catch (Exception e)
			{
				Exception in2 = new ApplicationException("Middle exception", e);
				Exception ex = new Exception("Outer Exception", in2);
				MyProblematicMethod(ex);
			}

			return null;
		}

		private void MyProblematicMethod(Exception ex)
		{
			throw ex;
		}
	}
}
