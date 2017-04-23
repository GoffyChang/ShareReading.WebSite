
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Mvc;
using ShareReading.WebSite.Common;
using ShareReading.WebSite.Models;

namespace ShareReading.WebSite.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                UserManage.LoginResult result = HttpContext.UserLogin(user.Remember, user.UserName, user.PassWord,"");

                if (result == UserManage.LoginResult.Success)
                {
                    if (HttpContext.Session["CurrentUrl"] == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        string url = HttpContext.Session["CurrentUrl"].ToString();
                        HttpContext.Session["CurrentUrl"] = null;
                        return Redirect(url);
                    }
                }
                else
                {
                    ModelState.AddModelError("failed", result.GetRemark());
                    return View();
                }

            }
            else
            {
                return View();
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            return RedirectToAction("Index","Home");
        }
        /// <summary>
        /// 验证码  直接写入Response
        /// </summary>
        public void VerifyCode()
        {
            string code = "";
            Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out code);
            base.HttpContext.Session["CheckCode"] = code;
            bitmap.Save(base.Response.OutputStream, ImageFormat.Gif);
            base.Response.ContentType = "image/gif";
        }

    }
}