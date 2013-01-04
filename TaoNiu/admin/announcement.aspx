<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="announcement.aspx.vb" Inherits="TaoNiu.announcement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div class="contentBox">
        <form id="form1" runat="server">
            <div>
                <h3>公告管理</h3>
                <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" 
                    DataSourceID="SqlDataSource1" DataTextField="title" DataValueField="id"></asp:ListBox>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:conStr %>" 
                    SelectCommand="SELECT [title], [id] FROM [announcement]">
                </asp:SqlDataSource>
                <asp:Button ID="Button1" runat="server" Enabled="False" Text="删除选中的公告" />
            </div>
            <div>
                <h3>发布公告</h3>
                <p>标题：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></p>
                <p>公告内容：</p>
                <asp:TextBox ID="TextBox2" style="height:200px;width:90%;" runat="server" TextMode="MultiLine"></asp:TextBox>
                <p></p>
                <asp:Button ID="Button2" runat="server" Text="发布" />
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
