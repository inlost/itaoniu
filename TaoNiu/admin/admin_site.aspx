<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="admin_site.aspx.vb" Inherits="TaoNiu.admin_site" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="contentBox">
        <form id="form1"  method="post" runat="server" action="server_page/main.aspx?action=siteInfo">
            <p><span>站点名称</span><asp:TextBox ID="site_name" runat="server"></asp:TextBox></p>
            <p><span>站点地址</span><asp:TextBox ID="site_url" runat="server"></asp:TextBox></p>
            <p><span>备案号</span><asp:TextBox ID="site_icp" runat="server"></asp:TextBox></p>
            <p><span>smtp服务器</span><asp:TextBox ID="site_smtp" runat="server"></asp:TextBox></p>
            <p><span>smtp用户名</span><asp:TextBox ID="site_smtp_u" runat="server"></asp:TextBox></p>
            <p><span>smtp密码</span><asp:TextBox ID="site_smtp_p" runat="server" 
                    TextMode="Password"></asp:TextBox></p>
            <p><span>维护工程师邮箱</span><asp:TextBox ID="site_support" runat="server"></asp:TextBox></p>
            <hr />
            <asp:Button ID="Button1" runat="server" Text="提交" />
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
