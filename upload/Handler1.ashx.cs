using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace upload
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //  context.Response.ContentType = "text/plain";
            //   context.Response.Write("Hello World");
            foreach (string  item in context.Request.Files)
            {
                HttpPostedFile httpPostedFile = context.Request.Files[item];
                
                if (httpPostedFile != null)
                {
                    var fileName = httpPostedFile.FileName;
                    httpPostedFile.SaveAs(context.Server.MapPath("~/upload/") + fileName);

                }
           
         //       context.Response.Write(ReadExcelAll(context.Server.MapPath("~/ExcelFiles/") + fileName));
            }
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