<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_Goods.aspx.vb" Inherits="TaoNiu.user_center_Goods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>商品管理</h1>
        <div id="info">
            <ol>
                <li>商品上架</li>
                <li>商品编辑</li>
                <li>商品下架</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以对您店铺里上商品进行上架，编辑，下架等操作。</p></div>

        


        <%If Request.Params("action") = Nothing Or Request.Params("action") = "add---0" Then%>
        <h2><span class="subTitle">托管录入审核</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">
                <form method ="post" enctype="multipart/form-data" action="server_page/shop_goods.aspx?action=weituo_pass" name="newGoods">
                    <%Call getLuRuList()%>
                </form> 
            </div>
        <h2><span class="subTitle">上架新商品</span><span class="subBt"><a href="user_center_GoodsList.aspx?at=goods&action=management">商品管理</a></span></h2>
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
                    <p><input type="checkbox" id="isDifPrice" name="difPrice" />同款异价</p>
                    <div id="difPrice" class="hidden">
                        <div id="difPrice_color">
                            <ul>
                                <li><input type="checkbox" name="difPrice_color" value="红色" />红色</li>
                                <li><input type="checkbox" name="difPrice_color" value="黄色" />黄色</li>
                                <li><input type="checkbox" name="difPrice_color" value="白色" />白色</li>
                                <li><input type="checkbox" name="difPrice_color" value="黑色" />黑色</li>
                                <li><input type="checkbox" name="difPrice_color" value="粉色" />粉色</li>
                            </ul>
                        </div>
                        <div id="difPrice_size">
                            <ul>
                                <li><input type="checkbox" name="difPrice_size" value="大号" />大号</li>
                                <li><input type="checkbox" name="difPrice_size" value="中号" />中号</li>
                                <li><input type="checkbox" name="difPrice_size" value="小号" />小号</li>
                            </ul>                        
                        </div>
                        <div id="difPrice_packge">
                            <ul>
                                <li><input type="checkbox" name="difPrice_packge" value="套餐一" />套餐一</li>
                                <li><input type="checkbox" name="difPrice_packge" value="套餐二" />套餐二</li>
                                <li><input type="checkbox" name="difPrice_packge" value="套餐三" />套餐三</li>
                                <li><input type="checkbox" name="difPrice_packge" value="套餐四" />套餐四</li>
                                <li><input type="checkbox" name="difPrice_packge" value="套餐五" />套餐五</li>
                            </ul>                                          
                        </div>
                        <div id="difPrice_price">
                        
                        </div>
                    </div>
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
<script type="text/javascript" charset="utf-8" src="include/editor/kindeditor-min.js"></script>
<script type="text/javascript">
    //<![CDATA[
    userCenter.setGoodsCat1();
    userCenter.setButtomCatOk();
    userCenter.setGoodsType();
    userCenter.setRangeChangeListen();
    userCenter.catReadyAdd();
    userCenter.setDifPrice();
    KE.show({
        id: 'goods_introduce',
        imageUploadJson: 'img_upload.aspx',
        allowFileManager: false
    });
    userCenter.mySubmit();
    //]]>
</script>
</asp:Content>
