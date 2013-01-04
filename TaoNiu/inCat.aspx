<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="inCat.aspx.vb" Inherits="TaoNiu.inCat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
    <div id="inCatNav" >
        <ul class="afterClear">
            <li><a class='selected' href='default.aspx'>首页</a></li>
            <%Call get_father_cat()%>
            <li><a href='http://bbs.itaoniu.com'>意见反馈</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <form id="form1" runat="server">
    <div id="goodsList">
        <div id="nowPath"><%Call getNowPath()%></div>
        <div id="catLists">
            <h2>分类列表</h2>
            <ul><%Call get_nav()%><div class="clear"></div></ul>
        </div>
        <div id="allGoods">
            <ul id="orderBy">
                <li><a <%Call getOrderClass("time-new") %> href="<% Call getOrderSrc("time-new") %>">最新的</a></li>
                <li><a <%Call getOrderClass("time-old") %> href="<% Call getOrderSrc("time-old") %>">旧点儿的</a></li>
                <li><a <%Call getOrderClass("price-cheap") %> href="<% Call getOrderSrc("price-cheap") %>">便宜的</a></li>
                <li><a <%Call getOrderClass("price-expensive") %> href="<% Call getOrderSrc("price-expensive") %>">贵的</a></li>
                <li><a <%Call getOrderClass("hot") %> href="<% Call getOrderSrc("hot") %>">热销的</a></li>
            </ul>
            <div id="allGoodsBox">
                <ul id="goodsListed">
                    <%Call getGoodsList()%>
                    <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                </ul>
                <ul id="pageLink">
                    <%Call getPageLink()%>
                </ul>
            </div>
        </div>
    </div> 
    <div id="goodsListSideBar">
        <h1>最新热卖</h1>
        <ul><%Call getSideBar()%></ul>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
