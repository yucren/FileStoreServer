using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace upload
{
    public partial class Home : Page
    {
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            
                //HttpPostedFile files = context.Request.Files["excelFile0"];
                //if (files != null)
                //{
                //    var fileName = new Random().Next(1000000) + Path.GetExtension(files.FileName);
                //    files.SaveAs(Server.MapPath("~/upload/") + fileName);
                //}
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
           



        }
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

        }
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
           


        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected void upfile_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFiles)
            {
                //指定上传文件在服务器上的保存路径
                string savePath = Server.MapPath("~/upload/");
                //检查服务器上是否存在这个物理路径，如果不存在则创建
                if (!System.IO.Directory.Exists(savePath))
                {
                    //需要注意的是，需要对这个物理路径有足够的权限，否则会报错
                    //另外，这个路径应该是在网站之下，而将网站部署在C盘却把文件保存在D盘
                    System.IO.Directory.CreateDirectory(savePath);
                }
               // savePath = savePath + "\\" + FileUpload1.FileName;
                foreach (var item in FileUpload1.PostedFiles)
                {
                    item.SaveAs(savePath + "\\" + item.FileName);
                }

                
              //  FileUpload1.SaveAs(savePath);//保存文件
                //不过需要注意的是，在客户端访问却需要指定的是URL地址，而不是服务器上的物理地址
                Response.Write("<script>alert('上传成功')</script>");
            }
        }

       
    }
}