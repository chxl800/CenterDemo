using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ScoreWeb.App_Help
{
    public class JsonSerializer
    {
        //初始化序列化对象。
        private readonly static JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

        /// <summary>
        /// object对象转换为将json字符串。
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        public static string ToJson(object pObj)
        {
            return javaScriptSerializer.Serialize(pObj);
        }

        /// <summary>
        /// 将json字符串转换为object对象。
        /// </summary>
        /// <param name="jsonStr">json字符串。</param>
        /// <returns>对象集合。</returns>
        public static List<T> JsonToObject<T>(string jsonStr)
        {
            object obj = javaScriptSerializer.DeserializeObject(jsonStr);
            return javaScriptSerializer.ConvertToType<List<T>>(obj);
        }

        /// <summary>
        /// 将json字符串转换为object对象。
        /// </summary>
        /// <param name="jsonStr">json字符串。</param>
        /// <returns>对象集合。</returns>
        public static T JsonToEntity<T>(string jsonStr)
        {
            object obj = javaScriptSerializer.DeserializeObject(jsonStr);
            return javaScriptSerializer.ConvertToType<T>(obj);
        }
    }
}