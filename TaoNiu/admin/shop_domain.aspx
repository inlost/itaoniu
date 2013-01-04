<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="shop_domain.aspx.vb" Inherits="TaoNiu.shop_domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div class="contentBox">
        <form id="form1" runat="server">
        <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" 
            DataTextField="shop_domain" DataValueField="id" AutoPostBack="True" 
            Width="229px"></asp:ListBox>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:conStr %>" 
            SelectCommand="SELECT [id], [shop_domain] FROM [shops]"></asp:SqlDataSource>
        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank">查看店铺</asp:HyperLink>
        <br />
        <asp:Button ID="Button1" runat="server" Text="已经处理" />
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
