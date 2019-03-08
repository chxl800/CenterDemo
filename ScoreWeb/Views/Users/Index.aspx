<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    考试类型
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script>
         $(function () {
             $("#ullist").find("li").removeAttr("class");
             $("#liusers").attr("class", "cur")
             $("#urHere").html("管理中心<b>></b><strong>用户列表</strong> ")
         })
         function getdata() {
             $.ajax({
                 url: "/Users/GetList?" + Math.random(),
                 type: "Post",
                 dataType: "json",
                 success: function (data) {
                     var obj = eval('(' + data + ')');
                     var r = obj.Success;
                     if (r) {
                         var d = obj.Data;
                         var hl = "";
                         for (var i = 0; i < d.length; i++) {
                             hl += "<tr><td style='text-align:center;'>" + d[i].ID + "</td><td style='text-align:center;'>" + d[i].Name + "</td><td style='text-align:center;'><a class='actionBtn' href='javascript:void(0);' onclick='update(" + d[i].ID + ",\"" + d[i].Name + "\")'>修改</a>&nbsp;&nbsp;<a class='actionBtn' href='javascript:void(0);' onclick='dele(" + d[i].ID + ")'>删除</a></td></tr>"
                         }
                         $("#tabresult").html(hl);

                     } else {
                         alert(obj.Msg)
                     }
                 }
             })
         }
         getdata();

         function add() {
             $("#light").show();
             $("#fade").show();
             $("#typetitle").html("添加用户");
             $("#ID").val("");
             $("input[name='Name']").val("");
             $("input[name='Pass']").val("");
         }
         function update(id, Name) {
             $("#light").show();
             $("#fade").show();
             $("#typetitle").html("修改用户");
             $("#ID").val(id);
             $("input[name='Name']").val(Name);
             $("input[name='Pass']").val("");

         }
         function dele(id) {
             if (confirm("确定删除此用户！")) {
                 $.ajax({
                     url: "/Users/DeleUsers?" + Math.random(),
                     data: { ID: id },
                     type: "Post",
                     dataType: "json",
                     success: function (data) {
                         var obj = eval('(' + data + ')');
                         var r = obj.Success;
                         if (r) {
                             alert(obj.Msg);
                             getdata();
                         } else {
                             alert(obj.Msg)
                         }
                     }
                 })
             }

         }

         function save() {

             if ($("input[name='Name']").val() == "") {
                 alert("用户名不能为空！");
                 return;
             }
             if ($("input[name='Pass']").val() == "") {
                 alert("密码不能为空！");
                 return;
             }
             $.ajax({
                 url: "/Users/SaveUsers?" + Math.random(),
                 data: { ID: $("#ID").val(), Name: $("input[name='Name']").val(), Pass: $("input[name='Pass']").val() },
                 type: "Post",
                 dataType: "json",
                 success: function (data) {
                     var obj = eval('(' + data + ')');
                     var r = obj.Success;
                     if (r) {
                         $("#light").hide();
                         $("#fade").hide();
                         alert(obj.Msg);
                         getdata();
                     } else {
                         alert(obj.Msg)
                     }
                 }
             })

         }

         function closediv() {
             $("#light").hide();
             $("#fade").hide();
         }
    </script>
<style> 
  .black_overlay{  display: none;  position: absolute;  top: 0%;  left: 0%;  width: 100%;  height: 100%;  background-color: #B1ACAC;  z-index:1001;  -moz-opacity: 0.8;  opacity:.80;  filter: alpha(opacity=80);  }  .white_content {  display: none;  position: absolute;   top: 25%;  left:50%;margin-left:-300px;  width: 600px;  height: 200px;  padding: 16px;    background-color: white;  z-index:1002;  overflow: auto;  }  </style> 
</head> 
<body>
    <div>
        
    
        <table class="tableBasic" cellspacing="1" cellpadding="2" width="500px" align="center" border="0">
            <thead>
                <tr><th  colspan="3" style="text-align:left">&nbsp;&nbsp;<a class="actionBtn"  href='javascript:void(0);' onclick='add()'>添加</a></th></tr>
                <tr><th >ID</th><th >用户名</th><th >操作</th></tr>
            </thead>
            <tbody id="tabresult">

            </tbody>
        </table>

        <div id="light" class="white_content" style="padding-top:50px;"> 
               <table class="tableBasic" cellspacing="1" cellpadding="2" width="500px" align="center" border="0">
                 <tr>
                   <td  style="text-align:center;" colspan="2" id="typetitle"></td>
                 </tr>
                 <tr>
                   <th  style="text-align:right;"><font style="color:red">*</font> 用户名：</th>
                   <td  style="text-align:left;"><input class="inpMain"  name="Name" style="width:200px;" maxlength="25"/></td>
                 </tr>
                 <tr>
                   <th  style="text-align:right;"><font style="color:red">*</font> 密&nbsp;&nbsp;码：</th>
                   <td  style="text-align:left;"><input class="inpMain"  name="Pass" style="width:200px;" maxlength="25"/></td>
                 </tr>
                 <tr>
                   <td  style="text-align:center;" colspan="2"><input type="button" value="保存"  style="width:50px;"  onclick="save()" /> &nbsp;&nbsp;<input type="button" value="关闭"  style="width:50px;"  onclick="    closediv()" /></td>
                 </tr>
               </table>
        </div> 
        <div id="fade" class="black_overlay"> 
        </div>
        <input id="ID" type="hidden"  />
    </div>
</asp:Content>
