<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master"
    CodeBehind="user_center_GoodsEdit.aspx.vb" Inherits="TaoNiu.user_center_GoodsEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="goods_type">
        <%Call getGoodsType()%></div>
    <div id="goods_sale_type">
        <%Call getGoodsSaleType()%></div>
    <p>
        发布到：<select id="shopId" name="shopId"><%Call getShopList()%></select></p>
    <h3>
        商品具体信息</h3>
    <p>
        发布到货架：<select name="shelf" id="shelf"><%Call getShelf()%></select></p>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
