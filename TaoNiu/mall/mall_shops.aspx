<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/mall.Master" CodeBehind="mall_shops.aspx.vb" Inherits="TaoNiu.mall_shops" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>商城店铺</h1>
        <div id="info">
            <ol>
                <li>店铺开设</li>
                <li>店铺关闭</li>
                <li>店铺浏览</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以对商城的店铺进行管理。</p></div>
        <h2><span class="subTitle">开设新店</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <form method="post" action="">
                <p class='halfWidth'><span class='infoTitle'>店铺名称：</span><span class='info'><input type="text" name="shop_name" /></span></p>
                <p class='halfWidth'><span class='infoTitle'>店铺楼层：</span><span class='info'>[shop floor]</span></p>
                <p class='halfWidth'><span class='infoTitle'>店铺有效期：</span><span class='info'>[keep time]</span></p>
                <p class='halfWidth'><span class='infoTitle'>店主姓名：</span><span class='info'><input type="text" name="shop_owner" /></span></p>
                <p class='halfWidth'><span class='infoTitle'>店主手机：</span><span class='info'><input type="text" name="owner_phone" /></span></p>
                <p class='halfWidth'><span class='infoTitle'></span><span class='info'><input class="subBt" type="submit" name="infoSub" value="开店"></span></p>
            </form>
            <div class="clear"></div>
        </div>

        <h2><span class="subTitle">店铺列表</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <p class="fullWidth"><span class="infoTitle">选择店铺楼层：</span><span class="info"><span class="red_star">*</span>[shop floor]</span></p>
            <p class="fullWidth"><span class="infoTitle">店铺：</span><span class="info">[shop]</span></p>
            <p class="fullWidth"><span class="infoTitle">店铺：</span><span class="info">[shop]</span></p>
            <div class="clear"></div>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
