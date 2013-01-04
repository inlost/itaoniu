<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="weibo.aspx.vb" Inherits="TaoNiu.weibo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>桃牛网微博</title>
    <meta name="Description" content="桃牛网" />
	<meta name="Keywords" content="桃牛网微博" />
    <link type='text/css' rel='stylesheet' href='style/main.css'/>
    <script language='javascript' type='text/javascript' src='Scripts/jquery-1.4.1.js'></script>    <script language='javascript' type='text/javascript' src='Scripts/main.js'></script>
</head>
<body id="WB">
   <div id="wBody">
             <div id="header" class="afterClear"> 
                <div id="header-box">
                     <div id="logoSearch" class="clear">
                        <a id="logo" href="default.aspx" target="_parent" title="桃牛网"></a>
                        <span id="weibologo"></span>
                    </div>
                </div>
        </div>
        <div class="weiboSns afterClear">
            <div id="weibo" >
                <div id="weibo-new">
                    <h2>看看大家都在聊什么？</h2>
                    <ul>
                        <%getSnsPosts()%>                    
                    </ul>
                </div>
                <div id="weibo-lists" class="afterClear">
                 <form method="post" action="server_page/user_service.aspx?action=login">                    <div id="weiboLogin">
                        <ul>
                            <li><span> 账 号：</span><input type="text" name="ctl00$ContentContent$uid" id="weiboName" /></li>
                            <li><span>密 码：</span><input type="password" name="ctl00$ContentContent$password" id="weiboPass" /></li>
                            <li> <button type="button" name="ctl00$ContentContent$btLogin" id="weiboSub">登 录</button></li>
                        </ul>
                    </div>
                    </form>
                    <h2>他们在这里</h2>
                    <%getUserList()%>
                </div>
            </div>
      </div>
   </div>
     <script type="text/javascript">
        //<![CDATA[
         tn.setFloor0("#weibo-new", "#weibo-new ul");
        //]]>
    </script>
   <div id="footer">        <div id="linkList">              <a href="../about.aspx">关于桃牛</a>              <a href="#">营销中心</a>              <a href="#">合作伙伴</a>              <a href="#">帮助中心</a>              <a href="net.aspx">诚征英才</a>              <a href="contactus.aspx">联系我们</a>              <a href="#">网站地图</a>              <a href="#">版权说明</a>              <a href="#">加入商城</a>              <a href="#">开放平台</a>              <a href="#">桃牛联盟</a>        </div>        <div id="cps">              <a id="wj" href="http://www.gx.cyberpolice.cn/liuzhou/index.asp?cityP=%C1%F8%D6%DD%CA%D0"></a>              <div id="cpTxt">                    <p>Copyright ? 2010-2011 iTaoNiu.com 版权所有</p>                    <p>增值电信业务经营许可证：桂B2-20080224 </p>                              </div>        </div>  
   </div>
</body>
</html>
