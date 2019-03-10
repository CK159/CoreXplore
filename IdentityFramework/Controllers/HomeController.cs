using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityFramework.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			
			
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";
			
			List<string> roles = ((ClaimsIdentity)User.Identity).Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value)
                            .ToList();

			return View(new AboutVM
			{
				Roles = roles,
				Claims = ((ClaimsIdentity)User.Identity).Claims.ToList()
			});
		}

		public class AboutVM
		{
			public List<string> Roles { get; set; }
			public List<Claim> Claims { get; set; }
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		[Authorize(Roles = "TestRole")]
		public ActionResult TestRolePage()
		{
			ViewBag.Message = "This page requires 'TestRole' role.";

			return View("Test");
		}
	}
}
