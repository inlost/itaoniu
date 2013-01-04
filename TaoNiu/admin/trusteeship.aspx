<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="trusteeship.aspx.vb" Inherits="TaoNiu.trusteeship" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div class="contentBox">
        <form id="form1" runat="server">
            <div>
                <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" 
                    DataSourceID="SqlDataSource1" DataTextField="taoniuId" DataValueField="id" 
                    Height="113px" Width="146px"></asp:ListBox>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:conStr %>" 
                    SelectCommand="SELECT [id], [taoniuId] FROM [trusteeship]">
                </asp:SqlDataSource>
                <asp:Button ID="Button1" runat="server" Text="删除所选" />
            </div>
            <div>
                <p>qq<asp:TextBox ID="TextQQ" runat="server"></asp:TextBox></p>
                <p>桃牛账号<asp:TextBox ID="TextTn" runat="server"></asp:TextBox></p>
                <asp:Button ID="Button2" runat="server" Text="添加" />
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
