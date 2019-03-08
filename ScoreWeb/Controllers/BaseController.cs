using ScoreWeb.App_Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreWeb.Controllers
{
    public class BaseController : Controller
    {
        //重写方法调用前事件
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string name = "";
            if (Session["LoginUser"] != null)
            {
                name = JsonSerializer.JsonToEntity<Users>(Session["LoginUser"].ToString()).Name;
            }
            else
            {
                if (Request.Cookies["LoginUser"] != null)
                {
                    name = JsonSerializer.JsonToEntity<Users>(Request.Cookies["LoginUser"].Value).Name;
                    Session["LoginUser"] = Request.Cookies["LoginUser"].Value;
                }
            }

            if (name == "")
                filterContext.Result = new RedirectResult("/Home/Login");
           
            
            base.OnActionExecuting(filterContext);
        }

    }
}
