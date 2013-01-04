<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/mall.Master" CodeBehind="mall_admin.aspx.vb" Inherits="TaoNiu.mall_admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>信息概览</h1>
        <div id="info">
            <ol>
                <li>商城基本信息</li>
                <li>商城流量信息</li>
                <li>商城销售概览</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看商城的基本情况概览。</p></div>
        <h2><span class="subTitle">商城基本信息</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <%Call witeMallInfo ("商城名称：","[title]") %>
            <%Call witeMallInfo ("商城状态：","[status]") %>
            <%Call witeMallInfo ("商城店铺数量：","[count of member]") %>
            <%Call witeMallInfo("商城开设日期：", "[date of startup]")%>
            <%Call witeMallInfo("商城会员数量：", "[count of vip]")%>
            <div class="clear"></div>
        </div>
        <h2><span class="subTitle">商城流量信息</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <%Call witeMallInfo ("当前在线人数：","[online number]") %>
            <%Call witeMallInfo("最高在线：", "[top online number]")%>
            <%Call witeMallInfo ("最高在线日期：","[date of top online number]") %>
            <div class="clear"></div>
        </div>
        <h2><span class="subTitle">销售情况</span><span class="subBt"><a href="#">资金变动详单</a></span></h2>
        <div class="infoBox">
            
            <div class="clear"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
