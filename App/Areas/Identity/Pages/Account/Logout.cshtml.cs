using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace App.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
	public class LogoutModel : PageModel
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ILogger<LogoutModel> _logger;

		public LogoutModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<LogoutModel> logger)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_logger = logger;
		}

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost(string returnUrl = null)
		{
			string username = (await _userManager.GetUserAsync(HttpContext.User))?.UserName;
			
			await _signInManager.SignOutAsync();
			
			_logger.LogInformation("User {username} logged out.", username);
			
			if (returnUrl != null)
			{
				return LocalRedirect(returnUrl);
			}
			else
			{
				return Page();
			}
		}
	}
}
