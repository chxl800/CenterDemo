using ScoreWeb.App_Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreWeb.Controllers
{
    public class UsersController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetList()
        {
            try
            {
                UsersService ScoreSer = new UsersService();
                List<Users> list = ScoreSer.GetUsersList();
                Message msg = new Message(true, "", list);
                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Message msg = new Message(false, ex.Message, "");
                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        //保存考试类型
        public JsonResult SaveUsers(string ID, string Name, string Pass)
        {

            try
            {
                UsersService ScoreSer = new UsersService();
                int result = ScoreSer.SaveUsers(ID, Name, Pass);
                Message msg = new Message(result > 0, "保存成功！", "");
                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Message msg = new Message(false, ex.Message, "");
                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        //删除考试类型
        public JsonResult DeleUsers(string ID)
        {
            try
            {
                UsersService ScoreSer = new UsersService();
                int result = ScoreSer.DeleUsers(ID);
                Message msg = new Message(result > 0, "删除成功！", "");
                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Message msg = new Message(false, ex.Message, "");
                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


        //Logout退出
        public ActionResult Logout()
        {
            Session["LoginUser"] = null;
            Response.Cookies["LoginUser"].Value = null;
            Response.Cookies["LoginUser"].Expires = System.DateTime.Now.AddDays(-1);
            return RedirectToAction("Login","Home");
        }

        //Logout退出
        public string GetName()
        {
            string name="";
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
            return name;
        }

    }
}
