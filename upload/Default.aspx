<%@ Page Title="文件上传" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="upload._Default" %>


<asp:Content ID="BodyContent"  ContentPlaceHolderID="MainContent" runat="server">
     
 
    <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True" Height="24px" Width="269px" />
    <asp:Button ID="upfile" runat="server" Text="上传" OnClick="upfile_Click" />
    <%--<input style="display: inline; width: 170px" id="excelFile" type="file" multiple="multiple" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/vnd.ms-excel" name="excelFile" required="required" onchange="upfile()">--%>
    <h3>已上传文件列表：</h3>   
    <div class="row" style="margin-top:20px;display:table">
    <%
        DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/upload"));
        var files = directory.EnumerateFiles();
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.Append("<ul style='list-style-type:decimal'>");
        foreach (var file in files.OrderByDescending(m=>m.CreationTime))
        {

            htmlBuilder.Append("<li><a href='/upload/upload/" + file.Name + "'>" + file.Name + "-------" + file.CreationTime + "</a></li>");
            var path = file.Directory.FullName + file.FullName;
  
        }

        var subDirecties = directory.GetDirectories();
        foreach (var item in subDirecties)
        {
            files = item.EnumerateFiles();
            foreach (var file in files.OrderByDescending(m=>m.CreationTime))
            {
                htmlBuilder.Append("<li><a href='/upload/upload/" + item.Name +"/"  + file.Name + "'>" + file.Name + "-------" + file.CreationTime + "</a></li>");
            }

        }


        htmlBuilder.Append("</ul>");
        Response.Write(htmlBuilder.ToString());

        %>
        </div>
    <div id="files"></div>
    <script>
        
         function upfile() {
            var fileObj = document.getElementById("excelFile").files; // js 获取文件对象
            if (typeof (fileObj) == "undefined" || fileObj.size <= 0) {
                alert("请选择Excel文件");
                return;
             }
             console.log(fileObj);
             var formFile = new FormData();
            
             formFile.append("action", "UploadVMKImagePath");
             for (var i = 0; i < fileObj.length; i++) {
               formFile.append("excelFile" + i, fileObj[i]); //加入文件对象        
             }
            
            var data = formFile;
            $.ajax({
                url: "Default",
                data: data,
                type: "Post",
                dataType: "text", //页面返回的数据类型
                cache: false,//上传文件无需缓存
                processData: false,//用于对data参数进行序列化处理 这里必须false
                contentType: false, //必须
                beforeSend: function () {

                    //$.messager.progress({

                    //    title: "提示",
                    //    msg: "加载中。。。。。。",


                    //});

                },
                success: function (result) {
                    //$('#box').datagrid({
                    //    url: "http://192.168.60.3/readexcel.ashx",
                    //});

                    //  console.log(result);
                    //$('#box').datagrid('loadData', JSON.parse(result));
                    //$.messager.progress('close');

                    alert("上传成功");

                },


            })
        };
    </script>

</asp:Content>
