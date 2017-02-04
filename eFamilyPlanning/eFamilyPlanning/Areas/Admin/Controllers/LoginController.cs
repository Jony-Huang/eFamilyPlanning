using eFamilyPlanning.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace eFamilyPlanning.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private UnitRepository unitRepository = new UnitRepository();


        public string MvcStringAjax(FormCollection fc)
        {
            string username = fc["username"];
            //var json = Json(unitRepository.LoginRepository.GetMenuAll());
            var json = JsonConvert.SerializeObject(unitRepository.LoginRepository.GetMenuAll());
            return json;
        }
        public JsonResult MvcJsonResultAjax(FormCollection fc)
        {
            string username = fc["username"];
            var json = Json(unitRepository.LoginRepository.GetMenu(username));
            return json;
        }

        public ActionResult WelCome()
        {
            return View();
        }

        public ActionResult Index3()
        {
            return View();
        }

        
        public ActionResult Index2()
        {
            return View();
        }

        // GET: Admin/Login
        [RoleAuthorize]
        public ActionResult Index() //后台管理首页
        {
            return View();
        }
        public ActionResult Login() //登录页面
        {
            var returnUrl = Request["returnUrl"];
            if (HttpContext.User.Identity.IsAuthenticated)
            {

                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/Admin/Login/Index");
                }
                
            }
            TempData["returnUrl"] = Convert.ToString(returnUrl);
            return View();
        }

        public int LoginSubmit(FormCollection fc)
        {
            //1 获取表单数据
            string username = fc["username"];
            string password = fc["password"];
            var returnUrl = Convert.ToString(TempData["returnUrl"]);
            var result = 0;

            //2 验证用户

            //var user = db.User.Where(u => u.Name == name && u.PassWord == pwd).FirstOrDefault();
            var user = unitRepository.LoginRepository.Get(filter: u => u.Name == username && u.PassWord == password).FirstOrDefault();
            unitRepository.Dispose();
            if (user != null)
            {
                //1、创建认证信息 Ticket 
                //使用FormsAuthentication.Encrypt 加密票据。
                //把加密后的Ticket 存储在Response Cookie中(客户端js不需要读取到这个Cookie，所以最好设置HttpOnly=True，防止浏览器攻击窃取、伪造Cookie)。这样下次可以从Request Cookie中读取了。
                //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,uname,DateTime.Now,DateTime.Now.AddMinutes(20),true, "7,1,8", "/" );
                //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                //cookie.HttpOnly = true;
                //HttpContext.Response.Cookies.Add(cookie);

                //2、获取认证信息
                // 登录后，在内容页，我们可以通过，当前请求的User.Identity.Name 获取到uname信息，也可以通过读取Request 中的Cookie 解密，获取到Ticket，再从其中获取uname 和 userData （也就是之前存储的角色ID信息）。
                //ViewData["user"] = User.Identity.Name;

                //var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                //var ticket2 = FormsAuthentication.Decrypt(cookie.Value);
                //string role = ticket.UserData;


                //FormsAuthentication.SetAuthCookie(name, true); //48小时有效
                FormsAuthentication.SetAuthCookie(username, false); //会话cookie
                //if (!string.IsNullOrWhiteSpace(returnUrl))
                //{
                //    return Redirect(returnUrl);
                //}
                //else
                //{
                //    return Redirect("/Admin/Login/Index");
                //}
                result = 1;
            }
            else
            {
                TempData["returnUrl"] = returnUrl;
                result = 0;
            }
            return result;
            //return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}