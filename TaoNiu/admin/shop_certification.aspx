<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="shop_certification.aspx.vb" Inherits="TaoNiu.shop_certification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <form id="form1" runat="server">
    <div class="contentBox">
        <p>
        <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" 
            DataTextField="apply_type" DataValueField="id" AutoPostBack="True" 
                Height="180px" Width="142px"></asp:ListBox>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:conStr %>" 
            
                SelectCommand="SELECT [id], [apply_type] FROM [shop_certion_apply] WHERE ([apply_status] = @apply_status)">
            <SelectParameters>
                <asp:Parameter DefaultValue="1" Name="apply_status" Type="Int16" />
            </SelectParameters>
        </asp:SqlDataSource>
            <asp:Label ID="Label1" runat="server" Text="认证类型"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="认证类型"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label3" runat="server" Text="认证内容"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="认证内容"></asp:Label>
        </p>
        <p>    <asp:Button ID="Button1" runat="server" Text="通过" style="height: 21px" />
    <asp:Button ID="Button2" runat="server" Text="不通过" /></p>
    </div> 
    </form> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
