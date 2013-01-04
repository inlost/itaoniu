<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_lm_list.aspx.vb" Inherits="TaoNiu.user_center_lm_list" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>店铺联盟</h1>
        <div id="info">
            <ol>
                <li>联盟查找</li>
                <li>联盟发布</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看管理您店铺的联盟商品。</p></div>
        <h2><span class="subTitle">已经发布的商品</span><span class="subBt"><a href="user_center_lm.aspx?at=tools">已有联盟</a></span></h2>
            <div class="infoBox">
                <form id="Form1" name="form1" runat="server">
                     店铺：<asp:ListBox ID="shop" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="shop_name" DataValueField="id" Rows="1" AutoPostBack="True"></asp:ListBox>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:conStr %>" 
                        SelectCommand="SELECT [id], [shop_name] FROM [shops] WHERE ([shop_owner] = @shop_owner)">
                        <SelectParameters>
                            <asp:SessionParameter Name="shop_owner" SessionField="id" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>               
                </form>
                <ul id="lianMengGoods">
                    <%displayGoods()%>
                </ul>
            </div>
    </div> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
