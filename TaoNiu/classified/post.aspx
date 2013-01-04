<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/classified.Master" CodeBehind="post.aspx.vb" Inherits="TaoNiu.post" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="theEmrty" class="afterClear">
        <h2><%getPost("title")%></h2>
        <div id="post_content">
            <ul id="infos">
                <li><span>联系电话：</span><%getPost("phone")%></li>
                <li><span>联 系 人：</span><%getPost("name")%></li>
                <li><span>发布时间：</span><%getPost("date")%></li>
            </ul>
            <div id="otherInfo" class="hidden">
                <%getPost("other")%>
            </div>
            <div id="postInner">
                <%getPost("content")%>
            </div>
        </div>
        <div id="sidebar">
        
        </div>
    </div>
    <script type="text/javascript">
    //<![CDATA[
        cat.postInformation();
        cat.postAct();
    //]]>
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">

</asp:Content>
