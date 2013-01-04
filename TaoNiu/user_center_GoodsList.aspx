<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_GoodsList.aspx.vb" Inherits="TaoNiu.user_center_GoodsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <form id="form1" runat="server">
    <div id="operateArea">
        <h1>商品管理</h1>
        <div id="tip"><p>在这儿，您可以对您店铺里上商品进行上架，编辑，下架等操作。</p></div>
        <div class="infoBox">
            <p>选择店铺:<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                    DataSourceID="SqlDataSource1" DataTextField="shop_name" DataValueField="id">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:conStr %>" 
                    SelectCommand="SELECT [id], [shop_name] FROM [shops] WHERE ([shop_owner] = @shop_owner)">
                    <SelectParameters>
                        <asp:SessionParameter Name="shop_owner" SessionField="id" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </p>
            <ul id="goodsListed">
                <%Call getGoodsList()%>
                <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
            </ul>
            <ul id="pageLink">
                <%Call getPageLink()%>
            </ul>
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
