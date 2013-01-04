<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_sale_zq.aspx.vb" Inherits="TaoNiu.user_center_sale_zq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>赠券营销</h1>
        <div id="info">
            <ol>
                <li>制定赠券规则</li>
                <li>修改赠券策略</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看管理您店铺的赠券策略。</p></div>
          <h2><span class="subTitle">赠券添加</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">
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
                <form method="post" action="server_page/sale_fun.aspx?action=zq">
                    <%call getForm() %>
                </form>
            </div>
          <h2><span class="subTitle">赠券管理</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">      
                <%getLogs() %>
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
<script type="text/javascript">
    //<![CDATA[
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
