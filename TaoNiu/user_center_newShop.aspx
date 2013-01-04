<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_newShop.aspx.vb" Inherits="TaoNiu.user_center_newShop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>开一家新店</h1>
        <div id="info">
            <ol>
                <li>1.填写店铺信息</li>
                <li>2.开店成功</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以开设一家新的店铺。</p></div>
        <div class="infoBox">
            <form name="newShop" method ="post" action="server_page/shop_goods.aspx?action=newShop">
                <% Call writeNewShopForm()%>
            </form>
        </div>
    </div> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
<script type="text/javascript" charset="utf-8" src="include/editor/kindeditor-min.js"></script>
<script type="text/javascript">
    //<![CDATA[
    userCenter.setRangeChangeListen();
    KE.show({
        id: 'shopIntro',
        imageUploadJson: 'img_upload.aspx',
        allowFileManager: false
    });
    //]]>
</script>
</asp:Content>
