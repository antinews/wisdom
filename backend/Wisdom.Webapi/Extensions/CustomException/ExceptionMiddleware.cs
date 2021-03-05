using log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Wisdom.Webapi.Extensions.CustomException.Models;

namespace Wisdom.Webapi.Extensions.CustomException
{
    public class ExceptionMiddleware
    {
        private static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(GlobalExceptionFilter));
        private readonly RequestDelegate _next;

        /// <summary>
        /// 处理每个http请求
        /// </summary>
        /// <param name="next">请求</param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var error = new ErrorModel
            {
                Code = 500,
                Message = $"资源服务器忙,请稍候再试,原因:{exception.Message}\n{exception.StackTrace}"
            };
            if (exception is UnauthorizeException)
            {
                error.Code = (int)HttpStatusCode.Unauthorized;
                error.Message = "未授权的访问(未登录或者登录已超时)";
            }
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = error.Code;
            log.Error(error.ToString());
            if (Configurations.DbContext.IsDevelopment)
            {
                Console.WriteLine(error.ToString());
            }
            return httpContext.Response.WriteAsync(error.ToString());
        }
    }
}
