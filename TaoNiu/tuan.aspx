<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="tuan.aspx.vb" Inherits="TaoNiu.tuan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link type="text/css" rel="stylesheet" href="style/tuan.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="inner">
        <div id="topBox">
            <h2>本期团购：<%Call getTuan("title")%></h2>
            <div id="innerLeft">
                <div id="buyBox">
                    <p><span>￥<%Call getTuan("price")%></span></p><%getSubForm()%>
                    <table class="deal-discount">
                        <tbody>
                            <tr>
                                <th>原价</th>
                                <th>折扣</th>
                                <th>节省</th>
                            </tr>
                            <tr>
                                <td><del>￥<%getTuan("oPrice")%></del></td>
                                <td><%getTuan("discount")%>%</td>
                                <td>￥<%Call getTuan("save")%></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div id="timeLeft">
                    <h3>剩余时间</h3>
                    <ul>
                        <li><span><%getTime("day")%></span>天</li>
                        <li><span><%getTime("hour")%></span>小时</li>
                        <li><span><%getTime("minute")%></span>分钟</li>
                    </ul>
                </div>
                <div id="tuanInfo">
                    <h3>最低启团人数<span><%getTuan("nb")%></span></h3>
                    <h3><span><%getTuan("nbNow")%></span>人已参与</h3>
                </div>
            </div>
            <div id="innerRight">
                <img src='<%getTuan("pic") %>'>
            </div>        
        </div>
        <div id="goodsIntro">
            <h3>本单详情</h3>
            <%getTuan("introduce")%>
        </div>
    </div>
    <div id="sideBar">
        <div id="shopInfo">
            <h3>商家信息</h3>
            <%getShopInfo()%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
