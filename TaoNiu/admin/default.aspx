<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="default.aspx.vb" Inherits="TaoNiu._default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
table{font-size:12px;}

.tb_1{
 background-color:#ffffff;
 border:1px solid #C9DDF0;
 margin:5px 0px 5px 5px;
 width:100%;
 float:left;
}

 .tb_2{
 width:100%;
 background-color:#ffffff;
 border:1px solid #C9DDF0;
 margin:15px auto;
 clear:both;
}

.tb_2 td{border-bottom: 1px dotted #C9DDF0;padding-left:6px;}

 .tb_2_b{
 width:100%;
 background-color:#ffffff;
 border:1px solid #C9DDF0;
 margin:0px auto;
 clear:both;
}

 .tb_3{
 background-color:#ffffff;
 border:1px solid #C9DDF0;
 margin:15px auto;
 padding:5px;
 clear:both;
}


.tr_1{
 padding-left:5px;
 padding-top:5px;
 font-weight:bold;
 font-size:14px;
 height:24px;
 text-align:center;
 background-color:#F3F9FE;
}

.tr_2{
 text-align:center;
}



.td_1{
 text-align:left;
}
.td_2{
 text-align:center;
}
.td_3{
 text-align:right;
}

.right{
	display:inline;
	float:right
}
.clear{
	clear:both;
	height:0px;
}
.wrap{
	width:950px;;
	margin:0 auto;
}
.bord{
	border:#b0bec7 1px solid;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div class="contentBox">
    <form id="Form1" method="post" runat="server">
<table class="tb_2_b">
<tr class="tr_1"> 
      <td><asp:label ID="systitle" runat="server" /></td>
    </tr>
</table>

<table class="tb_2">


<tr class="tr_1"> 
      <td colspan="2">写入权限</td>
    </tr>
    <tr>
      <td>空间是否支持写入：</td>
      <td><asp:label ID="obj_write" runat="server" /><br />
        <br />
        写入权限说明：
有些空间商的空间看起来用一些asp.net探针运行正常，其实只是验证了asp.net对空间的读取权限，asp.net的写入权限可能没有的，要是不支持差不多所有使用的Access数据库的asp.net程序用不了，也生成不了静态页面。。如果写入权限为支持的话基本这个空间才可以正常使用。</td>
    </tr>
	
	<tr class="tr_1"> 
      <td colspan="2">基本信息</td>
    </tr>
    <tr>
      <td >服务器名称：</td>
      <td><asp:label ID="ServerName" runat="server" /></td>
    </tr>
    <tr> 
      <td width="150" >操作系统 ：</td>
      <td><asp:label ID="ServerVer" runat="server" /></td>
    </tr>
    <tr>
      <td >服务器IP：</td>
      <td><asp:label ID="ServerIP" runat="server" /></td>
    </tr>
    <tr>
      <td >服务器域名：</td>
      <td><asp:label ID="ServerDomain" runat="server" /></td>
    </tr>
    <tr>
      <td >服务端脚本执行超时：</td>
      <td><asp:label ID="ServerOutTime" runat="server" />秒</td>
    </tr>
    <tr>
      <td >服务器现在时间：</td>
      <td><asp:label ID="ServerNow" runat="server" /></td>
    </tr>
	
    <tr>
      <td >Session总数：</td>
      <td><asp:label ID="ServerSessionTotal" runat="server" /></td>
    </tr>
    <tr>
      <td >Application总数：</td>
      <td><asp:label ID="ServerApplicationTotal" runat="server" /></td>
    </tr>
	
	<tr> 
      <td>IIS版本 ：</td><td><asp:label ID="IISVer" runat="server" /></td>
    </tr>
    <tr> 
      <td>.NET Framework 版本 ：</td><td><asp:label ID="NetVer" runat="server" /></td>
    </tr>
	
    <tr> 
      <td>相对路径 ：</td><td><asp:label ID="ProPath" runat="server" /></td>
    </tr>
    <tr> 
      <td>物理路径 ：</td><td><asp:label ID="ProPath_2" runat="server" /></td>
    </tr>
    <tr> 
      <td>运行时间 ：</td><td><asp:label ID="ServerRunTime" runat="server" />小时</td>
    </tr>
    <tr class="tr_1"> 
      <td colspan="2">系统组件信息</td>
    </tr>
    <tr>
      <td>Access数据库组件 ：</td><td><asp:label ID="Obj_Access" runat="server" /></td>
    </tr>
    <tr>
      <td>FSO文件操作组件 ：</td><td><asp:label ID="Obj_Fso" runat="server" /></td>
    </tr>
    
	
	<tr class="tr_1"> 
      <td colspan="2">邮件组件信息</td>
    </tr>
	
	<tr>
      <td>JMAIL邮件发送组件 ：</td><td><asp:label ID="Obj_Jmail" runat="server" /></td>
    </tr>
    <tr>
      <td>CDONTS邮件发送组件 ：</td><td><asp:label ID="Obj_Cdonts" runat="server" /></td>
    </tr>
	<tr class="tr_1"> 
      <td colspan="2">图像组件</td>
    </tr>
	<tr>
      <td>AspJpeg组件 ：</td><td><asp:label ID="Obj_AspJpeg" runat="server" /></td>
    </tr>
	
	<tr class="tr_1"> 
      <td colspan="2">文件上传组件</td>
    </tr>
	<tr><td>ASPUpload上传组件 ：</td><td><asp:label ID="obj_aspupload" runat="server" /></td>
    </tr>

	<tr class="tr_1"> 
      <td colspan="2">自定义组件</td>
    </tr>
	
    <tr><td>自定义组件查询：</td><td><INPUT TYPE="text" NAME="SelObj" id="SelObj" runat="server">&nbsp;<asp:button id="SelfObjChk" runat="server" Text="检测" OnClick="SelfObjChk_Click"></asp:button><asp:label ID="Obj_SelfObj" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;此处必须使用组件的ProgId或ClassId来检测</td>
    </tr>
	
	<tr class="tr_1"> 
      <td colspan="2">浏览者信息</td>
    </tr>
	
    <tr><td>浏览者ip地址：</td>
      <td>&nbsp;<asp:label ID="Brower_IP" runat="server" />&nbsp;</td>
	  </tr>

    <tr><td>浏览者操作系统：</td>
      <td>&nbsp;<asp:label ID="Brower_OSVer" runat="server" />&nbsp;</td>
    </tr>

    <tr><td>浏览器：</td>
      <td>&nbsp;<asp:label ID="Brower_Brower" runat="server" />&nbsp;</td>
    </tr>
	  
	      <tr><td>浏览器版本：</td>
      <td>&nbsp;<asp:label ID="Brower_BrowerVer" runat="server" />&nbsp;</td>
	  </tr>
	  
	      <tr><td>JavaScript：</td>
      <td>&nbsp;<asp:label ID="Brower_Javscript" runat="server" />&nbsp;</td>
	  </tr>
	  
	      <tr><td>VBScript：</td>
      <td>&nbsp;<asp:label ID="Brower_VBScript" runat="server" />&nbsp;</td>
	  </tr>
	  
	      <tr><td>JavaApplets：</td>
      <td>&nbsp;<asp:label ID="Brower_JavaApplets" runat="server" />&nbsp;</td>
	  </tr>
	  
	      <tr><td>Cookies：</td>
      <td>&nbsp;<asp:label ID="Brower_Cookies" runat="server" />&nbsp;</td>
	  </tr>
	  
	      <tr><td>语言：</td>
      <td>&nbsp;<asp:label ID="Brower_Language" runat="server" />&nbsp;</td>
	  </tr>
	  
	      <tr><td>Frames（分栏）:</td>
      <td>&nbsp;<asp:label ID="Brower_Frame" runat="server" />&nbsp;</td>
	  </tr> 
</table>
</form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
