<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_tools_trusteeship.aspx.vb" Inherits="TaoNiu.user_center_tools_trusteeship" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>店铺托管</h1>
        <div id="info">
            <ol>
                <li>查看托管</li>
                <li>使用托管</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可进行店铺托管的管理。</p></div>
        <h2><span class="subTitle">托管列表</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <%Call getTuList()%>
        </div>
        <h2><span class="subTitle">官方托管</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <form action="server_page/shop_goods.aspx?action=addTu" method="post">
                <p>店铺列表：<select id="shopList" name="shopId"><%Call getShopList()%></select> 托管类型：<select id="tuType" name="tuType"><% getTuType() %></select></p>         
                <p>托管人员选择<select id="tuId" name="tuId"><%Call getMember()%></select></p>
                <input type="submit" id="subForm" class="subBt" value ="提交" />     
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
