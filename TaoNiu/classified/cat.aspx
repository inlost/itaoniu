<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/classified.Master" CodeBehind="cat.aspx.vb" Inherits="TaoNiu.cat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="navagation">
        <ul id="nav" class="afterClear">
            <li><a href="index.aspx">首页>></a></li>
            <li><a href="cat.aspx?id=<% witeId() %>"><%witeCatName()%></a></li>
        </ul>
        <div id="catList">

        </div>
    </div>
    <div id="content">
        <%getList()%>
    </div>
    <div id="pageLink">
        <%getPageLink()%>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
<script type="text/javascript">
    //<![CDATA[
    cat.hoverItem();
    cat.postTle();
    //]]>
</script>

</asp:Content>
