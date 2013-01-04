<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/mall.Master" CodeBehind="mall_report.aspx.vb" Inherits="TaoNiu.mall_report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>报表系统</h1>
        <div id="info">
            <ol>
                <li>报表查看</li>
                <li>报表查询</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看商城的各种报表。</p></div>
        <h2><span class="subTitle">报表设置</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <form method="post" action="">
                <p class='halfWidth'><span class='infoTitle'>起始时间：</span><span class='info'>[start time]</span></p>
                <p class='halfWidth'><span class='infoTitle'>结束时间：</span><span class='info'>[end time]</span></p>
                <p class='halfWidth'><span class='infoTitle'>报表类型：</span><span class='info'>[report type]</span></p>
                <p class='halfWidth'><span class='infoTitle'>店铺选择：</span><span class='info'>[shop]</span></p>
                <p class='halfWidth'><span class='infoTitle'>楼层选择：</span><span class='info'>[floor]</span></p>
                <p class='halfWidth'><span class='infoTitle'>排序方法：</span><span class='info'>[order by]</span></p>
                <p class='halfWidth'><span class='infoTitle'></span><span class='info'><input class="subBt" type="submit" name="infoSub" value="生成报表"></span></p>
            </form>
            <div class="clear"></div>
        </div>
        <h2><span class="subTitle">当前报表</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
