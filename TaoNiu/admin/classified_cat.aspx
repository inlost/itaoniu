<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="classified_cat.aspx.vb" Inherits="TaoNiu.classified_cat" %>
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
                SelectCommand="SELECT [id], [catName] FROM [classifieds_cats] WHERE (([catDeep] = @catDeep) AND ([catFather] = @catFather))">
                <SelectParameters>
                    <asp:Parameter DefaultValue="2" Name="catDeep" Type="Int16" />
                    <asp:ControlParameter ControlID="ListBox1" Name="catFather" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <p>子分类移动到
                <asp:DropDownList ID="DropDownList2" runat="server">
                </asp:DropDownList>
                <asp:Button ID="removeCat2" runat="server" Text="删除选中" />
                <asp:Button ID="Button3" runat="server" Text="上移" />
                <asp:Button ID="Button4" runat="server" Text="下移" />
            </p>
            <p>添加分类
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:Button ID="addCat2" runat="server" Text="添加" style="height: 21px" /></p>
        </div>
        <div class="goods_cat">
            <h2>三级分类</h2>
            <asp:ListBox ID="ListBox3" class="inlist" runat="server" 
                DataSourceID="SqlDataSource3" DataTextField="catName" DataValueField="id"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT [id], [catName] FROM [classifieds_cats] WHERE (([catDeep] = @catDeep) AND ([catFather] = @catFather))">
                <SelectParameters>
                    <asp:Parameter DefaultValue="3" Name="catDeep" Type="Int16" />
                    <asp:ControlParameter ControlID="ListBox2" Name="catFather" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <p><asp:Button ID="removeCat3" runat="server" Text="删除选中" /></p>
            <p>添加分类
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                <asp:Button ID="addCat3" runat="server" Text="添加" />
                <asp:Button ID="Button5" runat="server" Text="上移" />
                <asp:Button ID="Button6" runat="server" Text="下移" />
            </p>
        </div>
        <div class="clear"></div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
