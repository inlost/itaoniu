<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="TaoNiu.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #login span{display:block;width:60px;float:left;}
        #login{	border:1px solid #94d3fe;background:#f1faff;}
        #login p{float:left;}
        #login hr{height:1px;border:none;clear:both;border-top:1px dashed #CCC;}
        #login img{position:relative ;top:186px;}
        #login h2{text-align:center;}
    </style>
</head>
<body style="text-align:center ;">
    <form id="form1" runat="server" method="post" action="server_page/main.aspx?action=admLogin">
    <div style="width:400px;text-align:left;margin:0 auto;padding-top:150px;">
        <div id="login" style ="padding:15px;">
            <h2>administrator login</h2>
            <hr />
            <p><span>ID：</span><asp:TextBox ID="id" runat="server"></asp:TextBox></p>
            <p><span>Pass:</span><asp:TextBox ID="pass" runat="server" TabIndex="1" 
                    TextMode="Password"></asp:TextBox></p>
            <p><span>Safe:</span><asp:TextBox ID="safe" runat="server" TabIndex="2" 
                    TextMode="Password"></asp:TextBox></p>
            <p><span>VdCode:</span><asp:TextBox ID="vdCode" runat="server" TabIndex="3"></asp:TextBox></p>
            <asp:Image id="ImageVd" runat="server" ImageUrl="~/server_page/vdImg.aspx" />
            <hr />
            <asp:Button ID="btLogin" runat="server" Text="登录" TabIndex="4" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="id" ErrorMessage="id is null!" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="pass" ErrorMessage="pass is null!"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="safe" ErrorMessage="safe is null!"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="vdCode" ErrorMessage="vd is null!"></asp:RequiredFieldValidator>
        </div>
    </div>
    </form>
</body>
</html>
