<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Site/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    考试管理
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
         <table class="tableBasic" cellspacing="1" cellpadding="2" width="99%" align="center" border="0">
         <thead>
          <tr>
                <th colspan="13"><a href="/Score/Import" class="actionBtn"   >导入</a>&nbsp;&nbsp;
                 <a href="javascript:void(0)" class="actionBtn" onclick="VaTime()" >有效期</a>
                  考试类型： 
                 <% SelectList SelList = ViewData["SelList"] as SelectList; %>
                 <%= Html.DropDownList("TypeName", SelList,new {style = "width :150px;"}) %>
                &nbsp;&nbsp;
                姓名：<input class="inpMain" id="Name" name="Name" style="width:100px;" maxlength="25"/>
                准考证号：<input class="inpMain" id="Number" name="Number" style="width:100px;" maxlength="25"/>
                有效日期：<input class="Wdate inpMain" id="ValidTimeStra" name="ValidTimeStra" style="width:100px;height:18px;" maxlength="25" onclick="WdatePicker()"  /> 至 
                    <input class="Wdate inpMain" id="ValidTimeEnd" name="ValidTimeEnd" style="width:100px;height:18px;" maxlength="25"  onclick="WdatePicker()"  />
                    <a href="javascript:void(0)" class="actionBtn"  onclick="seldata()" >查询</a>
                </th>
                 
            </tr>
            <tr>
                <th ><input id="all" type="checkbox" onclick="seleall(this)"  /></th>
                <th >ID</th>
                <th >有效日期</th>
                <th >考试类型</th>
                <th >准考证号</th>
                <th >姓名</th>
                <th >性别</th>
                <th >出生年月</th>
                <th >成绩</th>
                <th >单位</th>
                <th >职务</th>
                <th >联系方式</th>
                <th >导入日期</th>
            </tr>
             </thead>
             <tbody id="tabresult"></tbody>
           
        </table>
         <div id="paging" class="paging" style="display:none;">       
            <a href='javascript:void(0);' class='paging_B2'>2</a>
            <a href='javascript:void(0);' class='paging_A myImg'>上一页</a><a href='javascript:void(0);' class='paging_C2'>下一页</a>
            <span>跳转</span><input name='pageIndex' id='pageIndex' class='paging_D' type='text'><span>页</span>
            <input name='' value='GO' class='paging_E' onclick='jump();' type='button'> 
          </div>
        <input id='PageSize' value='20' type='hidden'/>
        <input id='PageCurr' value='1' type='hidden'/>
        <input id='Pages' value='0' type='hidden'/>
    </div>

    <div id="light" class="white_content" style="padding-top:50px;"> 
            <table class="tableBasic" cellspacing="1" cellpadding="2" width="500px" align="center" border="0" >
                <tr>
                <th style="text-align:center;" colspan="2" id="typetitle">批量修改勾选项有效日期</th>
                </tr>
                <tr>
                <th style="text-align:right;"><font style="color:red">*</font> 有效日期：</th>
                <td  style="text-align:left;"><input id="ValidTime" name="ValidTime" style="width:100px;height:18px;" maxlength="25" class="inpMain Wdate"  onclick="WdatePicker()"  /></td>
                </tr>
                 
                <tr>
                <td  style="text-align:center;" colspan="2"><input type="button" value="保存"  style="width:50px;"  onclick="save()" /> &nbsp;&nbsp;<input type="button" value="关闭"  style="width:50px;"  onclick="    closediv()" /></td>
                </tr>
            </table>
    </div> 
    <div id="fade" class="black_overlay"> 
    </div>



    <link href="../../Js/js.dates/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/js.dates/WdatePicker.js" type="text/javascript"></script>
    <style> 
    .black_overlay{  display: none;  position: absolute;  top: 0%;  left: 0%;  width: 100%;  height: 100%;  background-color: #B1ACAC;  z-index:1001;  -moz-opacity: 0.8;  opacity:.80;  filter: alpha(opacity=80);  }  .white_content {  display: none;  position: absolute;   top: 25%;  left:50%;margin-left:-300px;  width: 600px;  height: 200px;  padding: 16px;    background-color: white;  z-index:1002;  overflow: auto;  }  </style> 

    <script>

        $(function () {
            $("#ullist").find("li").removeAttr("class");
            $("#liscore").attr("class", "cur")
            $("#urHere").html("管理中心<b>></b><strong>考试列表</strong> ")
        })
        //有效日期操作star
        function seleall(obj) {
            if (obj.checked) {
                $("input[name='allid']").each(function () { this.checked = true; });
            } else {
                $("input[name='allid']").each(function () { this.checked = false; });
            }
        }
        function VaTime() {
            var mk = 0;
            $("input[name='allid']").each(function () {
                if (this.checked) {
                    mk++;
                }
            });
            if (mk == 0) {
                alert("请至少勾选一项数据记录");
                return;
            }
            $("#light").show();
            $("#fade").show();
            $("#ValidTime").val("");
        }
        function save() {
            if ($("#ValidTime").val() == "") {
                alert("请选择有效日期");
                return;
            }

            var ids = "";
            $("input[name='allid']").each(function () {
                if (this.checked) {
                    ids += this.value + ",";
                }
            });
            if (ids != "")
                ids = ids.substring(ids, ids.length - 1);

            $.ajax({
                url: "/Score/SaveValidTime?" + Math.random(),
                data: { ID: ids, ValidTime: $("#ValidTime").val() },
                type: "Post",
                dataType: "json",
                success: function (data) {
                    var obj = eval('(' + data + ')');
                    var r = obj.Success;
                    if (r) {
                        $("#light").hide();
                        $("#fade").hide();
                        alert(obj.Msg);
                        $("#PageCurr").val(1);
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

        //有效日期操作end

        


        /////////////////////////////////////////////////////////////////////////////////////////分页
        function getdata() {
            $("#Pages").val("");
            $("#paging").hide();

            var index = parseInt($("#PageCurr").val());
            var size = parseInt($("#PageSize").val());
            $("#tabresult").empty();

            $.ajax({
                url: "/Score/GetPageList?" + Math.random(),
                data: { PageSize: $("#PageSize").val(), PageCurr: $("#PageCurr").val(), TypeName: $("#TypeName").val(), Name: $("#Name").val(), Number: $("#Number").val(), ValidTimeStra: $("#ValidTimeStra").val(), ValidTimeEnd: $("#ValidTimeEnd").val() },
                type: "Post",
                dataType: "json",
                success: function (data) {
                    var obj = eval('(' + data + ')');
                    var r = obj.Success;
                    if (r) {
                        var d = obj.Data;
                        if (d.length == 0) {
                            alert("没查询到数据记录！")
                        } else {
                            var hl = "";
                            for (var i = 0; i < d.length; i++) {
                                hl += "<tr><td style='text-align:center;'><input name='allid' type='checkbox' value='" + d[i].ID + "' /></td><td >" + d[i].ID + "</td><td style='text-align:center;'>" + changeDate(d[i].ValidTime) + "</td><td >" + d[i].TypeName + "</td><td style='text-align:center;'>" + d[i].Number + "</td><td >" + d[i].Name + "</td><td style='text-align:center;'>" + d[i].Sex + "</td><td style='text-align:center;'>" + d[i].Birth + "</td><td style='text-align:center;'>" + (parseFloat(d[i].Grade) < 0 ? "缺" : d[i].Grade) + "</td><td >" + d[i].Unit + "</td><td >" + d[i].Job + "</td><td style='text-align:center;'>" + d[i].Phone + "</td><td style='text-align:center;'>" + changeDate(d[i].CreatTime) + "</td></tr>"
                            }
                            $("#tabresult").html(hl);

                            var count = parseInt(obj.Msg.split('|')[0]); //后台返回总条
                            var pages = parseInt(obj.Msg.split('|')[1]); //后台返回总页
                            $("#Pages").val(pages);
                            //分页
                            var page = "";
                            page += "<a href='javascript:void(0);' class='paging_B2' style='text-decoration:none;'>总记录：" + count + "，每页" + size + "条， 当前页/总页数 ：" + index + "/" + pages + "</a>"
                            if (index == 1) {
                                page += "<a href='javascript:void(0);' class='paging_A myImg'>上一页</a>";
                            } else {
                                page += "<a href='javascript:void(0);' class='paging_A2' onclick='pre()'>上一页</a>";
                            }
                            if (pages == index) {
                                page += "<a href='javascript:void(0);' class='paging_C myImg'>下一页</a>";
                            } else {
                                page += "<a href='javascript:void(0);' class='paging_C2' onclick='next()'>下一页</a>";
                            }

                            page += "<span>跳转</span><input name='pageIndex' id='pageIndex' class='paging_D' type='text'><span>页</span>";
                            page += "<input name='' value='GO' class='paging_E' onclick='jump();' type='button'>";
                            $("#paging").html(page);
                            $("#paging").show();
                        }

                    } else {
                        alert(obj.Msg)
                    }
                }
            })
        }
        getdata();

        //上一页
        function pre() {
            var pr = parseInt($("#PageCurr").val()) - 1;
            $("#PageCurr").val(pr);
            getdata();
        }
        //下一页
        function next() {
            var nx = parseInt($("#PageCurr").val()) + 1;
            $("#PageCurr").val(nx)
            getdata();
        }
        //跳转
        function jump() {
            var pageIndex = parseInt($("#pageIndex").val());
            var Pages = parseInt($("#Pages").val());
            if (pageIndex > Pages) {
                alert("超出总页数！")
            } else {
                $("#PageCurr").val(pageIndex);
                getdata();
            }


        }
        //查询
        function seldata() {
            $("#PageCurr").val(1);
            getdata();
        }

        //json后格式时间 
        function changeDate(str) {
            if (str == "" || str == null) {
                return "";
            } else {
                var object = str.toString().replace("/Date(", "");
                object = object.replace(")/", "");
                var date = new Date(parseInt(object, 10));
                var year = date.getFullYear();
                var month = date.getMonth() + 1;
                var day = date.getDate();
                return year + "-" + month + "-" + day;
            }
        }
        function changeDateTime(str) {
            if (str == "" || str == null) {
                return "";
            } else {
                var object = str.toString().replace("/Date(", "");
                object = object.replace(")/", "");
                var date = new Date(parseInt(object, 10));
                var year = date.getFullYear();
                var month = date.getMonth() + 1;
                var day = date.getDate();
                var hours = date.getHours();
                var minutes = date.getMinutes();
                var seconds = date.getSeconds();
                return year + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;
            }
        }
</script>
</asp:Content>





