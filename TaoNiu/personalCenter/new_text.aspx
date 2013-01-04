<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center_pc.master" CodeBehind="new_text.aspx.vb" Inherits="TaoNiu.new_text" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
<h1>用户中心-发布文字</h1>
<div id="info">
    <ol>
        <li>发布文字动态</li>
    </ol>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
<form method="post" enctype="multipart/form-data" action="../server_page/uCenter_pc.aspx?action=new_text">
    <div id="pb-post-area">
        <div id="pb-text-title-holder" class="pb-post-section">
            <h3 class="pb-section-title">标题<span>(可不填)</span></h3>
            <input tabindex="1" type="text" name="title" id="pb-text-title" />
        </div>
        <div id="pb-text-post-holder" class="pb-post-section">
            <h3 class="pb-section-title">内容</h3>
            <div id="pb-text-upload-pic">
                <div id="pb-text-font">添加图片:</div>
                <input id="File" type="file" name="imge" />
            </div>
            <textarea id="pb-text-textarea" name="pb-text-textarea" rows="15" cols="80"></textarea>
        </div> 
    </div> 
    <div id="post-privacy-holder">
        <div id="nav_font">文章内容</div>
        <select id="post-privacy-select" name="privacy-select">
            <option value="0">所有人可见</option>
            <option value="2">仅自己可见</option>
        </select>
    </div>
    <div id="pb-action-holder">
         <div id="action_input"><input id="Submit1" type="submit" value="发布" /></div>
        <div id="action_A"><a class="pure-button red" id="pb-cancel" href="default.aspx?at=pc">取消</a></div>
    </div>
</form>
<script type="text/javascript">
    //<![CDATA[
    mySns.submitTest();
    //]]>
</script>

</asp:Content>
