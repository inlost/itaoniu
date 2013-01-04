<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_myOrders.aspx.vb" Inherits="TaoNiu.user_center_myOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>订单管理</h1>
        <div id="info">
            <ol>
                <li>订单管理</li>
                <li>确认收货</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看管理您店铺的订单。</p></div>
        <%If Request.Params("action") = Nothing Then%>
        <h2><span class="subTitle">查看订单</span><span class="subBt"><a href="#">确认发货</a></span></h2>
            <h2><span class="subTitle">待处理订单</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">
                <%Call getOrders(False)%>
                <%Call getPageLink()%>
            </div>
            <h2><span class="subTitle">已完成交易</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">
                <%Call getOrders(True)%>
                <%Call getPageLink()%>
            </div>
        <br />
        <%Else%>
        <h2><span class="subTitle">确认发货</span><span class="subBt"><a href="user_center_myOrders.aspx?at=myorders">查看订单</a></span></h2>
            <%If Request.Params("id") <> Nothing Then%>
            <div class="infoBox">
                <%Call getOrderById()%>
            </div>
            <h2><span class="subTitle">确认发货</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">
                <form method="post" action="server_page/order.aspx?action=send">
                    <%Call getGoodFun()%>
                </form>
            </div>
            <h2><span class="subTitle">修改价格</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">
                <form method="post" action="server_page/order.aspx?action=modify_price">
                    <%Call getModifyPrice()%>
                </form>
            </div>
            <h2><span class="subTitle">投诉</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">
                <form method="post" action="server_page/order.aspx?action=tousu">
                    <%Call getGoodTousu()%>
                </form>
            </div>
            <%End If%>
        <br />
        <%End If%>
    </div> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
