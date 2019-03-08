using Aspose.Cells;
using ScoreWeb.App_Help;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace ScoreWeb.Controllers
{
    public class ScoreController : BaseController
    {
        //
        // GET: /Score/

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
            foreach (ScoreType item in list)
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

        public JsonResult GetPageList(int PageSize, int PageCurr, string TypeName, string Name, string Number, string ValidTimeStra, string ValidTimeEnd)
        {
            try
            {
                int Count = 0;
                int Pages = 0;
                ScoreService ScoreSer = new ScoreService();
                List<Score> list = ScoreSer.GetListPageWhere(PageSize, PageCurr, TypeName, Name, Number, ValidTimeStra, ValidTimeEnd, ref Count, ref Pages);
                Message msg = new Message(true, Count + "|" + Pages, list);

                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Message msg = new Message(false, ex.Message, "");
                return new JsonResult() { Data = JsonSerializer.ToJson(msg), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
   

       

        //导入
        public ActionResult Import()
        {
            ScoreService ScoreSer = new ScoreService();
            List<ScoreType> list = ScoreSer.GetTypeList();
            List<SelectListItem> SelList = new List<SelectListItem>();
            SelList.Add(new SelectListItem
            {
                Text = "请选择考试类型",
                Value = ""
            });
            foreach (ScoreType item in list)
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

        //上传
        [ValidateInput(false)]
        public ActionResult Upload(string TypeName, DateTime ValidTime)
        {
            try
            {
                string path = Server.MapPath("/Upload/");
                HttpPostedFileBase file = Request.Files["files"];
                if (file != null && file.ContentLength > 0)
                {

                    if (!Directory.Exists(path)) //判断文件夹是否存在
                    {
                        Directory.CreateDirectory(path); //不存在则创建
                    }
                    string NewFileName = DateTime.Now.ToString("yyyyMMddhhmmss");
                    path = Path.Combine(path, NewFileName + Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                }

                //返回的Excel数据
                DataSet dsExcel = new DataSet();
                //创建一个Workbook和Worksheet对象
                Worksheet wkSheet = null;
                Workbook wkBook = new Workbook();
                //加载文件
                wkBook.Open(path);

                //遍历读取sheet
                for (int i = 0; i < wkBook.Worksheets.Count; i++)
                {
                    wkSheet = wkBook.Worksheets[i];

                    //声明DataTable存放sheet
                    DataTable dtTemp = new DataTable();
                    //设置Table名为sheet的名称
                    dtTemp.TableName = wkSheet.Name;
                    //遍历行
                    for (int x = 0; x < wkSheet.Cells.MaxDataRow + 1; x++)
                    {
                        //声明DataRow存放sheet的数据行
                        DataRow dRow = null;
                        //遍历列
                        for (int y = 0; y < wkSheet.Cells.MaxDataColumn + 1; y++)
                        {
                            //获取单元格的值
                            string value = wkSheet.Cells[x, y].StringValue.Trim();
                            //如果是第一行，则当作标题
                            if (x == 0)
                            {
                                continue;
                            }
                            //如果是第二行，则当作表头
                            else if (x == 1)
                            {
                                //设置表头
                                DataColumn dCol = new DataColumn(value);
                                dtTemp.Columns.Add(dCol);
                            }
                            //非第二行，则为数据行
                            else
                            {
                                //每次循环到第一列时，实例DataRow
                                if (y == 0)
                                {
                                    dRow = dtTemp.NewRow();
                                }
                                //给第Y列赋值
                                dRow[y] = value;
                            }
                        }
                        if (dRow != null)
                        {
                            dtTemp.Rows.Add(dRow);
                        }
                    }
                    dsExcel.Tables.Add(dtTemp);
                }

                //释放对象
                wkSheet = null;
                wkBook = null;

                DateTime CreatTime = DateTime.Now;
                StringBuilder sb = new StringBuilder();
                //插入数据库
                if(dsExcel!=null && dsExcel.Tables.Count>0)
                {
                    for(int i=0 ; i<dsExcel.Tables.Count;i++)
                    {
                        if (dsExcel.Tables[i] != null && dsExcel.Tables[i].Rows.Count > 0)
                        {
                            foreach(DataRowView dr in dsExcel.Tables[i].DefaultView)
                            {
                                //序号	准考证号	姓名	性别	出生年月	成绩	单位	职务	联系方式
                                if (string.IsNullOrEmpty(dr["准考证号"].ToString()) || string.IsNullOrEmpty(dr["姓名"].ToString()))
                                    continue;
                                string sql = "insert into score(TypeID,TypeName,Number,Name,Sex,Birth,Grade,Unit,Job,Phone,ValidTime,CreatTime,Creator) values(" + TypeName + ",'','" + dr["准考证号"] + "','" + dr["姓名"] + "','" + dr["性别"] + "','" + dr["出生年月"] + "'," + isNumberic(dr["成绩"].ToString()) + ",'" + dr["单位"] + "','" + dr["职务"] + "','" + dr["联系方式"] + "','" + ValidTime + "','" + CreatTime + "','');";
                                sb.Append(sql);
                            }
                        }
                    }

                }
                //插入数据库
                if(!string.IsNullOrEmpty(sb.ToString()))
                {
                    DBHelp.ExecutSql(sb.ToString());
                }

            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
            
        }
        public JsonResult GetImport()
        {
            return new JsonResult() { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //判断是否数字型
        protected decimal isNumberic(string result)
        {
            decimal number = -1;
            if(string.IsNullOrEmpty(result))
                return number;
            System.Text.RegularExpressions.Regex rex =new System.Text.RegularExpressions.Regex(@"^[+-]?\d*[.]?\d*$");
            if (rex.IsMatch(result))
            {
               number = decimal.Parse(result);
            }
            return number;
        }


        /// <summary>
        /// 批量修改有效日期
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ValidTime"></param>
        /// <returns></returns>
        public JsonResult SaveValidTime(string ID, string ValidTime)
        {

            try
            {
                ScoreService ScoreSer = new ScoreService();
                int result = ScoreSer.SaveValidTime(ID, ValidTime);
                Message msg = new Message(result > 0, "保存成功！", "");
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
