<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/agent.Master" CodeBehind="default.aspx.vb" Inherits="TaoNiu._default3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>商品录入</h1>
        <div id="info">
            <ol>
                <li>商品录入</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以对委托录入店铺的商品进行操作。</p></div>
        <%If Request.Params("action") = Nothing Or Request.Params("action") = "add---0" Then%>
        <h2><span class="subTitle">录入新商品</span><span class="subBt"><a href="?at=goods&action=management"></a></span></h2>
            <div class="infoBox">
                <form method ="post" enctype="multipart/form-data" action="server_page/shop_goods.aspx?action=goods_add" name="newGoods">
                <div id="goods_add_1">
                    <h3>请选择商品所属分类</h3>
                    <select name="cat1" size="10" id="cat1" class ="goods_cat">                   
                    </select>
                    <select name="cat2" size="10" id="cat2" class ="goods_cat">                    
                    </select>
                    <select name="cat3" size="10" id="cat3" class ="goods_cat">
                    </select>
                    <select name="cat4" size="10" id="cat4" class ="goods_cat">
                    </select>
                    <div id="cat4_add">
                        <input type="text" id="new_cat" name="new_cat"/>
                        <a href="#" id="add_new_cat">添加</a>
                    </div>
                    <div id="goods_type" class="type"></div>
                    <div id="goods_sale_type" class="type"></div>
                    <input type="hidden" id="goods_cat" name="goods_cat" value="" />
                    <p>发布到：<select id="shopId" name="shopId"><%Call getShopList()%></select></p>
                    <div class ="clear"></div>
                    <input type="button" value ="下一步" id="catOk" class="subBt" />
                </div>
                <div id="goods_add_2" class="hidden">
                    <h3>商品具体信息</h3>
                    <p>发布到货架：<select name="shelf" id="shelf"></select></p>
                    <input type="hidden" value ="0" name="goods_discount" id="goods_discount" />
                    <input type="hidden" value="" name="goods_info" id="goods_info" />
                    <% Call getAddForm()%>
                    <input type="button" value ="上一页" id="back" class="subBt" />
                    <div class ="clear"></div>
                </div>
                </form>
            </div>
            <br />
        <%End If%>
    </div> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
<script type="text/javascript" charset="utf-8" src="../include/editor/kindeditor-min.js"></script>
<script type="text/javascript">
    //<![CDATA[
    userCenter.setGoodsCat1();
    userCenter.setButtomCatOk();
    userCenter.setGoodsType();
    userCenter.setRangeChangeListen();
    userCenter.catReadyAdd();
    KE.show({
        id: 'goods_introduce',
        imageUploadJson: 'img_upload.aspx',
        allowFileManager: false
    });
    userCenter.mySubmit();
    //]]>
</script>
</asp:Content>
