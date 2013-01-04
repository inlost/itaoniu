<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_lm.aspx.vb" Inherits="TaoNiu.user_center_lm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>店铺联盟</h1>
        <div id="info">
            <ol>
                <li>联盟查找</li>
                <li>联盟发布</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看管理您店铺的联盟商品。</p></div>
        <h2><span class="subTitle">已有联盟</span><span class="subBt"><a href="user_center_lm_list.aspx?at=tools">已经发布的商品</a></span></h2>
            <div class="infoBox">

            </div>
        <h2><span class="subTitle">发布联盟信息</span><span class="subBt"><a href="user_center_lm_search.aspx?at=tools">查找联盟信息</a></span></h2>
            <div class ="infoBox">
                <form id="Form1" name="form1" runat="server">
                     <asp:ListBox ID="shop" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="shop_name" DataValueField="id" Rows="1" AutoPostBack="True"></asp:ListBox>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:conStr %>" 
                        SelectCommand="SELECT [id], [shop_name] FROM [shops] WHERE ([shop_owner] = @shop_owner)">
                        <SelectParameters>
                            <asp:SessionParameter Name="shop_owner" SessionField="id" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>               
                </form>
                <form method="post" action="server_page/sale_fun.aspx?action=goodsLmAdd">
                    <div id="goods_add_1">
                        <h3>请选择需要商品所属分类</h3>
                        <select name="cat1" size="10" id="cat1" class ="goods_cat">                   
                        </select>
                        <select name="cat2" size="10" id="cat2" class ="goods_cat">                    
                        </select>
                        <select name="cat3" size="10" id="cat3" class ="goods_cat">
                        </select>
                        <select name="cat4" size="10" id="cat4" class ="goods_cat">
                        </select>
                    </div>
                    <%getLmForm() %>
                </form>
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
<script type="text/javascript">
    //<![CDATA[
    userCenter.setGoodsCat1();
    userCenter.sale_choseGood();
    //]]>
</script>
<div id="shelfChoice" class="hidden">
    <div id="div_opacity"></div>
    <div id="alter_bg">
        <div class="btn_close"><div class="btn_img"></div></div>
        <div id="choice_Select">
            <div id="select_one">货架名称：<select name="sale_S_1" size="10" id="sale_S_one" > </select></div>
            <div id="select_two">货架下的商品名称：<select name="sale_S_2" size="10" id="sale_S_two" > </select></div>
        </div>
        <div class="btn_location">
            <div class="alter_font">请选择商品！</div>
            <div class="btn_submit">确定</div>
            <div class="btn_cancell">取消</div>
        </div>
    </div>
</div>
</asp:Content>
