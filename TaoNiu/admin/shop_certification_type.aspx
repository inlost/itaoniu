<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="shop_certification_type.aspx.vb" Inherits="TaoNiu.shop_certification_type" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <form id="form1" runat="server">
    <div class="contentBox">
        <p><asp:ListBox ID="ListBox1" style="width:200px;" runat="server" DataSourceID="SqlDataSource1" 
                DataTextField="name" DataValueField="id"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT [id], [name] FROM [shop_certification_type]">
            </asp:SqlDataSource>
        </p>
        <p>
            <asp:Button ID="Button2" runat="server" Text="删除所选" /></p>
        <p>添加新的认证类型<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></p>
        <p>介绍：</p>
           <p> <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox></p>
           <p>费用：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></p>
        <p><asp:Button ID="Button1" runat="server" Text="添加" /></p>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
