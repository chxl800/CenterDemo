using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ScoreWeb.App_Help
{
    public class DBHelp
    {

        private static string dbconfig = System.Configuration.ConfigurationManager.AppSettings["MysqlDB"].ToString();

        public static List<T> GetSelect<T>(string sql)
        {
            ///////////////////获取MYSQ看数据返回值////////////////////////////  
            //连接字符串拼装  
            //myconn = new MySqlConnection("Host=127.0.0.1;UserName=root;Password=root;Database=score;Port=3306"); 
            MySqlConnection myconn = new MySqlConnection(dbconfig);
            //连接  
            myconn.Open();
            //查询命令赋值，可以写多条语句，多条语句之间用;号隔开  
            MySqlCommand mycom = new MySqlCommand(sql, myconn);
            MySqlDataReader myrec = mycom.ExecuteReader();

            List<T> list = new List<T>();
            //一次次读，读不到就结束  
            while (myrec.Read())
            {
                T obj = ExecDataReader<T>(myrec);
                list.Add(obj);
                //string   myInfo = myInfo + myrec["Name"] + " " + myrec["ID"];
            }
            //////关闭相关对象  
            myrec.Close();
            mycom.Dispose();
            myconn.Close();

            return list;

        }
        public static string GetSelectStr(string sql)
        {
            ///////////////////获取MYSQ看数据返回值////////////////////////////  
            //连接字符串拼装  
            //myconn = new MySqlConnection("Host=127.0.0.1;UserName=root;Password=root;Database=score;Port=3306"); 
            MySqlConnection myconn = new MySqlConnection(dbconfig);
            //连接  
            myconn.Open();
            //查询命令赋值，可以写多条语句，多条语句之间用;号隔开  
            MySqlCommand mycom = new MySqlCommand(sql, myconn);
            object obj  = mycom.ExecuteScalar();
            //////关闭相关对象  
            mycom.Dispose();
            myconn.Close();
            return obj.ToString();

        }
        //操作
        public static int ExecutSql(string sql)
        {
            int result = 0;
            MySqlConnection mysqlcon = new MySqlConnection(dbconfig);
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(sql, mysqlcon);
            result= mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();
            mysqlcon.Close();
            mysqlcon.Dispose();
            return result;


        }

         
        private static T ExecDataReader<T>(IDataReader reader)
        {
            T obj = default(T);
            try
            {
                Type type = typeof(T);
                obj = (T)Activator.CreateInstance(type);//从当前程序集里面通过反射的方式创建指定类型的对象   
                PropertyInfo[] propertyInfos = type.GetProperties();//获取指定类型里面的所有属性
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string fieldName = reader.GetName(i);
                        if (fieldName.ToLower() == propertyInfo.Name.ToLower())
                        {
                            //object val = reader[propertyInfo.Name];//读取表中某一条记录里面的某一列
                            object val = reader[fieldName];//读取表中某一条记录里面的某一列
                            if (val != null && val != DBNull.Value)
                            {
                                propertyInfo.SetValue(obj, val);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return obj;
        }


    }
}