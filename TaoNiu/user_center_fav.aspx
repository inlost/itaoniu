<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_fav.aspx.vb" Inherits="TaoNiu.user_center_fav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1><%Response.Write(Session("niceName"))%>的收藏</h1>
        <div id="info">
            <ol>
                <li>查看收藏</li>
                <li>管理收藏</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看管理您收藏。</p></div>
          <h2><span class="subTitle">商品收藏</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">
                <%Call getGoods()%>
            </div>
          <h2><span class="subTitle">店铺收藏</span><span class="subBt"><a href="#"></a></span></h2>
            <div class ="infoBox">
                <%Call getLianMengList()%>
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
