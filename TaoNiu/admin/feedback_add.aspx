<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="feedback_add.aspx.vb" Inherits="TaoNiu.feedback_add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
<form id="form1" runat="server">
 <p><span>反馈问题类型：</span><asp:DropDownList runat="Server" ID="questiontype">
     <asp:ListItem Value="0">不满意</asp:ListItem>
     <asp:ListItem Value="1">满意</asp:ListItem>
     </asp:DropDownList></p>
     <p><span>问题：</span><asp:TextBox ID="questiont" TabIndex="1" runat="server"></asp:TextBox></p>
     <p><asp:Button ID="btnSumbit" runat="server" Text="提交" /></p>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
