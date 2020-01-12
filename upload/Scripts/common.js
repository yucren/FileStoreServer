function uploadFile(id) {

    var fileObjs = document.getElementById(id).files; // js 获取文件对象
    if (typeof (fileObjs) === undefined || fileObjs.size <= 0) {
        alert("请选择文件后再单击上传按钮");
        return;
    }
    var formFile = new FormData();
    formFile.append("action", "UploadVMKImagePath");
    for (var i = 0; i < fileObjs.length; i++) {
        formFile.append("files" + i, fileObjs[i]); //加入文件对象
    }

    var data = formFile;
    $.ajax({
        url: 'file.ashx?action=uploadFile',
        data: data,
        type: "Post",
        dataType: "text",
        cache: false,//上传文件无需缓存
        processData: false,//用于对data参数进行序列化处理 这里必须false
        contentType: false, //必须
        xhr: function () { //获取ajaxSettings中的xhr对象。
            var xhr = $.ajaxSettings.xhr();
            if (xhr.upload) {
                xhr.upload.addEventListener("progress", function (e) {
                    var loaded = e.loaded;//已经上传大小情况
                    var tot = e.total;//附件总大小
                    var per = Math.floor(100 * loaded / tot);  //已经上传的百分比                     
                    //console.log('附件总大小 = ' + loaded);
                    //console.log('已经上传大小 = ' + tot);                    

                    app.progressValue = per;

                    //$('#uploadProgress').val(per);
                    //$('#uploadLable').text(per +'%');
                }, false);
                return xhr;
            }

        },
        beforeSend: function (xhr) {
            //$('#uploadProgress').css('visibility', 'visible');
            //$('#uploadLable').css('visibility', 'visible');
            app.progressVisible = 'visible';



        },
        success: function (result) {
            app.progressVisible = 'hidden',
                app.progressValue = 0;
            app.$refs.file.value = null;
            app.getFile();
            //$.messager.progress('close');
            if (result === "发生错误") {
                $.messager.alert({
                    title: "错误",
                    msg: "发生错误，上传错误",
                    icon: "error"
                })
                return;
            }
        }
    });
}
function ieVersion() {
    // 取得浏览器的userAgent字符串
    var userAgent = navigator.userAgent;
    // 判断是否为小于IE11的浏览器
    var isLessIE11 = userAgent.indexOf('compatible') > -1 && userAgent.indexOf('MSIE') > -1;
    // 判断是否为IE的Edge浏览器
    var isEdge = userAgent.indexOf('Edge') > -1 && !isLessIE11;
    // 判断是否为IE11浏览器
    var isIE11 = userAgent.indexOf('Trident') > -1 && userAgent.indexOf('rv:11.0') > -1;
    if (isLessIE11) {
        var IEReg = new RegExp('MSIE (\\d+\\.\\d+);');
        // 正则表达式匹配浏览器的userAgent字符串中MSIE后的数字部分，，这一步不可省略！！！
        IEReg.test(userAgent);
        // 取正则表达式中第一个小括号里匹配到的值
        var IEVersionNum = parseFloat(RegExp['$1']);
        if (IEVersionNum === 7) {
            // IE7
            return 7
        } else if (IEVersionNum === 8) {
            // IE8
            return 8
        } else if (IEVersionNum === 9) {
            // IE9
            return 9
        } else if (IEVersionNum === 10) {
            // IE10
            return 10
        } else {
            // IE版本<7
            return 6
        }
    } else if (isEdge) {
        // edge
        return 'edge'
    } else if (isIE11) {
        // IE11
        return 11
    } else {
        // 不是ie浏览器
        return -1
    }
}
