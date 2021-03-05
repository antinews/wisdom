using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Wisdom.Webapi.Extensions.CustomException.Models;
using Wisdom.Webapi.Models.Response;

namespace Wisdom.Webapi.Extensions.CustomException
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(GlobalExceptionFilter));
        /// <summary>
        /// 发生异常时进入
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            log.Error(context.Exception);
            if (context.ExceptionHandled == false)
            {
                var response = new ResponseModel();
                response.SetError(context.Exception.Message);
                context.Result = new JsonResult(response);
            }
            context.ExceptionHandled = true;
        }
        /// <summary>
        /// 异步发生异常时进入
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context) // Task开头的控制器方法
        {
            OnException(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Action 执行后拦截
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        /// <summary>
        /// Action 执行前拦截[模型验证应该在此处先处理]
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.HttpContext.Response.Headers["Access-Control-Allow-Origin"] = "*";//解决拦截器添加后跨域不生效的问题
            //if (!context.ModelState.IsValid)//验证参数的合法性问题。返回错误信息
            //{
            //    ///模型有效性验证失败处理逻辑...比如将提示信息返回
            //    StringBuilder stringBuilder = new StringBuilder();
            //    ///模型有效性验证失败处理逻辑.....
            //    ///如返回错误信息,这里可自动包装
            //    foreach (var item in context.ModelState.Values)
            //    {
            //        foreach (var error in item.Errors)
            //        {
            //            stringBuilder.Append($"{ error.ErrorMessage}|");
            //        }
            //    }

            //    context.Result = new ContentResult
            //    {
            //        Content = stringBuilder.ToString(),
            //        StatusCode = StatusCodes.Status200OK,
            //        ContentType = "text/html;charset=utf-8"
            //    };
            //}

        }
    }
}
