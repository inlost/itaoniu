<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_tools_type.aspx.vb" Inherits="TaoNiu.user_center_tools_type" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>录入委托</h1>
        <div id="info">
            <ol>
                <li>查看委托</li>
                <li>撤销委托</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可进行店铺商品委托录入人员进行管理。</p></div>
        <h2><span class="subTitle">委托列表</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <% Call getWeiTuoList()%>
        </div>
        <h2><span class="subTitle">添加委托</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <form action="server_page/shop_goods.aspx?action=addWt" method="post">
                <p>店铺列表：<select id="shopList" name="shopId"><%Call getShopList()%></select> 托管人账户：<input type="text" name="uName" /></p>
                <input type="submit" id="subForm" class="subBt" value ="提交" />     
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
