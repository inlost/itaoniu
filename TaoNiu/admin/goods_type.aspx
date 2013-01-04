<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="goods_type.aspx.vb" Inherits="TaoNiu.goods_type" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <form id="form1" runat="server">
    <div class="contentBox">
        <div id="goods_type">
            <p>商品类型</p>
            <asp:ListBox ID="listGoodType" runat="server" DataSourceID="SqlDataSource1" 
                DataTextField="name" DataValueField="id"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT * FROM [goods_type]"></asp:SqlDataSource>
            <p>
                <asp:Button ID="rmGoodType" runat="server" Text="删除选中分类" /><asp:TextBox ID="TextBox1"
                    runat="server"></asp:TextBox><asp:Button ID="addGoodType" runat="server" Text="添加" /></p>
        </div>
        <div id="sale_type">
            <p>商品销售类型</p>
            <asp:ListBox ID="listSaleType" runat="server" DataSourceID="SqlDataSource2" 
                DataTextField="name" DataValueField="id"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT * FROM [goods_sale_type]"></asp:SqlDataSource>
            <p>
                <asp:Button ID="rmSaleType" runat="server" Text="删除选中分类" /><asp:TextBox ID="TextBox2"
                    runat="server"></asp:TextBox><asp:Button ID="addSaleType" runat="server" Text="添加" /></p>
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
