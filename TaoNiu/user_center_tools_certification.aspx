<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_tools_certification.aspx.vb" Inherits="TaoNiu.user_center_tools_certification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>店铺认证</h1>
        <div id="info">
            <ol>
                <li>查看认证</li>
                <li>提交认证</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可进行店铺认证的管理。</p></div>
        <h2><span class="subTitle">卖家信用概览</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">

        </div>
        <h2><span class="subTitle">审核中的认证</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <%getCeList() %>
        </div>
        <h2><span class="subTitle">提交新认证</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <form action="server_page/shop_goods.aspx?action=addCe" method="post">
                <p>店铺列表：<select id="shopList" name="shopId"><%Call getShopList()%></select> 认证类型：<select id="ceType" name="ceType"><%getCeType() %></select></p>         
                <p>认证资料编辑</p>
                <input type="text" id="ceContent"  style='width:95%;height:400px;' name="shop_introduce" />  
                <br />
                <input type="submit" id="subForm" class="subBt" value ="提交" />     
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
<script type="text/javascript" charset="utf-8" src="include/editor/kindeditor-min.js"></script>
<script type="text/javascript">
    //<![CDATA[
    KE.show({
        id: 'ceContent',
        imageUploadJson: 'img_upload.aspx',
        allowFileManager: false
    });
    //]]>
</script>
</asp:Content>
