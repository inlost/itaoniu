<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="search.aspx.vb" Inherits="TaoNiu.search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
    <div id="inCatNav">
        <ul><%Call get_nav()%></ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="goodsList">
        <div id="nowPath"></div>
        <div id="allGoods">


            <div class="filter" id="J_Filter">
	            <ul class="filter-tabbar" id="J_FilterTabBar">
			        <li><span class="l"></span><span class="r"></span><a href="#" title="">所有宝贝</a></li>
	      	        <li><span class="l"></span><span class="r"></span><a href="search.aspx?q=qqqq" title="">二手</a></li>
                    <li><span class="l"></span><span class="r"></span><a href="#" title="">交换</a></li>
                    <li><span class="l"></span><span class="r"></span><a href="#" title="">赠送</a></li>
	            </ul>
	            <form id="filterForm" action="search.aspx" method="get">
		            <fieldset>
			            <ul class="basic">
				            <li class="type hoverMenu">
					            <span class="select-item"><span>支付类型：</span></span>
					            <ul class="item-list">
						            <li id="pay_type_sy"><input type="radio" name="goods_pay_type" <%setPayCheck("pay_type_sy") %> value="0" />所有</li>
						            <li id="pay_type_xs"><input type="radio" name="goods_pay_type" <%setPayCheck("pay_type_xs") %> value="1" />在线支付</li>
						            <li id="pay_type_hdfk"><input type="radio" name="goods_pay_type" <%setPayCheck("pay_type_hdfk") %> value="2" />货到付款</li>
					            </ul>
				            </li>
			            </ul>
			            <div class="advanced" id="J_FilterOptions">
				            <table>
					            <caption>详细属性筛选</caption>
					            <tbody>
						            <tr>
							            <th>店铺保障：</th>
                                        <%getBaoZhang()%>
                                    </tr>
					            </tbody>
				            </table>
			            </div>
                        <input type="hidden" name="z" value="<% getParams("z") %>" />
                        <input type="hidden" name="p" value="<% getParams("p") %>" />
                        <input type="hidden" name="q" value="<% getParams("q") %>" />
                        <button class="submit" type="submit" id="J_SubmitBtn">确定</button>
		            </fieldset>
	            </form>
		            <!-- end filter condition -->
	            <div id="filterplaceholder">
		            <div class="toolbarWrapper">
			            <ul class="toolbar" id="J_FilterToolbar">
				            <!-- list-mode 列表模式; thumb-mode: 大图模式; -->
				            <li class="mode thumb-mode">
                                <%getDisplay()%>					            
				            </li>
				            <!-- by-price-asc 价格低到高; by-price-desc 价格高到低; by-credit-desc 信用高到低 -->
				            <li class="order hoverMenu">
					            <span class="title">排序 </span>
					            <a id="J_OrderSelector" href="<%call geturl %>" class="select-item  order-by"><span><span>默认排序</span></span></a>
					            <ul class="order-options item-list" id="J_OrderByList">
						            <li class="by-price-asc"><a href="<%call geturl %>&orderBy=goods_prize&orderByWay=asc">价格从低到高</a></li>
						            <li class="by-price-desc"><a href="<%call geturl %>&orderBy=goods_prize&orderByWay=desc">价格从高到低</a></li>
						            <li class="by-credit-desc"><a href="<%call geturl %>&orderBy=goods_pj_good&orderByWay=desc">信用从高到低</a></li>
						            <li class="by-sale-desc"><a href="<%call geturl %>&orderBy=goods_number_now&orderByWay=asc">销量从高到低</a></li>
						            <li class="by-default selected"><a href="<%call geturl %>">恢复默认排序</a></li>
					            </ul>
				            </li>
			            </ul>
		            </div>
	            </div>
            </div>




            <div id="allGoodsBox">
                <ul id="goodsListed">
                    <%Call getGoodsList()%>
                    <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                </ul>
                <ul id="pageLink">
                    <%Call getPageLink()%>
                </ul>
            </div>
        </div>
    </div> 
    <div id="goodsListSideBar">
        <h1>最新热卖</h1>
        <%Call getSideBar()%>
    </div>
    <script type="text/javascript">
    //<![CDATA[
        tn.setSearchOrder();

    //]]>
    </script> 
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
