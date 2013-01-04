<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="contactus.aspx.vb" Inherits="TaoNiu.contactus" %>
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
		<li><a href="net.aspx" >诚聘英才</a></li>
        <li><a href="contactus.aspx" class="title" >联系我们</a></li>
	</ul>
</div>
<div class="bring_left">
<div class="postion">
	<span>您的位置:<a class="a_link" href="../../Default.aspx" target="_parent">itaoniu</a> >联系我们</span>
</div>
<div class="nav_about">
<pre>

        <span class="red_bold">联系我们</span>

        地 址：柳州市八一路福柳新都15栋2-2-2
        邮 箱：<span class="blue">hr@itaoniu.com</span>
        电 话：18978059931
</pre>
</div>
</div>


<script type="text/javascript">
    (function tle() {
        var title = $(".title").html();
        $("title").html("iTaoNiu - " + title);
    })();
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
