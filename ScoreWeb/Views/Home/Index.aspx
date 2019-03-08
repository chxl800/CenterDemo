<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>查看考试</title>
    <link href="../../Common/tab.css" rel="stylesheet" />
    <script src="../../Js/jquery-1.7.1.min.js"></script>
    <script>
        function Search() {
            if ($("#TypeName").val() == "")
            {
                alert("请选择考试类型！");
                return;
            }
            if ($("input[name='Name']").val() == "") {
                alert("姓名不能为空！");
                return;
            }
            if ($("input[name='Number']").val() == "") {
                alert("准考证号不能为空！");
                return;
            }
            if ($("input[name='Code']").val() == "") {
                alert("验证码不能为空！");
                return;
            }
            $("#tabresult").hide();
            $("#tabresult").empty();
            $.ajax({
                url: "/Home/GetList?" + Math.random(),
                data: { TypeName: $("#TypeName").val(), Name: $("input[name='Name']").val(), Number: $("input[name='Number']").val(), Code: $("input[name='Code']").val() },
                type: "Post",
                dataType: "json",
                success: function (data) {
                    var obj = eval('(' + data + ')');
                    var r = obj.Success;
                    if (r) {
                        var d = obj.Data;
                        if(d.length==0)
                        {
                            alert("没查询到数据记录！")
                        } else {
                            var hl = "<tr><th class='th_tr'>考试类型</th><th class='th_tr'>准考证号</th><th class='th_tr'>姓名</th><th class='th_tr'>成绩</th></tr>";
                            for(var i=0;i<d.length;i++)
                            {
                                hl += "<tr><td class='td_bg'>" + d[i].TypeName + "</td><td class='td_bg'>" + d[i].Number + "</td><td class='td_bg'>" + d[i].Name + "</td><td class='td_bg'>" + (parseFloat(d[i].Grade) < 0 ? "缺" : d[i].Grade) + "</td></tr>"
                            }
                            $("#tabresult").html(hl);
                            $("#tabresult").show();
                        }
 
                    } else {
                        alert(obj.Msg)
                    }
                }
            })
        }
        function Clear() {
            $("#TypeName").get(0).selectedIndex = 0;
            $("input[name='Name']").val("");
            $("input[name='Number']").val("");
            $("input[name='Code']").val("");
        }
        $(function () {
            $('#imgCode').on("click", function () {
                $('#imgCode').attr('src', '/Home/ShowCode?' + Math.random());
            });
        })

    </script>
</head>
<body>
    <div style="padding-top:50px;">
        <table class="tabs" cellspacing="1" cellpadding="2" width="500px" align="center" border="0">
             <tr>
               <th class="th_tr" style="text-align:right;"><font style="color:red">*</font> 考试类型：</th>
               <td class="td_bg" style="text-align:left;">
                 <% SelectList SelList = ViewData["SelList"] as SelectList; %>
                 <%= Html.DropDownList("TypeName", SelList,new {style = "width :150px;"}) %>
               </td>
             </tr>
             <tr>
               <th class="th_tr" style="text-align:right;"><font style="color:red">*</font> 姓名：</th>
               <td class="td_bg" style="text-align:left;"><input name="Name" style="width:100px;" maxlength="25"/></td>
             </tr>
             <tr>
               <th class="th_tr" style="text-align:right;"><font style="color:red">*</font> 准考证号：</th>
               <td class="td_bg" style="text-align:left;"><input name="Number" style="width:100px;" maxlength="25"/></td>
             </tr>
              <tr>
               <th class="th_tr" style="text-align:right;"><font style="color:red">*</font> 验证码：</th>
               <td class="td_bg" style="text-align:left;"><input name="Code" style="width:100px;" maxlength="4"/> <img id="imgCode" src="/Home/ShowCode" style="cursor:pointer; width:100px;height:25px" /></td>
             </tr>
             <tr>
               <td class="td_bg" style="text-align:center;" colspan="2"><input type="button" value="考试查询"  style="width:100px;"  onclick="Search()" />&nbsp;&nbsp;<input type="button" value="重置"  style="width:50px;"   onclick="Clear()" /></td>
             </tr>
        </table>

        <table id="tabresult" class="tabs" cellspacing="1" cellpadding="2" width="500px" align="center" border="0" style="display:none">
        </table>
    </div>
</body>
</html>
