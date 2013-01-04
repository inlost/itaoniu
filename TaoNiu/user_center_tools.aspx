<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_tools.aspx.vb" Inherits="TaoNiu.user_center_tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>店铺工具</h1>
        <div id="info">
            <ol>
                <li>店铺托管</li>
                <li>店铺认证</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可使用桃牛提供的店铺工具。</p></div>
          <h2><span class="subTitle">店铺工具</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">
               <ul id="saleFunLink">
                    <li><a href='user_center_tools_certification.aspx?at=tools'>店铺认证</a><p>参与店铺认证计划，提升店铺形象，获得更多订单。</p></li>
                    <li><a href='user_center_tools_trusteeship.aspx?at=tools'>店铺托管</a><p>参与店铺托管，24小时导购值守，获得更多订单。</p></li>
                    <li><a href='user_center_tools_type.aspx?at=tools'>录入委托</a><p>委托他人进行店铺商品的编辑录入。</p></li>
                    <li><a href="user_center_lianMeng.aspx?at=tools">友情链接</a><p>扩大店铺见光度，四面八方来客户。</p></li>
                    <li><a href="user_center_lm.aspx?at=tools">店铺联盟</a><p>店铺组合销售，让商品小店更有吸引力。</p></li>
               </ul>
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
