using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ShareReading.WebSite.Models;

namespace ShareReading.WebSite.Common
{
    public static class UserManage
    {
        public enum LoginResult
        {
            /// <summary>
            /// 登录成功
            /// </summary>
            [EnumExtension.RemarkAttribute("登录成功")]
            Success = 0,
            /// <summary>
            /// 用户不存在
            /// </summary>
            [EnumExtension.RemarkAttribute("用户不存在")]
            NoUser = 1,
            /// <summary>
            /// 密码错误
            /// </summary>
            [EnumExtension.RemarkAttribute("密码错误")]
            WrongPwd = 2,
            /// <summary>
            /// 验证码错误
            /// </summary>
            [EnumExtension.RemarkAttribute("验证码错误")]
            WrongVerify = 3,
            /// <summary>
            /// 账号被冻结
            /// </summary>
            [EnumExtension.RemarkAttribute("账号被冻结")]
            Frozen = 4
        }
        public static LoginResult UserLogin(this HttpContextBase context, bool remeber, string name = "", string pwd = "",string verify = "")
        {
            if (string.IsNullOrEmpty(verify) || context.Session["CheckCode"] == null || !context.Session["CheckCode"].ToString().Equals(verify, StringComparison.OrdinalIgnoreCase))
            {
                return LoginResult.WrongVerify;
            }
            var user = new UserModel();//todo 数据库查询用户根据用户登录名
            if (user == null)
            {
                return LoginResult.NoUser;
            }
            else if (!user.PassWord.Equals(MD5Encrypt.Encrypt(pwd)))
            {
                return LoginResult.WrongPwd;
            }
            else if (user.State == (int)CommonEnum.UserState.Frozen)
            {
                return LoginResult.Frozen;
            }
            else
            {
                #region Cookie
                UserModel currentUser = new UserModel()
                {
                    UserName = user.UserName,
                    UserEmail =  user.UserEmail,
                    PassWord = user.PassWord,
                    UserTel = user.UserTel,
                };
                //HttpCookie cookie = context.Request.Cookies.Get("CurrentUser");
                //if (cookie == null)
                //{
                if (remeber)
                {
                    HttpCookie myCookie = new HttpCookie("CurrentUser");
                    myCookie.Value = JsonConvert.SerializeObject(currentUser);
                    myCookie.Expires = DateTime.Now.AddDays(7);
                    context.Response.Cookies.Add(myCookie);
                }
                //}
                #endregion Cookie

                #region Session
                //context.Session.RemoveAll();
                var sessionUser = context.Session["CurrentUser"];

                context.Session["CurrentUser"] = currentUser;
                context.Session.Timeout = 3;//minute  session过期等于Abandon
                #endregion Session
                return LoginResult.Success;
            }
        }
        public static void UserLogout(this HttpContextBase context)
        {
            #region Cookie
            HttpCookie myCookie = context.Request.Cookies["CurrentUser"];
            if (myCookie != null)
            {
                myCookie.Expires = DateTime.Now.AddMinutes(-1);//设置过过期
                context.Response.Cookies.Add(myCookie);
            }

            #endregion Cookie

            #region Session
            var sessionUser = context.Session["CurrentUser"];
            context.Session["CurrentUser"] = null;//表示将制定的键的值清空，并释放掉，
            context.Session.Remove("CurrentUser");
            context.Session.Clear();//表示将会话中所有的session的键值都清空，但是session还是依然存在，
            context.Session.RemoveAll();//
            context.Session.Abandon();//就是把当前Session对象删除了，下一次就是新的Session了   
            #endregion Session
        }
    }
}