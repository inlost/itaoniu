<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/mall.Master" CodeBehind="mall_setings.aspx.vb" Inherits="TaoNiu.mall_setings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>商城设置</h1>
        <div id="info">
            <ol>
                <li>商城设置查看</li>
                <li>商城设置修改</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以对商城的设置进行浏览管理。</p></div>
        <h2><span class="subTitle">商城设置</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <form method="post" action="">
                <p class='halfWidth'><span class='infoTitle'>商城名称：</span><span class='info'><input type="text" name="mall_name" /></span></p>
                <p class='halfWidth'><span class='infoTitle'>商城地址：</span><span class='info'><input type="text" name="shop_owner" /></span></p>
                <p class='halfWidth'><span class='infoTitle'>商城营业时间：</span><span class='info'>[working time]</span></p>
                <p class='halfWidth'><span class='infoTitle'>商城配送时间：</span><span class='info'>[send time]</span></p>
                <p class='halfWidth'><span class='infoTitle'>商城电话：</span><span class='info'><input type="text" name="owner_phone" /></span></p>
                <p class='halfWidth'><span class='infoTitle'></span><span class='info'><input class="subBt" type="submit" name="infoSub" value="开店"></span></p>
            </form>
            <div class="clear"></div>
        </div>
        <h2><span class="subTitle">商城楼层设置</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <form method="post" action="">
                <p class='halfWidth'><span class='infoTitle'>楼层名称：</span><span class='info'><input type="text" name="mall_name" /></span></p>
                <p class='halfWidth'><span class='infoTitle'></span><span class='info'><input class="subBt" type="submit" name="infoSub" value="添加"></span></p>
            </form>
            <p class='halfWidth'><span class='infoTitle'>楼层：</span><span class='info'>[floor]</span></p>
            <p class='halfWidth'><span class='infoTitle'>楼层：</span><span class='info'>[floor]</span></p>
            <p class='halfWidth'><span class='infoTitle'>楼层：</span><span class='info'>[floor]</span></p>
            <p class='halfWidth'><span class='infoTitle'>楼层：</span><span class='info'>[floor]</span></p>
            <p class='halfWidth'><span class='infoTitle'>楼层：</span><span class='info'>[floor]</span></p>
            <p class='halfWidth'><span class='infoTitle'>楼层：</span><span class='info'>[floor]</span></p>
            <div class="clear"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
