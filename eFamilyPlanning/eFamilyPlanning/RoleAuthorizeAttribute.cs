using eFamilyPlanning.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eFamilyPlanning
{
 
 public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private UnitRepository unitRepository = new UnitRepository();
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var isAuth = false;

            if (!filterContext.RequestContext.HttpContext.Request.IsAuthenticated) //未登录
            {
                isAuth = false;
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", Action = "Login", returnUrl = filterContext.HttpContext.Request.Url, returnMessage = "您未登录" }));
                return;
            }
            else //已经登录
            {
                if (filterContext.RequestContext.HttpContext.User.Identity != null) //已经登录且已经标识
                {
                    var name = filterContext.HttpContext.User.Identity.Name; //当前用户的名称
                    if (name == "admin")
                    {
                        isAuth = true;
                    }
                    else
                    {
                        var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                        var actionName = filterContext.ActionDescriptor.ActionName;

                        //获取当前用户的所有权限
                        var menu = unitRepository.LoginRepository.GetMenu(name);
                        unitRepository.Dispose();
                        if (menu != null)
                        {
                            isAuth = menu.Any(m => m.Controller == controllerName && m.Action == actionName);
                        }
                    }
                }

            }

            if (!isAuth)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", Action = "Index", returnUrl = filterContext.HttpContext.Request.Url, returnMessage = "您无权查看" }));
                return;
            }
            else
            {
                base.OnAuthorization(filterContext);
            }

            //base.OnAuthorization(filterContext);
        }
    }
}