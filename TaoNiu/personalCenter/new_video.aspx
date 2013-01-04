﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center_pc.master" CodeBehind="new_video.aspx.vb" Inherits="TaoNiu.new_video" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
<h1>用户中心-发布视频</h1>
<div id="info">
    <ol>
        <li>发布视频动态</li>
    </ol>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
<form method="post" enctype="multipart/form-data" action="../server_page/uCenter_pc.aspx?action=new_video">
<div id="pb-photo-pick-holder" class="pb-post-section">
    <div id="pb-post-area">
        <div id="pb-audio-search-holder" class="pb-post-section" style="">
            <h3 class="pb-section-title">链接地址<span>(支持优酷/土豆/酷6网的视频发布)</span></h3>
            <input type="text" class="pb-input-text" id="pb-audio-search-input"/>
            <a id="search_video" href="#">确定</a>
        </div>
        <div id="pb-audio-preview-holder" class="pb-post-media-preview clearfix" style="display: none; ">
            <img id="pb-audio-thumb" alt=""/>
            <h3 id="pb-video-title"></h3>
        </div>
        <div id="song_rst" style="display: none; ">
            <ul></ul>
        </div>
        <div id="pb-audio-desc-holder" class="pb-post-section">
            <input type="hidden" id="v_title" name="m_title" value="" />
            <input type="hidden" id="v_palyer" name="m_palyer" value="" />
            <input type="hidden" id="v_img" name="m_img" value="" />
            <h3 class="pb-section-title">描述<span>(可不填)</span></h3>
            <textarea id="pb-text-textarea" name="pb-text-textarea" rows="15" cols="80"></textarea>
        </div>
    </div>
     <div id="post-privacy-holder">
        <div id="nav_font">视频内容</div>
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
    pCenter.getVideo();
//]]>
</script>
</asp:Content>