using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreWeb.App_Help
{
    public class Message
    {
            bool success;
            string msg;
            object data;
            public Message() { }
            public Message(bool success, string msg, object data)
            {
                this.success = success;
                this.msg = msg;
                this.data = data;
            }

            public bool Success
            {
                get { return success; }
                set { success = value; }
            }
            public string Msg
            {
                get { return msg; }
                set { msg = value; }
            }
            public object Data
            {
                get { return data; }
                set { data = value; }
            }
    }
}