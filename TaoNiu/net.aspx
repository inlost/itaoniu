<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="net.aspx.vb" Inherits="TaoNiu.bring" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link type='text/css' rel='stylesheet' href='style/bring.css'/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
<div id="left_frame">
	<ul>
		<li class="right_li_one">网站信息</li>
		<li><a href="about.aspx" >关于我们</a></li>
		<li><a href="net.aspx" class="title" >诚聘英才</a></li>
        <li><a href="contactus.aspx">联系我们</a></li>
	</ul>
</div>
<div class="bring_left">
<div class="postion">
	<span>您的位置:<a class="a_link" href="Default.aspx" >itaoniu</a> >诚聘英才</span>
</div>
<div class="nav_center">
<pre>
<span class="red_bold">柳州高薪招聘.net程序员</span>
公司名称：柳州市桃牛信息技术有限责任公司
    。精通asp.net（c#，vb至少一种）
    。良好的代码书写风格
    。精通mvc
    。有一定的数据库设计经验
    。有ajax应用开发经验
    。了解并能合理使用设计模式
    。了解页面性能优化
    。熟悉HTML，javascript
    。有大型web应用开发维护经验（集群web server session同步等）
    ☆加分项：有silverlight等富媒体开发经验 
	      了解linkq
　　待遇：
　　  ．试用期2个月，待遇1500
  　　．转正之后2000-2500
　  　．工作半年后，底薪上浮50%，公司买五金
    
    求职信请投放至: <span class="red">hr@itaoniu.com</span>
    <span class="bold">备注</span>：
        请将含一寸免冠相片的个人简历一并发送至<span class="blue">hr@itaoniu.com</span>。请在    简历中提供能直接联系您的电话，谢绝无约拜访，我们会在收到简历后    及时与您取得联系。
</pre>
</div>
<div id="right_frame">
	<ul>
		<li><a href="net.aspx">.net程序员</a></li>
	</ul>
</div>
</div>


<script type="text/javascript">
<!--
    (function tle() {
        var title = $(".title").html();
        $("title").html("iTaoNiu - " + title);
    })();
//-->
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
