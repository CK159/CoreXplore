using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace App.Util
{
	public class HtmlincludeTagHelper : TagHelper
	{
		private IHostingEnvironment _env;

		public HtmlincludeTagHelper(IHostingEnvironment env)
		{
			_env = env;
		}

		public string Path { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = null;

			if (Path == null)
			{
				return;
			}
			
			string fullPath = System.IO.Path.Combine(_env.ContentRootPath, Path);
			string markup = await File.ReadAllTextAsync(fullPath);
			output.Content.SetHtmlContent(markup);
		}
	}

}
