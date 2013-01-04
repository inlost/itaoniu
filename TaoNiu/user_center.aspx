<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center.aspx.vb" Inherits="TaoNiu.user_center1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1><%Response.Write(Session("niceName"))%>的信息</h1>
        <div id="info">
            <ol>
                <li>查看信息</li>
                <li>完善信息</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以看到您最近的交易概览，查看和修改您的个人信息和注册资料。</p></div>
        <% If Request.Params("action") = "View" Or Request.Params("action") = Nothing Then%>
        <h2><span class="subTitle">我的信息</span><span class="subBt"><a href="?action=modify">修改资料</a></span></h2>
            <div class="infoBox">
                <% Call getUserInfo()%>
            </div>
        <h2><span class="subTitle">公告</span><span class="subBt"><a href="#"></a></span></h2>
            <div class ="infoBox">
                <%Call getAd()%>
            </div>
        <% ElseIf Request.Params("action") = "real" Then%>
            <h2><span class="subTitle">修改资料</span><span class="subBt"><a href="user_center.aspx">实名认证</a></span></h2>
            <div class="infoBox">
                <form id="checkReal" method ="post" enctype="multipart/form-data" action ="server_page/user_service.aspx?action=checkReal">
                    <% Call getCheckRealForm()%>
                </form>
            </div>
        <%Else%>
        <h2><span class="subTitle">修改资料</span><span class="subBt"><a href="user_center.aspx">我的信息</a></span></h2>
            <div class="infoBox">
                <form id="infoModify" method ="post" action ="server_page/user_service.aspx?action=modifyInfo">
                    <% Call getModifyForm()%>
                    <input class="subBt" type="submit" name="infoSub" value ="提交更改" />
                </form>
            </div>
            <h2><span class="subTitle">实名认证</span><span class="subBt"></span></h2>
            <div class="infoBox">
                <form id="Form1" method ="post" enctype="multipart/form-data" action ="server_page/user_service.aspx?action=checkReal">
                    <% Call getCheckRealForm()%>
                </form>
            </div>
            <h2><span class="subTitle">修改密码</span><span class="subBt"></span></h2>
            <div class="infoBox">
                <form id="modifyPass" method ="post" action ="server_page/user_service.aspx?action=modifyPass">
                    <% Call getModifyPassForm()%>
                    <input class="subBt" type="submit" name="infoSub" value ="提交更改" />
                </form>
            </div>
        <%End If%>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
