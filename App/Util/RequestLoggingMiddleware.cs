using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;

/* Reading the request body: A 3-step process
 * 1. Get a reference to the request body stream at the beginning of the stream and allow resetting back to beginning
 * 2. Read the body stream
 * 3. Set body stream back to beginning using initial reference
 */

/* Reading the response body: A 6-step process
 * 1. Get a reference to the original response body stream (which is connected to the client) before anything is put into it
 * 2. Make a new stream that allows for seeking/rewinding
 * 3. Assign that new stream to the response body 
 * 4. Execute the request (the rest of the pipeline will dump stuff into the new stream)
 * 5. Look through the now-populated response body
 * 6. Copy all of the new stream into the original stream since only the original one gets sent to the client
 */

//https://elanderson.net/2017/02/log-requests-and-responses-in-asp-net-core/
//http://www.palador.com/2017/05/24/logging-the-body-of-http-request-and-response-in-asp-net-core/
//https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/

namespace App.Util
{
    public class RequestLoggingMiddleware
    {   
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //TODO: Do not use dependency injection for context - this middleware should be entirely separate from other code
        //By using the dependency injected context, the SaveChanges() calls in here could save things that
        //weren't supposed to have been saved by the main action
        //Create a context subclass and register it as a transient dependency and request an instance of it twice instead
        //(one for the pre-request logging and one for the post-request logging
        public async Task Invoke(HttpContext context, Dbc dbContext)
        {
            Stopwatch timer = Stopwatch.StartNew();
            DateTime requestBegin = DateTime.Now;
            
            //Log initial request data
            RequestLog log = await LogRequest(context.Request, requestBegin, dbContext);

            Stream originalRespBody = context.Response.Body;
            
            //Setup new stream for the response to be put into
            using (MemoryStream responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                
                //Process request - Execute request pipeline
                await _next.Invoke(context);
                
                //The request is now complete and response data is available - find out what happened
                await LogResponse(context.Response, log, timer, dbContext);
            
                //Copy the contents of the new stream (which contains the response)
                //to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalRespBody);
                //Seems like a good idea to put things back in case this isn't the very first middleware in the chain
                context.Response.Body = originalRespBody;
            }
        }
    
        private async Task<RequestLog> LogRequest(HttpRequest req, DateTime requestBegin, Dbc dbContext)
        {
            RequestLog log = new RequestLog
            {
                RequestBegin = requestBegin,
                URL = req.GetDisplayUrl(),
                RequestMethod = req.Method,
                RequestContentType = req.Headers.GetValueOrDefault("Content-Type", ""),
                RequestSize = (int)(req.ContentLength ?? 0),
                IP = req.HttpContext.Connection.RemoteIpAddress.ToString(),
                UserAgent = req.Headers.GetValueOrDefault("User-Agent", ""),
                Referer = req.Headers.GetValueOrDefault("Referer", "")
            };

            //Capture request body stream at start position
            Stream originalReqBody = req.Body;
            //Allow resetting stream back to start after reading it
            req.EnableRewind();

            //Get the request body
            string requestAsText = await new StreamReader(req.Body).ReadToEndAsync();

            //Save a bit of it for logging
            if (!requestAsText.IsNullOrWhitespace())
                log.RequestText = requestAsText.Left(2048);

            //Reset the request stream back to the beginning
            req.Body = originalReqBody;
            
            dbContext.RequestLogs.Add(log);
            await dbContext.SaveChangesAsync();

            return log;
        }

        private async Task LogResponse(HttpResponse resp, RequestLog log, Stopwatch timer, Dbc dbContext)
        {
            dbContext.Entry(log);
            log.ResponseStatus = resp.StatusCode;
            log.ResponseContentType = resp.Headers.GetValueOrDefault("Content-Type", "");
            //ContentLength not always set. Fallback to buffer length if unset
            log.ResponseSize = (int)(resp.ContentLength ?? resp.Body.Length);
            log.Location = resp.Headers.GetValueOrDefault("Location", "");
            //TODO: log.ResponseType = ???!!!
            
            //Earlier, the response.body stream was replaced with one that allows rewinding
            //so rewind to beginning, read it, and rewind it again.
            resp.Body.Seek(0, SeekOrigin.Begin);
            string responseAsText = await new StreamReader(resp.Body).ReadToEndAsync();
            resp.Body.Seek(0, SeekOrigin.Begin);
            
            if (!responseAsText.IsNullOrWhitespace())
                log.ResponseText = responseAsText.Left(2048);

            //Do as much work as possible before getting the time
            log.ResponseMs = (decimal)timer.Elapsed.TotalMilliseconds;
            
            await dbContext.SaveChangesAsync();
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