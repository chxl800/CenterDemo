using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ScoreWeb.App_Help
{
    public class UsersService
    {
        /// <summary>
        /// 用户
        /// </summary>
        /// <returns></returns>
        public List<Users> GetUsersList()
        {
            string sql = "SELECT * FROM Users where Isdele=0 order by ID desc";
            List<Users> list = DBHelp.GetSelect<Users>(sql);
            return list;
        }
        /// <summary>
        /// 用户
        /// </summary>
        /// <returns></returns>
        public List<Users> GetUsers(string Name)
        {
            string sql = "SELECT * FROM Users where Isdele=0 and Name='" + Name + "' order by ID desc";
            List<Users> list = DBHelp.GetSelect<Users>(sql);
            return list;
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <returns></returns>
        public int SaveUsers(string ID, string Name, string Pass)
        {

            string sql = "";
            Pass = GetMd5Str(Pass);
            if (string.IsNullOrEmpty(ID))
            {
                sql = "insert into Users(Name,Pass,Isdele) values('" + Name + "','" + Pass + "',0)";
            }
            else
            {
                sql = "update Users set Name='" + Name + "',Pass='" + Pass + "' where ID=" + ID + "";
            }

            return DBHelp.ExecutSql(sql);
        }

        /// <summary>
        /// del用户
        /// </summary>
        /// <returns></returns>
        public int DeleUsers(string ID)
        {
            string sql = "update Users set Isdele=1 where ID=" + ID + "";
            return DBHelp.ExecutSql(sql);
        }

        /// <summary>
        /// MD5 16位加密
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public string GetMd5Str(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }




    }
}