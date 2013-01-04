<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="range.aspx.vb" Inherits="TaoNiu.range" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <form id="form1" runat="server">
<div class="contentBox">
    <h2>送货范围管理</h2>
    <p><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1"
            runat="server" Text="添加" style="height: 21px" />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:Button ID="Button4" runat="server" Text="添加" />
    </p>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:conStr %>" 
        
        SelectCommand="SELECT [id], [range] FROM [send_range] WHERE ([father] = @father)">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="father" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:conStr %>" 
        SelectCommand="SELECT [id], [range] FROM [send_range] WHERE ([father] = @father)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ListBox1" Name="father" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" 
        DataTextField="range" DataValueField="id" Height="285px" Width="161px" 
        AutoPostBack="True"></asp:ListBox>
    <asp:ListBox ID="ListBox2" runat="server" Height="284px" 
        style="margin-top: 0px" Width="208px" DataSourceID="SqlDataSource2" 
        DataTextField="range" DataValueField="id"></asp:ListBox>
    <asp:Button ID="Button3" runat="server" Text="删除选中" />
    <br />
    <asp:Button ID="Button2" runat="server" Text="删除选中" />
</div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
