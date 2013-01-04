<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="shop.aspx.vb" Inherits="TaoNiu.shop1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type='text/css' rel='stylesheet' href="<%Call shoop_Info("style")%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
    <div id="inCatNav">
        <%Call shoop_Info("nav")%>
    </div>
    <div id="shopInfo">
        <h2><a href="<%Call shoop_Info("link")%>"><%Call shoop_Info("name")%></a></h2>
        <div><%Call shoop_Info("introduce")%></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="sideBar">
        <%Call getSideBar()%>
    </div>
    <div id="goodBox">
        <%If Request.Params("shelf") = Nothing Then
                Call getShelfList()
            Else
                Call getShelfList(Request.Params("shelf"))
                Call getPageLink()
            End If%>
    </div>
    <div class="clear"></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
  <script type="text/javascript">
        //<![CDATA[
              buySale.shopTitle();
        //]]>
    </script>   
<div id="lianMeng">
    <div class="leftSide">
        <h2>店铺联盟</h2>
        <p><%Call getLianMengAdd()%></p>
    </div>
    <div class="rightSide">
        <ul id='lmList'><%Call getLianMengList()%><div class ="clear"></div></ul>
    </div>
    <div class="clear"></div>
</div>
</asp:Content>
