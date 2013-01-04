<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front.Master" CodeBehind="login.aspx.vb" Inherits="TaoNiu.login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="loginBox">
        <form id="formLogin" runat="server" method="post" action="server_page/user_service.aspx?action=login">
            <p class="inputBox"><span>用户名或邮箱</span><asp:TextBox ID="uid" runat="server"></asp:TextBox></p>
            <p class="inputBox"><span>密码</span><asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox></p>
            <p class="login">
                <asp:Button ID="btLogin" runat="server" Text="" />忘记密码？<a href="reg.aspx">免费注册</a></p>
            <p id="vdMessage">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="uid" ErrorMessage="用户名不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="password" ErrorMessage="密码不能为空"></asp:RequiredFieldValidator>
                <div class="clear"></div>
        </p>
            <%Call getRedirectURL()%>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
