using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreWeb.App_Help
{
    public class ScoreService
    {
        public List<Score> GetList()
        {
            string sql = "SELECT s.ID,s.TypeID,s.Number,s.Name,s.Sex,s.Birth,s.Grade,s.Unit,s.Job,s.Phone,s.ValidTime,s.CreatTime,s.Creator,t.TypeName FROM score s inner join scoretype t on t.ID=s.TypeID where t.isdele=0 order by s.ID desc";
            List<Score> list = DBHelp.GetSelect<Score>(sql);
            return list;
        }

        public List<Score> GetListPageWhere(int PageSize, int PageCurr, string TypeName, string Name, string Number, string ValidTimeStra, string ValidTimeEnd, ref int Count, ref int Pages)
        {
            try
            {
                string sqlwhere = "";
                if (!string.IsNullOrEmpty(TypeName))
                {
                    sqlwhere += " and s.TypeID='" + TypeName + "'";
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    sqlwhere += " and s.Name like '%" + Name + "%'";
                }
                if (!string.IsNullOrEmpty(Number))
                {
                    sqlwhere += " and s.Number like '%" + Number + "%'";
                }
                if (!string.IsNullOrEmpty(ValidTimeStra))
                {
                    sqlwhere += " and s.ValidTime >= '" + ValidTimeStra + "'";
                }
                if (!string.IsNullOrEmpty(ValidTimeEnd))
                {
                    sqlwhere += " and s.ValidTime <= '" + ValidTimeEnd + "'";
                }
                string whe = string.Format(@"SELECT count(0) FROM score s inner join scoretype t on t.ID=s.TypeID where t.isdele=0 {0} order by s.ID desc", sqlwhere);
                string CountRows = DBHelp.GetSelectStr(whe);

                Count = Int32.Parse(CountRows);
                Pages = Int32.Parse(CountRows) % PageSize==0 ? Int32.Parse(CountRows) % PageSize :  Int32.Parse(CountRows) / PageSize +1;

                string sql = string.Format(@"SELECT s.ID,s.TypeID,s.Number,s.Name,s.Sex,s.Birth,s.Grade,s.Unit,s.Job,s.Phone,s.ValidTime,s.CreatTime,s.Creator,t.TypeName FROM score s inner join scoretype t on t.ID=s.TypeID where t.isdele=0 {0} order by s.ID desc limit {1}, {2}", sqlwhere, (PageSize * (PageCurr-1)), PageSize);
                List<Score> list = DBHelp.GetSelect<Score>(sql);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 前台查询考试结果
        /// </summary>
        /// <param name="TypeName"></param>
        /// <param name="Name"></param>
        /// <param name="Number"></param>
        /// <returns></returns>
        public List<Score> GetListWhere(string TypeName, string Name, string Number)
        {
            string sqlwhere="";
            if(!string.IsNullOrEmpty(TypeName))
            {
                sqlwhere += " and s.TypeID='" + TypeName + "'";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                sqlwhere += " and s.Name='" + Name + "'";
            }
            if (!string.IsNullOrEmpty(Number))
            {
                sqlwhere += " and s.Number='" + Number + "'";
            }
            string VTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //有效日期
            string sql = string.Format(@"SELECT s.ID,s.TypeID,s.Number,s.Name,s.Sex,s.Birth,s.Grade,s.Unit,s.Job,s.Phone,s.ValidTime,s.CreatTime,s.Creator,t.TypeName FROM score s inner join scoretype t on t.ID=s.TypeID where t.isdele=0 and s.ValidTime>='{1}' {0} order by s.ID desc", sqlwhere,VTime);
            List<Score> list = DBHelp.GetSelect<Score>(sql);
            return list;
        }

        /// <summary>
        /// / 批量修改有效日期
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ValidTime"></param>
        /// <returns></returns>
        public int SaveValidTime(string ID, string ValidTime)
        {
            string sql = string.Format("update score set ValidTime='{0}' where ID in ({1})", ValidTime, ID);
            return DBHelp.ExecutSql(sql);
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 考试类型
        /// </summary>
        /// <returns></returns>
        public List<ScoreType> GetTypeList()
        {
            string sql = "SELECT * FROM scoretype where Isdele=0 order by ID desc";
            List<ScoreType> list = DBHelp.GetSelect<ScoreType>(sql);
            return list;
        }

        /// <summary>
        /// 保存考试类型
        /// </summary>
        /// <returns></returns>
        public int SaveType(string ID,string TypeName)
        {

            string sql = "";
            if (string.IsNullOrEmpty(ID))
            {
                sql = "insert into scoretype(TypeName,Isdele) values('" + TypeName + "',0)";
            }
            else
            {
                sql = "update scoretype set TypeName='" + TypeName + "' where ID=" + ID + "";
            }

            return DBHelp.ExecutSql(sql);
        }

        /// <summary>
        /// 考试类型
        /// </summary>
        /// <returns></returns>
        public int DeleType(string ID)
        {
            string sql = "update scoretype set Isdele=1 where ID=" + ID + "";
            return DBHelp.ExecutSql(sql);
        }


    }
}