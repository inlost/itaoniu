<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="bring.aspx.vb" Inherits="TaoNiu.bring" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link type='text/css' rel='stylesheet' href='style/bring.css'/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
<div id="left_frame">
	<ul>
		<li class="right_li_one">网站信息</li>
		<li><a href="about.aspx"class="title"  >关于我们</a></li>
		<li><a href="net.aspx" >诚聘英才</a></li>
        <li><a href="contactus.aspx">联系我们</a></li>
	</ul>
</div>
<div class="bring_left">
<div class="postion">
	<span>您的位置:<a class="a_link" href="../../Default.aspx" target="_parent">itaoniu</a> >关于我们</span>
</div>
<div class="nav_about">
<pre>
<span class="red_bold">柳州市桃牛信息技术有限责任公司简介</span>

    柳州市桃牛信息技术有限责任公司2011年5月30日成立，注册资金100万，现有员工4人。
桃牛网<a class="a_link" href="../../Default.aspx" target="_parent">www.itaoniu.com</a>将是公司成立后第一个重要的战略任务。桃牛网试图创造一种网上
销售、消费与日常生活完全融合的崭新商业模式，并由此带给所有人如下的全新体验与认识：
任何人都可以上网销售任何东西，任何销售行为都肯定能赚到钱，销售将极度地可信、快捷且
便宜。
    桃牛网自信这将是一场新的互联网商业革命，同时将催生大量难以估计的新的互联网商
业行为，创造海量的就业和财富，帮助弱势的人群以近乎零的成本和平等的姿态参与到商品经
济的社会生活中来，抬升整个商业文明水平，并在这个过程中极大地推动国民经济更快更稳
健地发展，最终深刻地改变所有人的消费模式与生活方式。
    .桃牛网的<span class="green">信念</span>：<span class="bold blue">诚信、公平、透明</span>
    .桃牛网的<span class="green">宗旨</span>：我们存在，是因为我们对人有帮助
    .桃牛网的<span class="green">发展计划</span>：我们计划在柳州启动桃牛网，第二年开始进行商业模式的广西区内
复制，之后将很快进行全国范围模式复制，三年内实现网上与地面网络服务的全国覆盖。
    桃牛网在这个宏伟的计划中，需要所有有志于实践自己人生理想的人士的帮助与支持，
桃牛网将以前所未有的开放的态度来为追求梦想的人才提供事业的机遇与广阔的平台。

  地 址：柳州市八一路福柳新都15栋2-2-2
  邮 箱：hr@itaoniu.com
  电 话：18978059931
</pre>
</div>
</div>


<script type="text/javascript">
    (function tle() {
        var title = $(".title").html();
        $("title").html("iTaoNiu - "+title);
    })();
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
