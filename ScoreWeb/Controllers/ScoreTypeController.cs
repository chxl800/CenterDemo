using ScoreWeb.App_Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreWeb.Controllers
{
    public class ScoreTypeController : BaseController
    {
        //
        // GET: /ScoreType/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetList()
        {
            try
            {
                ScoreService ScoreSer = new ScoreService();
                List<ScoreType> list = ScoreSer.GetTypeList();
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
        public JsonResult SaveType(string ID, string TypeName)
        {

            try
            {
                ScoreService ScoreSer = new ScoreService();
                int result = ScoreSer.SaveType(ID,TypeName);
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
        public JsonResult DeleType(string ID)
        {
            try
            {
                ScoreService ScoreSer = new ScoreService();
                int result = ScoreSer.DeleType(ID);
                Message msg = new Message(result > 0, "删除成功！", "");
                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Message msg = new Message(false, ex.Message, "");
                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        

    }
}
