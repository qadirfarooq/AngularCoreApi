using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.MiddleWare
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        public ILogger<ExceptionMiddleware> Logger { get; }
        public IHostEnvironment Env { get; }
        // who is next middle ware to call.. thats what he request delegate is
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        
        {
            this.Env = env;
            this.Logger = logger;
            this.next = next;

        }
        // must have same name to tell framework that there is method to invoke.. in middle ware.. 
        public async Task InvokeAsync(HttpContext context)
        {
            try{
                    await next(context);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                var response = Env.IsDevelopment() ? new ApiException(context.Response.StatusCode, 
                ex.Message, ex.StackTrace?.ToString()):
                 new ApiException(context.Response.StatusCode, ex.Message," Internal Server Error");  

                 var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                 var json = JsonSerializer.Serialize(response,options);
                 await context.Response.WriteAsync(json);
            }
        }
    }
}