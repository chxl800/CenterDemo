<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    考试明细导入
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="myfrm" action="/Score/Upload" method="post" enctype="multipart/form-data">

<table class="tableBasic" cellspacing="1" cellpadding="2" width="500px" align="center" border="0">
    <tr>
        <th  colspan="2">考试明细-导入</th>
        </tr>
        <tr>
        <th  style="text-align:right;"><font style="color:red">*</font> 考试类型：</th>
        <td  style="text-align:left;">
            <% SelectList SelList = ViewData["SelList"] as SelectList; %>
            <%= Html.DropDownList("TypeName", SelList,new {style = "width :150px;"}) %>
        </td>
        </tr>
        <tr>
        <th  style="text-align:right;"><font style="color:red">*</font> 有效日期：</th>
        <td  style="text-align:left;"><input id="ValidTime" name="ValidTime" style="width:100px;height:18px;" maxlength="25" class="Wdate inpMain"  onclick="WdatePicker()"  /></td>
        </tr>
        <tr>
        <th  style="text-align:right;"><font style="color:red">*</font> 选择文件：</th>
        <td  style="text-align:left;"><input id="files" type="file" name="files"  style="width:150px;"  /></td>
        </tr>
        <tr>
              
        <td  style="text-align:center;" colspan="2"><input type="button" value="导入"  style="width:50px;" onclick="vdata()"  />
            <input id="Submit1" type="submit" value="submit" style="display: none" />
            <font style="color:red"><br /><br />
            说明:1 先填写带*的内容 ，再导入。2 模板里面列名不能修改，列名，列数量保持一样。 3 Exel 可以加多个标签导入，第1行标题可改，第2行列名必须一样。
             </font><a href="../../Common/2016职称英语考试成绩(模板）.xlsx">模板下载</a></td>
        </tr>
</table>
</form>
    <link href="../../Js/js.dates/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/js.dates/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function vdata() {
            if ($("#TypeName").val() == "") {
                alert("请选择考试类型！");
                return;
            }
            if ($("#ValidTime").val() == "") {
                alert("请选择有效期！");
                return;
            }
            if ($("#files").val() == "") {
                alert("请选择文件！");
                return;
            }
            $("#Submit1").click();
        }
        $(function () {
            $("#ullist").find("li").removeAttr("class");
            $("#liscore").attr("class", "cur")
            $("#urHere").html("管理中心<b>></b><strong>考试明细-导入</strong> ")
        })
    </script>
</asp:Content>
  