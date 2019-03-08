using ScoreWeb.App_Help;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ScoreService ScoreSer = new ScoreService();
            List<ScoreType> list = ScoreSer.GetTypeList();
            List<SelectListItem> SelList = new List<SelectListItem>();
            SelList.Add(new SelectListItem
            {
                Text = "请选择考试类型",
                Value = ""
            });
            foreach(ScoreType item in list)
            {
                SelList.Add(new SelectListItem
                {
                    Text = item.TypeName,
                    Value = item.ID.ToString()
                });
            }
            ViewData["SelList"] = new SelectList(SelList, "Value", "Text", "");
            return View();
        }

        public JsonResult GetList(string TypeName, string Name, string Number, string Code)
        {

            try {
              
                if (string.IsNullOrEmpty(TypeName))
                {
                    throw new Exception("考试类型不能为空！");
                }
                if (string.IsNullOrEmpty(Name))
                {
                    throw new Exception("姓名不能为空！");
                }
                if (string.IsNullOrEmpty(Number))
                {
                    throw new Exception("准考证号不能为空！");
                }
                if (string.IsNullOrEmpty(Code))
                {
                    throw new Exception("验证码不能为空！");
                }
                if(Code.ToLower()!=Session["ValidateCode"].ToString().ToLower())
                {
                    throw new Exception("验证码不正确！");

                }
                ScoreService ScoreSer = new ScoreService();
                List<Score> list = ScoreSer.GetListWhere(TypeName, Name, Number);
                Message msg = new Message(true,"",list);  
                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Message msg = new Message(false, ex.Message, "");  
                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

 
        /// <summary>
        /// 显示验证码图片
        /// </summary>
        /// <returns></returns>
        public void ShowCode()
        {
            string validateCode = CreateRandomCode(4);
            Session["ValidateCode"] = validateCode;
            CreateImage(validateCode);  
        }


        public string charSet = "2,3,4,5,6,8,9,A,B,C,D,E,F,G,H,J,K,M,N,P,R,S,U,W,X,Y";
        /// <summary>   
        /// 生成验证码         
        /// <param name="n">位数</param>  
        /// <returns>验证码字符串</returns>  
        private string CreateRandomCode(int n)
        {
            string[] CharArray = charSet.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(CharArray.Length - 1);
                if (temp == t)
                {
                    return CreateRandomCode(n);
                } temp = t;
                randomCode += CharArray[t];
            }
            return randomCode;
        }
        private void CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 13);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 23);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Bold));        // 前景色       
            Brush b = new System.Drawing.SolidBrush(Color.Black);        // 背景色      
            g.Clear(Color.White);        // 填充文字       
            g.DrawString(checkCode, f, b, 0, 1);        // 随机线条       
            Pen linePen = new Pen(Color.Gray, 0); Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                int x1 = rand.Next(image.Width);
                int y1 = rand.Next(image.Height);
                int x2 = rand.Next(image.Width);
                int y2 = rand.Next(image.Height);
                g.DrawLine(linePen, x1, y1, x2, y2);
            }
            // 随机点       
            for (int i = 0; i < 30; i++)
            {
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);
                image.SetPixel(x, y, Color.Gray);
            }
            // 边框      
            g.DrawRectangle(new Pen(Color.Gray), 0, 0, image.Width - 1, image.Height - 1);        // 输出图片 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }




      ////////////////////////////////////////////////////////登录
        public ActionResult Login()
        {
            return View();
        }

        //登录
        public JsonResult LoginSubmit(string Name,string Pass)
        {
            try
            {
                UsersService ScoreSer = new UsersService();
                List<Users> list = ScoreSer.GetUsers(Name);
                Message msg = new Message(false, "登录成功！", "");
                if (list != null && list.Count>0)
                {

                    int uk = 0;
                    Users user = new Users();
                    foreach (Users entity in list)
                    {
                        if (entity.Pass == ScoreSer.GetMd5Str(Pass)) //检查密码
                        {
                            uk = 1;
                            user = entity;
                            break;
                        }
                    }


                    if (uk == 0)
                    {
                        msg.Msg = "密码不正确!";
                    }
                    else
                    {
                        msg.Success = true;
                        //保存在session 和Cookie
                        string json = JsonSerializer.ToJson(user);
                        Response.Cookies["LoginUser"].Value = json;
                        Response.Cookies["LoginUser"].Expires = System.DateTime.Now.AddDays(1); //设置过期时间

                        Session["LoginUser"] = json;
                    }
             
                }
                else
                {
                    msg.Msg = "用户名不正确!";
                }
                 
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
