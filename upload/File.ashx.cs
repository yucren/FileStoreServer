using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;

namespace upload
{
    /// <summary>
    /// File 的摘要说明
    /// </summary>
    public class File : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            if (action=="getFiles")
            {
                
                string searchValue = string.IsNullOrEmpty(context.Request["search"])?"": context.Request["search"];
                List<FileInfo> fileInfos = new List<FileInfo>();
                DirectoryInfo directory = new DirectoryInfo(context.Server.MapPath("~/upload"));
                var files = directory.EnumerateFiles();
                fileInfos.AddRange(files);
                var subDirecties = directory.GetDirectories();
                foreach (var item in subDirecties)
                {
                    files = item.EnumerateFiles();
                    fileInfos.AddRange(files);
                }
                var fileObjects = from file in fileInfos orderby file.CreationTime descending
                                  where  file.Name.ToUpper().Contains(searchValue.ToUpper())
                                  select new
                                  {
                                      file.Name,
                                      file.FullName,
                                      CreationTime= file.CreationTime.ToString("yyyy-MM-dd hh:mm:ss"),
                                      Length=  file.Length/1024 +"KB" 

                                  }; 
                context.Response.Write(JsonConvert.SerializeObject(fileObjects));
                context.Response.End();
            }
            else if (action=="uploadFile")
            {
                var errorBuilder = new StringBuilder();                
                    try
                    {

                        Dictionary<string, string> upfiles = new Dictionary<string, string>();

                        var files = context.Request.Files;
                        foreach (string item in files)
                        {
                            HttpPostedFile file = files[item];
                            if (file != null)
                            {

                            var fileName = Path.GetFileName(file.FileName);
                                file.SaveAs(context.Server.MapPath("~/upload/") + fileName);
                                upfiles.Add(fileName, file.FileName);                                
                            }
                        }                   
                    }
                    catch (Exception ex)
                    {
                        errorBuilder.AppendLine("发生错误");
                        errorBuilder.AppendLine(ex.Message);
                    }
                    context.Response.Write(errorBuilder.ToString());
                context.Response.End();
            }
            else if (action =="deleteFile")
            {
                string files = context.Request["files"];
                if (!string.IsNullOrEmpty(files))
                {
                    context.Response.Write(DeleteFiles(files, context));
                }
                

            }
        }

        public string DeleteFiles(string uploadfiles,HttpContext context)
        {
            var errorBuilder = new StringBuilder();

            var con = context.Request.ServerVariables["REMOTE_ADDR"];
            if (!string.IsNullOrEmpty(con) && con =="10.10.13.230")
            {
            try
            {
                var files = uploadfiles.Split(new char[] { ',' });
                foreach (var file in files)
                {
                    try
                    {

                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    if (System.IO.File.Exists(context.Server.MapPath("~/upload/" + file)))
                    {
                        System.IO.File.Delete(context.Server.MapPath("~/upload/" + file));

                    }
                    else
                    {
                        // var match = Regex.Match(file, @"(?<=\\)\S*?", RegexOptions.RightToLeft);
                        errorBuilder.AppendLine("未找到此文件:" + file);
                    }
                }
                

            }
            catch (Exception ex)
            {
                errorBuilder.AppendLine("发生错误" + ex.Message);
                
            }
            

            }
            else
            {
                errorBuilder.AppendLine("对不起你没有权限删除文件");
            }
            return errorBuilder.ToString();

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