<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/user_center.Master" CodeBehind="user_center_lianMeng.aspx.vb" Inherits="TaoNiu.user_center_lianMeng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="operateArea">
        <h1>店铺联盟</h1>
        <div id="info">
            <ol>
                <li>查看联盟店铺</li>
                <li>管理联盟店铺</li>
            </ol>
        </div>
        <div id="tip"><p>在这儿，您可以查看管理您店铺的联盟商家。</p></div>
        <%If Request.Params("action") = "add" Then%>
            <h2><span class="subTitle">要添加到联盟的店铺</span><span class="subBt"><a href="#"></a></span></h2>
            <div class ="infoBox">
                <%Call getShopInfo()%>
            </div>
          <h2><span class="subTitle">添加联盟店铺</span><span class="subBt"><a href="?at=lianMeng&action=vinew">查看联盟店铺</a></span></h2>
            <div class="infoBox">
                <form action="server_page/lianMeng.aspx?action=add" method="post">
                        <%Call getAddForm()%>      
                </form>     
            </div>
        <%Else%>
            <h2><span class="subTitle">联盟店铺</span><span class="subBt"><a href="#"></a></span></h2>
            <div class ="infoBox">
                <form id="form1" runat="server">
                <p>选择店铺：<asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="shop_name" DataValueField="id" AutoPostBack="True" Rows="1"></asp:ListBox>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:conStr %>" 
                        SelectCommand="SELECT [id], [shop_name] FROM [shops] WHERE ([shop_owner] = @shop_owner)">
                        <SelectParameters>
                            <asp:SessionParameter Name="shop_owner" SessionField="id" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </p>
                <ul id="lianMengList"><%Call getLianMengList()%><div class="clear"></div></ul>
            </div>            
            <h2><span class="subTitle">店铺店标上传</span><span class="subBt"><a href="#"></a></span></h2>
            <div class ="infoBox">
                <p>选择店铺：<asp:ListBox ID="ListBox2" runat="server" DataSourceID="SqlDataSource1" 
                    DataTextField="shop_name" DataValueField="id" AutoPostBack="True" Rows="1"></asp:ListBox>
                </p>    
                </form>
                <form enctype="multipart/form-data" method ="post" action="server_page/lianMeng.aspx?action=icon">           
                    <%Call getShopIcon()%>
                    <input type="file" name="icon" />
                    <input class='subBt' type='submit' name='infoSub' value ='确认添加' />
                </form>
            </div>        
        <%End If%>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
