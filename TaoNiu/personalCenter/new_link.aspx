<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center_pc.master" CodeBehind="new_link.aspx.vb" Inherits="TaoNiu.new_link" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
<h1>用户中心-发布链接</h1>
<div id="info">
    <ol>
        <li>发布链接动态</li>
    </ol>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
<form method="post" enctype="multipart/form-data" action="../server_page/uCenter_pc.aspx?action=new_link">
<div id="pb-post-area">
    <div id="pb-link-title-holder" class="pb-post-section">
        <h3 class="pb-section-title">标题<span>(可不填)</span></h3>
        <input id="pb-link-title-input" class="pb-input-text" name="pb-link-title-input"/>
    </div>
    <div id="pb-link-url-holder" class="pb-post-section">
        <h3 class="pb-section-title">链接地址</h3>
        <input id="pb-link-url-input" class="pb-input-text" name="link_address"/>
    </div>
    <div id="post-privacy-holder">
        <div id="nav_font">链接内容</div>
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
</asp:Content>
