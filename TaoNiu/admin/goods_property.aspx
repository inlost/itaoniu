<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="goods_property.aspx.vb" Inherits="TaoNiu.goods_property" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <form id="form1" runat="server">
    <div id="contentBox">
        <div>
            <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource1" DataTextField="catName" DataValueField="id" 
                Height="178px" Width="133px"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT [id], [catName] FROM [goods_cat] WHERE ([catDeep] = @catDeep)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="1" Name="catDeep" Type="Int16" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:ListBox ID="ListBox2" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource2" DataTextField="catName" DataValueField="id" 
                Height="194px" Width="130px"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT [id], [catName] FROM [goods_cat] WHERE ([fatherCat] = @fatherCat)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ListBox1" Name="fatherCat" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:ListBox ID="ListBox3" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource3" DataTextField="catName" DataValueField="id" 
                Height="188px" Width="127px"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT [id], [catName] FROM [goods_cat] WHERE ([fatherCat] = @fatherCat)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ListBox2" Name="fatherCat" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:ListBox ID="ListBox4" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource4" DataTextField="catName" DataValueField="id" 
                Height="201px" Width="125px"></asp:ListBox>
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                ConnectionString="<%$ ConnectionStrings:conStr %>" 
                SelectCommand="SELECT [id], [catName] FROM [goods_cat] WHERE ([fatherCat] = @fatherCat)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ListBox3" Name="fatherCat" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
        <div>
            <div style="width:100px;height:150px;display:block;float:left;margin:10px;">
                <asp:ListBox ID="ListBox5" runat="server" Height="184px" Width="97px" 
                    AutoPostBack="True" DataSourceID="SqlDataSource5" DataTextField="name" 
                    DataValueField="id"></asp:ListBox>   
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:conStr %>" 
                    SelectCommand="SELECT * FROM [cat_property] WHERE ([catId] = @catId)">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="-1" Name="catId" SessionField="nowCat" 
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <p>编辑用列表，新添加属性请不要选择</p>
                <p>
                    <asp:Button ID="Button2" runat="server" Text="删除" Width="89px" /></p>
            </div>
            <div style="width:500px;height:150px;display:block;float:left;">
                <p>属性名称：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></p>
                <p>
                    属性样式：<asp:ListBox ID="ListBox6" runat="server" Rows="1">
                        <asp:ListItem Value="1">下拉框</asp:ListItem>
                        <asp:ListItem Value="2">复选框</asp:ListItem>
                    </asp:ListBox>
                </p>
                <p>属性（一行一个）：</p>
                <p><asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Height="277px" 
                        Width="480px"></asp:TextBox></p>
                <p>
                    <asp:Button ID="Button1" runat="server" Text="添加" /></p>
            </div>       
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
