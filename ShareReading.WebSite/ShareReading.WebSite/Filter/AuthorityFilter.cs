using System;
using System.Web;
using System.Web.Mvc;
using ShareReading.WebSite.Models;

namespace ShareReading.WebSite.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class AuthorityFilter : AuthorizeAttribute
    {
        /// <summary>
        /// 未登录时返还的地址
        /// </summary>
        private string _LoginPath = "";
        public AuthorityFilter()
        {
            this._LoginPath = "/User/Login";
        }

        public AuthorityFilter(string loginPath)
        {
            this._LoginPath = loginPath;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
|| filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;//表示支持控制器、action的AllowAnonymousAttribute
            }
            var sessionUser = HttpContext.Current.Session["CurrentUser"];//使用session
            //var memberValidation = HttpContext.Current.Request.Cookies.Get("CurrentUser");//使用cookie
            if (!(sessionUser is UserModel))
            {
                HttpContext.Current.Session["CurrentUrl"] = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectResult(this._LoginPath);
            }
        }
    }
}