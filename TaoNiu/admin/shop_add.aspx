<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="shop_add.aspx.vb" Inherits="TaoNiu.shop_add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <form id="form1" runat="server">
    <div class="contentBox">
        <p>店主<asp:TextBox ID="shop_owner" runat="server"></asp:TextBox></p>
        <p>店名<asp:TextBox ID="shop_name" runat="server"></asp:TextBox></p>
        <p>介绍</p><p><asp:TextBox ID="shop_introduce" runat="server" TextMode="MultiLine"></asp:TextBox></p>
        <p>联系方式</p><p><asp:TextBox ID="shop_address" runat="server" TextMode="MultiLine"></asp:TextBox></p>
        <p>认证：<asp:CheckBoxList ID="shop_certification" runat="server" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="id" 
                RepeatColumns="5">
            </asp:CheckBoxList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT [id], [name] FROM [shop_certification_type]">
            </asp:SqlDataSource>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" Text="添加" /></p>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
