$(function () {
    Load(1, 10);
})
//加载内容
function Load(pageindex, pagesize) {
    var UserId = "";
    if ($("#name").val() == null) {
        userId = null;
    }
    else {
        userId = $("#name").val();
    }
    $.ajax({
        url: "/Journal/GetList",
        type: "get",

        dataType: "json",
        data: { UserName: userId, pageindex: pageindex, pagesize: pagesize },
        success: function (obj) {
            $("#tb").empty();

            if (obj.Data.length > 0) {
                var str = "";
                $(obj.Data).each(function () {
                    str += "<tr>"
                    str += "<td>" + this.AccountName + "</td>"
                    str += "<td>" + this.Operate + "</td>"
                    str += "<td>" + (this.OpreateResult == 1 ? "成功" : "失败") + "</td>"
                    str += "<td>" + (this.OperateTime.replace(/T/,' ').substring(0,19)) + "</td>"
                    str += "</tr>"
                })
                $("#tb").append(str);
                Fen(obj.Count, pageindex, pagesize);
            }
        }
    })
}
//分页
function Fen(Count, Pageindex, Pagesize) {
    $("#page").empty();
    var str = "";
    var page = Math.ceil(Count / Pagesize);
    str += "共 " + page + " 页"
    str += "&nbsp;&nbsp;&nbsp;&nbsp;"
    str += "第 " + Pageindex + "/" + page + " 页";
    str += "&nbsp;&nbsp;&nbsp;&nbsp;"
    if (Pageindex > 1) {
        str += "<a href='#' onclick='Load(1," + Pagesize + ")'>首页</a>";
        str += "&nbsp;&nbsp;&nbsp;&nbsp;"
        str += "<a href='#' onclick='Load(" + (Pageindex - 1) + "," + Pagesize + ")'>上一页</a>";
        str += "&nbsp;&nbsp;&nbsp;&nbsp;"
    }
    else {
        str += "首页";
        str += "&nbsp;&nbsp;&nbsp;&nbsp;"
        str += "上一页";
        str += "&nbsp;&nbsp;&nbsp;&nbsp;"
    }
    if (Pageindex < page) {
        str += "<a href='#' onclick='Load(" + (Pageindex + 1) + "," + Pagesize + ")'>下一页</a>";
        str += "&nbsp;&nbsp;&nbsp;&nbsp;"
        str += "<a href='#' onclick='Load(" + page + "," + Pagesize + ")'>尾页</a>";
        str += "&nbsp;&nbsp;&nbsp;&nbsp;"
    }
    else {
        str += "下一页";
        str += "&nbsp;&nbsp;&nbsp;&nbsp;"
        str += "尾页";
        str += "&nbsp;&nbsp;&nbsp;&nbsp;"
    }
    str += "每页 " + Pagesize + " 条数据"
    str += "&nbsp;&nbsp;&nbsp;&nbsp;"
    str += "共 " + Count + " 条数据"
    $("#page").append(str);
}