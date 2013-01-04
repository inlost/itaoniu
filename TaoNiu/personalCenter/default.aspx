<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center_pc.master" CodeBehind="default.aspx.vb" Inherits="TaoNiu._default2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
<h1>用户中心</h1>
<div id="info">
    <ol>
        <li>查看好友最新动态</li>
        <li>发布自己的动态</li>
    </ol>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
<div id="functions">
    <div class="pb-avatar">
        <a class="blog-avatar" href="http://inlost.diandian.com" style="background-image:url(http://farm1.img.libdd.com/156/C87DB09F2BDD86536F79B35F1FFB169C_63_63.jpg)">那个白</a>
    </div>
    <div class="pb-triangle"></div>
    <div class="pb-action-holder">
        <div class="pb-triangle"></div>
        <div class="pb-action" id="pb-action">
            <ul class="clearfix">
                <li>
                    <a class="text" href="new_text.aspx?at=pc">文字</a>
                </li>
                <li>
                    <a class="photo" href="new_img.aspx?at=pc">照片</a>
                </li>
                <li>
                    <a class="music" href="new_music.aspx?at=pc">音乐</a>
                </li>
                <li>
                    <a class="link" href="new_link.aspx?at=pc">链接</a>
                </li>
                <li>
                    <a class="video" href="new_video.aspx?at=pc">视频</a>
                </li>
            </ul>
        </div>
    </div>
</div>
<div id="timeLineShow">
    <%Call getTimeLine()%>
</div>
<script type="text/javascript">

//<![CDATA[
    pCenter.showVideo();

//]]>

</script>

</asp:Content>
