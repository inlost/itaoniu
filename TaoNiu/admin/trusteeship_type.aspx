<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="trusteeship_type.aspx.vb" Inherits="TaoNiu.trusteeship_type" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div class="contentBox">
        <form id="form1" runat="server">
            <div>
                <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" 
                    DataSourceID="SqlDataSource1" DataTextField="typeName" DataValueField="id" 
                    Height="113px" Width="146px"></asp:ListBox>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:conStr %>" 
                    SelectCommand="SELECT [id], [typeName] FROM [trusteeship_type]">
                </asp:SqlDataSource>
                <asp:Button ID="Button1" runat="server" Text="删除所选" />
            </div>
            <div>
                <p>类型名称<asp:TextBox ID="TextName" runat="server"></asp:TextBox></p>
                <p>开始时间（0-24的整数）<asp:TextBox ID="TextStart" runat="server"></asp:TextBox></p>
                <p>结束时间（0-24的整数）<asp:TextBox ID="TextEnd" runat="server"></asp:TextBox></p>
                <p>费用（整数）<asp:TextBox ID="TextFee" runat="server"></asp:TextBox></p>
                <asp:Button ID="Button2" runat="server" Text="添加" />
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
