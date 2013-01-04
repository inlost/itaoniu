<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/classified.Master" CodeBehind="newPost.aspx.vb" Inherits="TaoNiu.newPost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
<div id="navagation">
        <ul id="nav" class="afterClear">
            <li><a href="index.aspx">首页>></a></li>
            <li><a href="#">信息发布</a></li>
        </ul>
        <div id="catList">

        </div>
    </div>
    <form method="post" action="../server_page/classified.aspx?action=newPost" enctype ="multipart/form-data" >
        <div id="catChoise">
            <select name="cat1" deep="1"></select>
            <select name="cat2" deep="2"></select>
            <select name="cat3" deep="3"></select>
        </div>
        <div id="thePost">
            <p><span class="star_red">*</span>标题：<input type="text" name="title" id="title" /></p>
            <input type="text" name="postContent" id="postContent" />
            <p><span class="star_red">*</span>手机/电话 ：<input type="text" name="phone" id="phone" /></p>
            <p><span class="star_red">*</span>联系人姓名：<input type="text" name="name" id="name" /></p>
            <p><span></span>图片(可为空)：<input type="file"  name="img" id="img" /></p>
            <p><span></span>价格(可为空)：<input type="text" name="price" id="price" /></p>
            <input type="submit" id="fabu" class="fabu1" value="马上发布" />
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
<script type="text/javascript" charset="utf-8" src="../include/editor/kindeditor-min.js"></script>
<script type="text/javascript">
    //<![CDATA[
    classified.setPostCat();
    KE.show({
        id: 'postContent',
        imageUploadJson: 'img_upload.aspx',
        allowFileManager: false
    });
    //]]>
</script>
</asp:Content>
