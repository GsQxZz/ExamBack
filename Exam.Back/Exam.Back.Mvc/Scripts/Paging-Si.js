//分页数据
//pageIndex:当前页码数,初始加载为1
//PageSize:每页的页码数,
//Count:所有数据的条数
//Name:用来存放分页控件的控件<input id='Name' type='button'/>
function Paging(PageIndex, PageSize, Count, Name) {
    //$("#'" + Name + "'").empty();
    $("#PagesDiv").empty();
    var CurrentPage = Math.ceil(Count / PageSize);
    $("#zhui").append("&nbsp共有:<label>" + CurrentPage + "</label>页");
    var str = "";
    if (PageSize == Count && PageIndex == 1 || PageSize > Count) {
        str += "<label>首页</label>" + "&nbsp";
        str += "<label>上一页</label>" + "&nbsp";
        str += "<label>下一页</label>" + "&nbsp";
        str += "<label>最后一页</label>" + "&nbsp";
        str += "<label>当前第" + PageIndex + "页</label>";
    }
    else if (PageIndex == 1 && Count > PageSize) {
        str += "<label>首页</label>" + "&nbsp";
        str += "<label>上一页</label>" + "&nbsp";
        str += "<a href='#' style='color:blue' onclick='showProduct(" + (PageIndex + 1) + "," + PageSize + ")'>下一页</a>" + "&nbsp";
        str += "<a href='#' style='color:blue' onclick='showProduct(" + CurrentPage + "," + PageSize + ")'>最后一页</a>" + "&nbsp";
        str += "<label>当前第" + PageIndex + "页</label>";
    }
    else if (PageIndex > 1 && PageIndex < CurrentPage) {
        str += "<a href='#' style='color:blue' onclick='showProduct(" + 1 + "," + PageSize + ")'>首页</a>" + "&nbsp";
        str += "<a href='#' style='color:blue' onclick='showProduct(" + (PageIndex - 1) + "," + PageSize + ")'>上一页</a>" + "&nbsp";
        str += "<a href='#' style='color:blue' onclick='showProduct(" + (PageIndex + 1) + "," + PageSize + ")'>下一页</a>" + "&nbsp";
        str += "<a href='#' style='color:blue' onclick='showProduct(" + CurrentPage + "," + PageSize + ")'>最后一页</a>" + "&nbsp";
        str += "<label>当前第" + PageIndex + "页</label>";
    }
    else {
        str += "<a href='#' style='color:blue' onclick='showProduct(" + 1 + "," + PageSize + ")'>首页</a>" + "&nbsp";
        str += "<a href='#' style='color:blue' onclick='showProduct(" + (PageIndex - 1) + "," + PageSize + ")'>上一页</a>" + "&nbsp";
        str += "<label>下一页</label>" + "&nbsp";
        str += "<label>最后一页</label>" + "&nbsp";
        str += "<label>当前第" + PageIndex + "页</label>";
    }
    //$("#'" + Name + "'").append(str);
    $("#PagesDiv").append(str);
}