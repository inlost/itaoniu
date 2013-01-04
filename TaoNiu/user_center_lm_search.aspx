<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_lm_search.aspx.vb" Inherits="TaoNiu.user_center_lm_search" %>
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
        <h2><span class="subTitle">查找联盟信息</span><span class="subBt"><a href="user_center_lm.aspx?at=tools">发布联盟信息</a></span></h2>
            <div class ="infoBox">
                <form id="Form1" name="form1" runat="server">
                     店铺：<asp:ListBox ID="shop" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="shop_name" DataValueField="id" Rows="1" AutoPostBack="True"></asp:ListBox>
                    &nbsp;货架：<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:conStr %>" 
                        SelectCommand="SELECT [id], [shop_name] FROM [shops] WHERE ([shop_owner] = @shop_owner)">
                        <SelectParameters>
                            <asp:SessionParameter Name="shop_owner" SessionField="id" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>               
                     <asp:ListBox ID="shif" runat="server" AutoPostBack="True" 
                         DataSourceID="SqlDataSource2" DataTextField="shelf_name" DataValueField="id" 
                         Rows="1"></asp:ListBox>
&nbsp;商品：<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                         ConnectionString="<%$ ConnectionStrings:conStr %>" 
                         SelectCommand="SELECT * FROM [shop_shelf] WHERE ([shop_id] = @shop_id)">
                         <SelectParameters>
                             <asp:ControlParameter ControlID="shop" Name="shop_id" 
                                 PropertyName="SelectedValue" Type="Int32" />
                         </SelectParameters>
                     </asp:SqlDataSource>
                     <asp:ListBox ID="goods" runat="server" AutoPostBack="True" 
                         DataSourceID="SqlDataSource3" DataTextField="goods_title" DataValueField="id" 
                         Rows="1"></asp:ListBox>
                     <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                         ConnectionString="<%$ ConnectionStrings:conStr %>" 
                         SelectCommand="SELECT [id], [goods_title] FROM [goods_products] WHERE (([shopId] = @shopId) AND ([shelf] = @shelf))">
                         <SelectParameters>
                             <asp:ControlParameter ControlID="shop" Name="shopId" 
                                 PropertyName="SelectedValue" Type="Int32" />
                             <asp:ControlParameter ControlID="shif" Name="shelf" 
                                 PropertyName="SelectedValue" Type="Int32" />
                         </SelectParameters>
                     </asp:SqlDataSource>
                </form>
            </div>
        <h2><span class="subTitle">找到的商品</span><span class="subBt"><a href="#"></a></span></h2>
            <div class ="infoBox">
                <%getGoodsList()%>
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
