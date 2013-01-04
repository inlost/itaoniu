<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="goods_cat_firstShow.aspx.vb" Inherits="TaoNiu.goods_cat_firstShow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
<form id="form1" runat="server">
    <div class="contentBox">
        <h2>全站活动</h2>
        <asp:ListBox ID="list_hd_home" runat="server" DataSourceID="SqlDataSource1" 
            DataTextField="title" DataValueField="id"></asp:ListBox>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:conStr %>" 
            SelectCommand="SELECT [id], [title] FROM [activities] WHERE ([cat] = @cat)">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="cat" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Button ID="del_hd_home" runat="server" Text="删除所选" />
        <p>活动名称：<asp:TextBox ID="home_hd_title" runat="server"></asp:TextBox></p>
        <p>活动连接：<asp:TextBox ID="home_hd_src" runat="server"></asp:TextBox></p>
        <p>活动海报：<asp:FileUpload ID="home_hd_img" runat="server" /></p>
        <asp:Button ID="hd_add" runat="server" Text="添加活动" />
        <hr />
        <h2>大类活动</h2>
        <asp:ListBox ID="list_cat" runat="server" AutoPostBack="True" 
            DataSourceID="SqlDataSource2" DataTextField="catName" DataValueField="id"></asp:ListBox>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:conStr %>" 
            SelectCommand="SELECT [id], [catName] FROM [goods_cat] WHERE ([catDeep] = @catDeep)">
            <SelectParameters>
                <asp:Parameter DefaultValue="1" Name="catDeep" Type="Int16" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:ListBox ID="cat_hd" runat="server" DataSourceID="SqlDataSource3" 
            DataTextField="title" DataValueField="id"></asp:ListBox>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:conStr %>" 
            SelectCommand="SELECT [id], [title] FROM [activities] WHERE ([cat] = @cat)">
            <SelectParameters>
                <asp:ControlParameter ControlID="list_cat" Name="cat" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Button ID="del_hd_cat" runat="server" Text="删除选中" />
        <p>活动名称：<asp:TextBox ID="cat_hd_title" runat="server"></asp:TextBox></p>
        <p>活动连接：<asp:TextBox ID="cat_hd_src" runat="server"></asp:TextBox></p>
        <p>活动海报：<asp:FileUpload ID="cat_hd_img" runat="server" /></p>
        <asp:Button ID="cat_hd_add" runat="server" Text="添加活动" />
    </div> 
</form> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
