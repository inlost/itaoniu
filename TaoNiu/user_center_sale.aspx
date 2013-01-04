<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_sale.aspx.vb" Inherits="TaoNiu.user_center_sale" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>营销平台</h1>
        <div id="info">
            <ol>
                <li>发布营销活动</li>
                <li>管理营销活动</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看管理您店铺的营销活动。</p></div>
          <h2><span class="subTitle">营销工具</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">
               <ul id="saleFunLink">
                    <li><a href='user_center_sale_jf.aspx?at=sale'class='sale_red'>积分</a><p>按照买家购买商品的金额给予一定比例的积分，积分只能在线上一次性使用，一积分等于一分钱。</p></li>
                    <li><a href='user_center_sale_zq.aspx?at=sale'class='sale_green'>赠券</a><p>买家单笔消费满一定金额时给予赠券，可在线上线下使用。</p></li>
                    <li><a href='user_center_sale_tg.aspx?at=sale' class='sale_blue' >团购</a><p>团体购物形式</p></li>
                    <li><a href='user_center_sale_pf.aspx?at=sale' class='sale_orange' >批发</a><p>买家购买数量达到一定额度，可给予批发价</p></li>
               </ul>
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
