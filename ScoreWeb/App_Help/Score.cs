using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreWeb.App_Help
{
    public class Score
    {
        public int ID { set; get; }
        public int TypeID { set; get; }
        public string TypeName { set; get; }
        public string Number { set; get; }
        public string Name { set; get; }
        public string Sex { set; get; }
        public string Birth { set; get; }
        public decimal Grade { set; get; }
        public string Unit { set; get; }
        public string Job { set; get; }
        public string Phone { set; get; }
        public DateTime ValidTime { set; get; }
        public DateTime CreatTime { set; get; }
        public string Creator { set; get; }
        public int Count { set; get; }
        public int Pages { set; get; }
    }
}