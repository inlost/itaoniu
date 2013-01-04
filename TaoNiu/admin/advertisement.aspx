<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="advertisement.aspx.vb" Inherits="TaoNiu.advertisement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="contentBox">
        <form id="form1"  method="post" runat="server">
        <div>
            <h3>广告管理</h3>
            <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" 
                DataTextField="adTitle" DataValueField="id"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT [adTitle], [id] FROM [advertisement]">
            </asp:SqlDataSource>
            <br />
            <asp:Button ID="Button2" runat="server" Text="删除选中" />
        </div>
        <div>
            <h3>广告发布</h3>
            <p>广告类型：</p>
            <asp:ListBox ID="adType" runat="server">
                <asp:ListItem Value="uCenter">用户中心推送广告</asp:ListItem>
                <asp:ListItem Value="sideBar">边栏广告</asp:ListItem>
            </asp:ListBox><br />
            <p>广告标题：<asp:TextBox ID="adTitle" runat ="server"></asp:TextBox></p>
            <p>广告内容：</p>
            <asp:TextBox ID="ad" runat="server" Height="300px" Width="90%"></asp:TextBox><br />
            <asp:Button ID="Button1" runat="server" Text="发布" />
        </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
<script type="text/javascript" charset="utf-8" src="../include/editor/kindeditor.js"></script>
<script type="text/javascript" >
    KE.show({
        id: 'ctl00_ContentContent_ad',
        imageUploadJson: 'img_upload.aspx',
        allowFileManager: false
    });
</script>
</asp:Content>
