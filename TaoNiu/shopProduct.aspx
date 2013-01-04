<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="shopProduct.aspx.vb" Inherits="TaoNiu.shopProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type='text/css' rel='stylesheet' href="<%Call shoop_Info("style")%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
    <div id="logo" class="hidden">
        <h2><a href="<%Call shoop_Info("link")%>"><%Call shoop_Info("name")%></a></h2>
        <h3><%Call shoop_Info("introduce")%></h3>
    </div>
    <div id="inCatNav">
        <%Call shoop_Info("nav")%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="sideBar">
        <%Call getSideBar()%>
    </div>
    <div id="goodBox">
         <div id="goodShow">
            <h1><%Call good_info("title")%></h1>
            <%Call good_info("buyBox","buyBox")%>
            <div id="price_select">
                <div id="select"><span class="tp">&nbsp;</span></div>
                <ul id="price_select_color">
                    <li class="price_select_title">颜色：</li>
                    <li><a href="#" cs="1">红色</a></li>
                    <li><a href="#" cs="22">黄色</a></li>
                    <li><a href="#" cs="15">蓝色</a></li>
                    <li><a href="#" cs="3">白色</a></li>
                </ul>
                <ul id="price_select_size">
                    <li class="price_select_title">大小：</li>
                    <li><a href="#" cs="22">大号</a></li>
                    <li><a href="#" cs="18">中号</a></li>
                    <li><a href="#" cs="10">小号</a></li>
                </ul>
                <ul id="price_select_inOne">
                    <li class="price_select_title">套餐：</li>
                    <li><a href="#" cs="5">套餐一</a></li>
                    <li><a href="#"cs="7">套餐二</a></li>
                    <li><a href="#" cs="9">套餐三</a></li>
                </ul>
            </div>
            <div class="clear"></div>
        </div>
        <div id="goodCommants">
            <ul id="funList">
                <li><a class="selected" id="#info" href="#info">商品详情</a></li>
                <li><a href="#comments">评价详情</a></li>
                <li><a href="#log">成交记录</a></li>
                <li><a href="#service">售后服务</a></li>
                <li><a href="#liuYan">留言板</a></li>
            </ul>
            <div id="info" rel="<%call getGid() %>">
                <%Call good_info("info")%>
            </div>
        </div>   
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
<script type="text/javascript" src="Scripts/jquery.jqzoom.min.js"></script>
    <script type="text/javascript">
        //<![CDATA[
        tn.checkNumber();
        tn.setGoodInfoTab();
        buySale.favorites_good();
        buySale.favorites_shop();
        buySale.gwc_add();
        buySale.buySelect();
        $(".jqzoom").jqueryzoom();
        buySale.meatSetion();
        //]]>
    </script>   

</asp:Content>
