<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center_pc.master" CodeBehind="new_img.aspx.vb" Inherits="TaoNiu.new_img" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
<h1>用户中心-发布图片</h1>
<div id="info">
    <ol>
        <li>发布图片动态</li>
    </ol>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
<form method="post" enctype="multipart/form-data" action="../server_page/uCenter_pc.aspx?action=new_images">
<div id="pb-photo-pick-holder" class="pb-post-section holder_music">
    <div id="pb-photo-flash-holder">
        <input id="File" type="file" name="images" />
    </div>
    <div id="pb-photo-upload-file-status"></div>
    <div id="pb-photo-file-tip">JPG, GIF, PNG或BMP. 不超过8MB</div>
    <div id="pb-photo-upload-total-status"></div>
     <div id="post-privacy-holder">
        <div id="nav_font">图片内容</div>
        <select id="post-privacy-select" name="privacy-select">
            <option value="0">所有人可见</option>
            <option value="2">仅自己可见</option>
        </select>
    </div>
    <div id="pb-action-holder">
        <div id="action_input"><input id="Submit1" type="submit" value="发布" /></div>
        <div id="action_A"><a class="pure-button red" id="pb-cancel" href="default.aspx?at=pc">取消</a></div>
    </div>
</div>
</form>
<script type="text/javascript">
    //<![CDATA[
    mySns.submitTest();
    //]]>
</script>
</asp:Content>
