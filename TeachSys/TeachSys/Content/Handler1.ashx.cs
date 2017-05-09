using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeachSys.Content
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string str = "[{'no':'1','name':'三国1'},{'no':2,'name':'演绎g'}]";
            str= str.Replace("'", "\"");
            context.Response.Write(str);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}