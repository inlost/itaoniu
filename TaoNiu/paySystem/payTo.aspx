<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="payTo.aspx.vb" Inherits="TaoNiu.payTo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    #font_d {
        margin:100px auto;
        width:280px;
        height:180px;
        border:1px dashed #C2C2C2;
    }
    #font_div{
        width:280px;
        height:180px;
        background-color:#FFFACD;
        text-align:center;
        vertical-align:middle;
        position:relative;
        display:table-cell;
    }
	#font_show p {
	    position:static;
	    +position:absolute;
	    top:50%;
	}
	#font_show p b{
	    position:static;
	    +position:relative;
	    top:-50%;
	    left:-50%;
	    font-size:13px;
	    color:black;
	}
    #Label1{display:none;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="font_d">
    <div id="font_div">
        <div id="font_show"><p><b>页面跳转中，请稍候......</b></p></div>
    </div>
    </div>
    </form>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</body>
</html>
