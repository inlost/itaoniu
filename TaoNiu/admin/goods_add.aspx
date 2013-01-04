<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/admin_main.Master" CodeBehind="goods_add.aspx.vb" Inherits="TaoNiu.goods_add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div class="contentBox">
        <form id="form1" runat="server">
            
            <p><span>店铺标号：</span><asp:TextBox ID="shopId" TabIndex="1" runat="server"></asp:TextBox></p>
            <p><span>发布者编号：</span><asp:TextBox ID="ownerId" TabIndex="2" runat="server"></asp:TextBox></p>
            <p><span>商品分类：</span><asp:DropDownList ID="cat1" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="catName" DataValueField="id" 
                    TabIndex ="3" AutoPostBack="True">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:conStr %>" 
                    SelectCommand="SELECT [id], [catName] FROM [goods_cat] WHERE ([catDeep] = @catDeep)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="1" Name="catDeep" Type="Int16" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:DropDownList ID="cat2" runat="server" AutoPostBack="True" 
                    TabIndex ="4" DataSourceID="SqlDataSource2" DataTextField="catName" DataValueField="id">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:conStr %>" 
                    SelectCommand="SELECT [id], [catName] FROM [goods_cat] WHERE ([fatherCat] = @fatherCat)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="cat1" Name="fatherCat" 
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:DropDownList ID="cat3" runat="server" DataSourceID="SqlDataSource3" 
                    TabIndex ="5" DataTextField="catName" DataValueField="id">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:conStr %>" 
                    SelectCommand="SELECT [id], [catName] FROM [goods_cat] WHERE ([fatherCat] = @fatherCat)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="cat2" Name="fatherCat" 
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </p>
            <p><span>商品类型：</span>
                <asp:RadioButtonList ID="goods_type" runat="server" 
                    DataSourceID="SqlDataSource4" DataTextField="name" DataValueField="id" TabIndex ="6"
                    RepeatColumns="6">
                </asp:RadioButtonList>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:conStr %>" 
                    SelectCommand="SELECT [id], [name] FROM [goods_type]"></asp:SqlDataSource>
            </p>
            <p><span>商品销售类型：</span><asp:RadioButtonList ID="goods_sale_type" runat="server" TabIndex ="7"
                    DataSourceID="SqlDataSource5" DataTextField="name" DataValueField="id" 
                    RepeatColumns="6">
                </asp:RadioButtonList>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:conStr %>" 
                    SelectCommand="SELECT * FROM [goods_sale_type]"></asp:SqlDataSource>
            </p>
            <p><span>商品标题：</span><asp:TextBox ID="goods_title" TabIndex="8" runat="server"></asp:TextBox></p>
            <p><span>商品价格：</span><asp:TextBox ID="goods_prize" TabIndex="9" runat="server"></asp:TextBox></p>
            <p><span>商品折扣：</span><asp:DropDownList ID="discount1" runat="server">
                <asp:ListItem>0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem>9</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="discount2" runat="server">
                    <asp:ListItem>0</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p><span>商品属性：</span><asp:ListBox ID="goods_info_list" runat="server"></asp:ListBox>
                <asp:TextBox ID="goods_info_add" runat="server"></asp:TextBox><asp:Button ID="info_add"
                    runat="server" Text="添加" />
            </p>
            <p><span>商品图片：</span><asp:FileUpload ID="goods_picture" runat="server" />
            </p>
            <p><span>商品介绍：</span><asp:TextBox ID="goods_introduce" TabIndex="13" runat="server" 
                    TextMode="MultiLine"></asp:TextBox></p>
            <p><span>商品数量：</span><asp:TextBox ID="goods_number" TabIndex="14" runat="server"></asp:TextBox></p>
            <p><span>开售时间：</span><asp:RadioButtonList ID="goods_on_sale_date" runat="server" 
                    RepeatColumns="2">
                <asp:ListItem Selected="True" Value="now">立即</asp:ListItem>
                <asp:ListItem Value="other">选择时间</asp:ListItem>
                </asp:RadioButtonList>
                日期：
                <asp:DropDownList ID="sale_year" runat="server">
                </asp:DropDownList>-
                <asp:DropDownList ID="sale_month" runat="server">
                </asp:DropDownList>-
                <asp:DropDownList ID="sale_day" runat="server">
                </asp:DropDownList>
                时间：
                <asp:DropDownList ID="sale_hour" runat="server">
                </asp:DropDownList>：
                <asp:DropDownList ID="sale_min" runat="server">
                </asp:DropDownList>
            </p>
             <p><span>是否使用橱窗位：</span><asp:RadioButtonList ID="goods_tuijian" runat="server" 
                     RepeatColumns="2">
                 <asp:ListItem Value="1">使用</asp:ListItem>
                 <asp:ListItem Selected="True" Value="0">不使用</asp:ListItem>
                 </asp:RadioButtonList>
            </p>
            <p><span>送货范围：</span><asp:TextBox ID="goods_range" runat="server" 
                    TextMode="MultiLine"></asp:TextBox></p>
            <p><span>商品有效期：</span><asp:RadioButtonList ID="goods_Period" runat="server" 
                    RepeatColumns="2">
                <asp:ListItem Value="7">7天</asp:ListItem>
                <asp:ListItem Selected="True" Value="14">14天</asp:ListItem>
                </asp:RadioButtonList>
            </p>
            <p><span>有无发票：</span><asp:RadioButtonList ID="goods_invoice" runat="server" 
                    RepeatColumns="2">
                <asp:ListItem Value="1">有</asp:ListItem>
                <asp:ListItem Selected="True" Value="0">无</asp:ListItem>
                </asp:RadioButtonList>
            </p>
            <p><span>保修类型：</span><asp:RadioButtonList ID="goods_warranty_type" runat="server" 
                    RepeatColumns="3">
                <asp:ListItem Value="0">无</asp:ListItem>
                <asp:ListItem Selected="True" Value="1">店铺保修</asp:ListItem>
                <asp:ListItem Value="2">全国联保</asp:ListItem>
                </asp:RadioButtonList>
            </p>
            <p><span>保修时长：</span><asp:DropDownList ID="goods_warranty_time" runat="server">
                </asp:DropDownList>个月
            </p>
            <p><span>保修补充：</span><asp:TextBox ID="goods_warranty_other" runat="server" 
                    TextMode="MultiLine"></asp:TextBox></p>
        <asp:Button ID="btPub" runat="server" Text="发布" />
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
<script type="text/javascript" charset="utf-8" src="../include/editor/kindeditor.js"></script>
<script type="text/javascript" >
    KE.show({
        id: 'ctl00_ContentContent_goods_introduce',
        imageUploadJson: 'img_upload.aspx',
        allowFileManager: false
    });
</script>
</asp:Content>
