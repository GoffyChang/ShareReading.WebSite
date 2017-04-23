using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareReading.WebSite.Models;

namespace ShareReading.WebSite.Filter
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class LogExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)//异常有没有被处理过
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())//检查请求头
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new AjaxResult()
                        {
                            Result = DoResult.Failed,
                            PromptMsg = "系统出现异常，请联系管理员",
                            DebugMessage = filterContext.Exception.Message
                        }
                    };
                }
                else
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        ViewData = new ViewDataDictionary<string>(filterContext.Exception.Message)
                    };
                }
                filterContext.ExceptionHandled = true;
            }
        }
    }
}