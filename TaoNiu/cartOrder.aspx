<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="cartOrder.aspx.vb" Inherits="TaoNiu.cartOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
    <div id="inCatNav">
        <ul>
            <li><a href='default.aspx'>首页</a></li>
            <li><a href='cart.aspx'>购物车</a></li>
            <li><a class='selected' href='#'>订单提交</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="goodsInfo">
        <h2>商品列表</h2>
        <%Call getGood()%>
    </div>
    <div id="submitOrder">
        <form id="order" method="post" action="server_page/order.aspx?action=addList">
            <%Call getForm()%>
        </form>
    </div>
    <div class="clear"></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
