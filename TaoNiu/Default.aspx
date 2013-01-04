<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="Default.aspx.vb" Inherits="TaoNiu._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <ul class="fix afterClear">
       <li class="select"><a target="_self" href="javascript:void(0);"><span>首页</span></a><b class="rc-lt"></b><b class="rc-rt"></b></li>
	   <li><a href="mall/index.aspx" title=""><span>桃牛商城</span></a></li>
       <li><a href="classified/index.aspx" title=""><span>哞哞叫</span></a></li>
       <li><a href="#" title=""><span>无名良品</span></a></li>
       <li><a href="#" title=""><span>吃喝玩乐</span></a></li>
       <li><a href="#" title=""><span>HiTao妆扮</span></a></li>
       <li><a href="#" title=""><span>桃牛娱乐</span></a></li>
   </ul>
   <div id="top-big-box">
        <div id="catList">
            <h2 class="catListTitle">所有商品分类</h2>
            <ul>
                <% Call getCatList()%>
            </ul>
        </div>
        <div id="hd_list_box_border">
            <div id="hd_list_box">
                <ol id="hd_imgList">
                    <%Call getHdImgList()%>
                </ol>
                <ol id="hd_titleList">
                    <%Call getHdTitleList()%>
                </ol>
            </div>
        </div>
        <div id="sall-best">
            <div id="tt">
                <ul>
                    <li><a id="bds" class="selected">最新公告</a></li>
                    <li><a id="ggs" href="search.aspx?ation=msha">秒杀专区</a></li>
                    <li><a id="rms">超值热卖</a></li>
                </ul>
            </div>
            <div id="bd" class="homeSwitch">
                <ul><%Call getGg()%></ul>   
            </div>
            <div id="gg" class="hidden homeSwitch">
                         
            </div>
            <div id="rm" class="hidden homeSwitch">
            
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <div id="floorBox">
        <div id="floor0">
            <div id="bbs">
                <div id="bbs-hot">
                     <h2>热帖</h2>
                    <script type="text/javascript" src="http://localhost/bbs/tools/showtopics.aspx?count=10&views=20&time=2&order=2&template=1"></script>               
                </div>
                <div id="bbs-new">
                     <h2>最新</h2>
                    <script type="text/javascript" src="http://localhost/bbs/tools/showtopics.aspx"></script>                      
                </div>
            </div>
            <div id="sns">
                <div id="sns-new">
                    <h2>正在发生</h2>
                    <ul>
                        <%getSnsPosts()%>                    
                    </ul>
                </div>
                <div id="users-lists">
                    <h2>这里的住户</h2>
                    <%getUserList()%>
                </div>
            </div>
        </div>
        <div id="floor1">
            <%Call getSallTejia()%>
        </div>
        <div id="floor2">
            <%Call getNewShops()%>
        </div>
        <div id="floor3">
            <!--免费-->
            <%Call getFree()%>
        </div>
        <div id="floor4">
            <!--赠券-->
            <%Call getZq()%>
        </div>
        <div id="floor5">
            <!--甩卖-->
            <%Call getShuai()%>
        </div>
        <div id="floor6">
            <!--秒杀-->
            <%Call getSallMshaBd()%>
        </div>
        <div id="floor7">
            <!--团购-->
            <%Call getTuan()%>
        </div>
        <div id="floor8">
            <!--团购-->
            <%Call getPi()%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
    <script type="text/javascript">
        //<![CDATA[
        tn.setNavHover();
        tn.setHdFloat();
        tn.setFloorHover();
        tn.setSwitchHome();
        tn.setFloor0("#sns-new", "#sns-new ul");
        //]]>
    </script>
</asp:Content>
