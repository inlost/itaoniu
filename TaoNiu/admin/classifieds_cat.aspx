<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="classifieds_cat.aspx.vb" Inherits="TaoNiu.classifieds_cat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <form id="form1" runat="server">
    <div id="contentBox_g_c">
        <div class="goods_cat">
            <h2>一级分类</h2>
            <asp:ListBox ID="ListBox1" class="inlist" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource1" DataTextField="catName" DataValueField="id"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT [id], [catName] FROM [classifieds_cats] WHERE ([catDeep] = @catDeep)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="1" Name="catDeep" Type="Int16" />
                </SelectParameters>
            </asp:SqlDataSource>
            <p>子分类移动到
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
                <asp:Button ID="removeCat1" runat="server" Text="删除选中" />
                <asp:Button ID="Button1" runat="server" Text="上移" />
                <asp:Button ID="Button2" runat="server" Text="下移" />
            </p>
            <p>添加分类
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:Button ID="addCat1" runat="server" Text="添加" /></p>
        </div>
        <div class="goods_cat">
            <h2>二级分类</h2>
            <asp:ListBox ID="ListBox2" class="inlist" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource2" DataTextField="catName" DataValueField="id"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT [id], [catName] FROM [classifieds_cats] WHERE ([catFather] = @catFather)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ListBox1" Name="catFather" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <p>
                <asp:Button ID="removeCat2" runat="server" Text="删除选中" />
                <asp:Button ID="Button3" runat="server" Text="上移" />
                <asp:Button ID="Button4" runat="server" Text="下移" />
            </p>
            <p>添加分类
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:Button ID="addCat2" runat="server" Text="添加" style="height: 21px" /></p>
        </div>
        <div class="clear"></div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
