using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace App.Util
{
	public class ApiErrorHandler : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			ILoggerFactory loggerFactory = (ILoggerFactory)context.HttpContext.RequestServices.GetService(typeof(ILoggerFactory));

			if (loggerFactory != null)
			{
				ILogger logger = loggerFactory.CreateLogger(typeof(ApiErrorHandler));
				logger.LogError(context.Exception, "API Error {type}: {message}",
					context.Exception.GetType().FullName, context.Exception.Message);
			}
			
			ProblemDetailsX pd = new ProblemDetailsX(context.Exception);
			context.Result = new JsonResult(pd, new JsonSerializerSettings
			{
				Formatting = Formatting.Indented
			});
			context.HttpContext.Response.StatusCode = pd.Status ?? 500;
			context.ExceptionHandled = true;
			base.OnException(context);
		}
	}
}
