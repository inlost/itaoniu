<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="cart.aspx.vb" Inherits="TaoNiu.cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
<div id="inCatNav">
    <ul class="afterClear">
        <li><a class='selected' href='default.aspx'>首页</a></li>
        <li><a href='http://bbs.itaoniu.com'>意见反馈</a></li>
    </ul>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <form id="form1" runat="server">
    <div id="goodsList">
        <div id="nowPath"><a href='default.aspx'>首页</a>&gt;<span>购物车</span></div>
        <div id="catLists">
        </div>
        <div id="allGoods">
            <div id="allGoodsBox">
                <ul id="goodsListed">
                    <%Call getGoodsList()%>
                </ul>
                <ul id="pageLink">

                </ul>
            </div>
        </div>
    </div> 
    <div id="goodsListSideBar">
        <h1>购物车操作</h1>
        <a href="server_page/cart.aspx?action=clear_cart">清空购物车</a>
        <a href="cartOrder.aspx">去收银台</a>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
    <script type="text/javascript">
        //<![CDATA[
        buySale.gwc_number_modify();
        buySale.gwc_del();
        //]]>
    </script>   


</asp:Content>
