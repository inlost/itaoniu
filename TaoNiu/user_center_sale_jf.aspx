<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_sale_jf.aspx.vb" Inherits="TaoNiu.user_center_sale_jf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>积分营销</h1>
        <div id="info">
            <ol>
                <li>制定积分规则</li>
                <li>修改积分策略</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看管理您店铺的积分策略。</p></div>
          <h2><span class="subTitle">积分管理</span><span class="subBt"><a href="#"></a></span></h2>
            <div class="infoBox">
                <form name="form1" runat="server">
                     <asp:ListBox ID="shop" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="shop_name" DataValueField="id" Rows="1" AutoPostBack="True"></asp:ListBox>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:conStr %>" 
                        SelectCommand="SELECT [id], [shop_name] FROM [shops] WHERE ([shop_owner] = @shop_owner)">
                        <SelectParameters>
                            <asp:SessionParameter Name="shop_owner" SessionField="id" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>               
                </form>
                <form method="post" action ="server_page/sale_fun.aspx?action=jf">
                    <%Call getForm()%>
                </form>
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">

</asp:Content>
