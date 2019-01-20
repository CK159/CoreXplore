using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;

//https://elanderson.net/2017/02/log-requests-and-responses-in-asp-net-core/
//http://www.palador.com/2017/05/24/logging-the-body-of-http-request-and-response-in-asp-net-core/
namespace App.Util
{
    public class RequestLoggingMiddleware
    {   
        private readonly RequestDelegate _next;
       // private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next/*, ILogger<RequestLoggingMiddleware> logger*/)
        {
            _next = next;
            //_logger = logger;
        }

        //TODO: Do not use dependency injection for context - this middleware should be entirely separate from other code
        //By using the dependency injected context, the SaveChanges() calls in here could save things that
        //weren't supposed to have been saved by the main action
        //Create a context subclass and register it as a transient dependency and request an instance of it twice instead
        //(one for the pre-request logging and one for the post-request logging
        //TODO: Properly record response data. That all needs to come after the await _next.Invoke(context);
        public async Task Invoke(HttpContext context, Dbc dbContext)
        {
            MemoryStream injectedRequestStream = new MemoryStream();
        
            try
            {
                HttpRequest req = context.Request;
                HttpResponse resp = context.Response;
                
                RequestLog log = new RequestLog
                {
                    URL = req.GetDisplayUrl(),
                    RequestMethod = req.Method,
                    RequestContentType = req.Headers.GetValueOrDefault("Content-Type", ""),
                    RequestText = "",
                    ResponseStatus = resp.StatusCode,
                    ResponseContentType = resp.Headers.GetValueOrDefault("Content-Type", ""),
                    ResponseSize = (int)(resp.ContentLength ?? 0),
                    ResponseMs = -1, //https://stackoverflow.com/questions/37395227/add-response-headers-to-asp-net-core-middleware
                    ResponseType = ResponseTypes.Unknown,
                    ResponseText = "",
                    IP = resp.HttpContext.Connection.RemoteIpAddress.ToString(),
                    UserAgent = req.Headers.GetValueOrDefault("User-Agent", ""),
                    Referer = req.Headers.GetValueOrDefault("Referer", ""),
                    Location = resp.Headers.GetValueOrDefault("Location", "")
                };
                
                //var requestLog = 
                //$"REQUEST HttpMethod: {req.Method}, Path: {req.Path}";
        
                using (var bodyReader = new StreamReader(req.Body))
                {
                    var bodyAsText = bodyReader.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(bodyAsText))
                    {
                        log.RequestText = bodyAsText.Substring(0, Math.Min(bodyAsText.Length, 2048));
                    }
        
                    byte[] bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
                    injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                    injectedRequestStream.Seek(0, SeekOrigin.Begin);
                    req.Body = injectedRequestStream;
                }
        
                //_logger.LogTrace(requestLog);
				//Console.WriteLine(requestLog);

                dbContext.RequestLogs.Add(log);
                await dbContext.SaveChangesAsync();
        
                await _next.Invoke(context);                                
            }
            finally
            {
                injectedRequestStream.Dispose();
            }           
        }  
    }

    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}