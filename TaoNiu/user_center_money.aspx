<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_money.aspx.vb" Inherits="TaoNiu.user_center_money" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>资金管理</h1>
        <div id="info">
            <ol>
                <li>提款</li>
                <li>充值</li>
                <li>查看资金变动情况</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看管理您的资金情况。</p></div>
        <h2><span class="subTitle">资金信息</span><span class="subBt"><a href="user_center_money_log.aspx?at=money">资金变动详单</a></span></h2>
        <div class="infoBox">
            <%getMoneyInfo() %>
            <div class="clear"></div>
        </div>
        <h2><span class="subTitle">提现</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <form method="post" action="server_page/money.aspx?action=moveMoney">
                <%Call getMoneyMoveForm()%>
                <div class="clear"></div>
            </form>
        </div>
        <h2><span class="subTitle">充值</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <form method="post" action="server_page/money.aspx?action=addNewMoney">
                <%Call getMoneyAdd()%>
                <div class="clear"></div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
