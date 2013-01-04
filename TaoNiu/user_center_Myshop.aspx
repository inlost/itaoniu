<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_Myshop.aspx.vb" Inherits="TaoNiu.user_center_Myshop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>我的店铺</h1>
        <div id="info">
            <ol>
                <li>货架管理</li>
                <li>店铺编辑</li>
                <li>店铺认证</li>
                <li>关闭店铺</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以对您所有的店铺进行基本管理。</p></div>
        <% If Request.Params("action") = Nothing Then%>
        <h2><span class="subTitle">店铺信息</span><span class="subBt"><a href="?at=myShop&action=modify">店铺编辑</a></span></h2>
        <div class="infoBox">
            <p>店铺列表：<select id="shopList"><%Call getShopList()%></select></p>
            <div id="shopInfo"></div>
            <div class ="clear"></div>
        </div> 
        <h2><span class="subTitle">货架管理</span><span class="subBt"><a href="?at=myShop&action=modify">店铺认证</a></span></h2>
        <div class="infoBox">
            <div id="shelfList">
                <p>添加一个新货架：<input type="text" id="shelfName" name="newShelf" /><input class="subBt" id="addShelf" type="button" value ="添加" /></p>
                <h3>当前店铺的货架列表：</h3>
                <div id="inShelfList"></div>
            </div>
            <div class ="clear"></div>
        </div> 
        <%ElseIf Request.Params("action") = "modify" Then%>
        <h2><span class="subTitle">店铺编辑</span><span class="subBt"><a href="?at=myShop">店铺信息</a></span></h2>
        <div class="infoBox">
            <form method ="post" action="server_page/shop_goods.aspx?action=shopModify">
                <p>店铺列表：<select id="shopId" name="shopId"><%Call getShopList()%></select></p>
                <p class='fullWidth'><span class='infoTitle'>Tip：</span><span class='info'>不需要修改的信息留空即可。</span></p>
                <p class='fullWidth'><span class='infoTitle'>店铺简介：</span><span class='info'></span></p>
                <input type="text" id="shopIntro"  style='width:95%;height:400px;' name="shop_introduce" />
                <%Call setModfyForm()%>
                <p class='fullWidth'><span class='infoTitle'>店铺状态：</span><span class='info'>
                    <select id="shop_status" name="shop_status">
                        <option value ="1">正常</option>
                        <option value ="2">关闭</option>
                        <option value ="3">删除</option>
                    </select>
                </span></p>
                <input type="submit" id="subForm" class="subBt" value ="确定" />           
            </form>
            <div class ="clear"></div>
        </div> 
        <%End If%>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
<script type="text/javascript" charset="utf-8" src="include/editor/kindeditor-min.js"></script>
    <script type="text/javascript">
        //<![CDATA[
        userCenter.setShopInfo();
        userCenter.addShopInfoListen();
        userCenter.setRangeChangeListen();
        KE.show({
            id: 'shopIntro',
            imageUploadJson: 'img_upload.aspx',
            allowFileManager: false
        });
        //]]>
    </script>
</asp:Content>
