<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<!DOCTYPE html>
<!--[if lt IE 7 ]> <html lang="en" class="no-js ie6 lt8"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="no-js ie7 lt8"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="no-js ie8 lt8"> <![endif]-->
<!--[if IE 9 ]>    <html lang="en" class="no-js ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html lang="en" class="no-js"> <!--<![endif]-->
    <head>
        <meta charset="UTF-8" />
        <title>登录</title>
        <link rel="stylesheet" type="text/css" href="/css/demo.css" />
        <link rel="stylesheet" type="text/css" href="/css/style.css" />
		<link rel="stylesheet" type="text/css" href="/css/animate-custom.css" />
        <script src="../../Js/jquery-1.7.1.min.js"></script>
          <script>
              function login() {
                  if ($("#username").val() == "") {
                      alert("用户名不能为空！");
                      return;
                  }
                  if ($("#password").val() == "") {
                      alert("密码不能为空！");
                      return;
                  }
       
                  $.ajax({
                      url: "/Home/LoginSubmit?" + Math.random(),
                      data: { Name: $("#username").val(),Pass: $("#password").val() },
                      type: "Post",
                      dataType: "json",
                      success: function (data) {
                          var obj = eval('(' + data + ')');
                          var r = obj.Success;
                          if (r) {
                              var d = obj.Data;
                              window.location.href = "/score/index";

                          } else {
                              alert(obj.Msg)
                          }
                      }
                  })
              }
            
    </script>
    </head>
    <body>
        <div class="container">
            <!-- Codrops top bar -->
            <div class="codrops-top">
                <span class="right">
                </span>
                <div class="clr"></div>
            </div><!--/ Codrops top bar -->
            <header>
                <h1>欢迎使用考试系统管理</h1>

            </header>
            <section>				
                <div id="container_demo" >
                    <!-- hidden anchor to stop jump http://www.css3create.com/Astuce-Empecher-le-scroll-avec-l-utilisation-de-target#wrap4  -->
                    <a class="hiddenanchor" id="toregister"></a>
                    <a class="hiddenanchor" id="tologin"></a>
                    <div id="wrapper">
                        <div id="login" class="animate form">
                                <h1>Log in</h1> 
                                <p> 
                                    <label for="username" class="uname" data-icon="u" > 你的用户名</label>
                                    <input id="username" name="username" required="required" type="text" placeholder="请输入你的用户名"/>
                                </p>
                                <p> 
                                    <label for="password" class="youpasswd" data-icon="p"> 你的密码 </label>
                                    <input id="password" name="password" required="required" type="password" placeholder="请输入你的密码" /> 
                                </p>
                              
                                <p class="login button"> 
                                    <input type="button" value="Login"  onclick="login()"/> 
								</p>
                                <p class="change_link">
									 
								</p>
                        </div>

                 
						
                    </div>
                </div>  
            </section>
        </div>
    </body>
</html>