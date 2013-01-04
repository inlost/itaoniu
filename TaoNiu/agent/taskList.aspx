<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/agent.Master" CodeBehind="taskList.aspx.vb" Inherits="TaoNiu.taskList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>录入列表</h1>
        <div id="info">
            <ol>
                <li>已录入商品查看</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看已经完成的录入。</p></div>
        <h2><span class="subTitle">等待审核</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <%Call getShList()%>
        </div>
        <h2><span class="subTitle">审核通过</span><span class="subBt"><a href="#"></a></span></h2>
        <div class="infoBox">
            <%Call getShList2()%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
