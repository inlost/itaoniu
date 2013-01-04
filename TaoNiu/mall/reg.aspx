<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/template/site_front_withSearch.master" CodeBehind="reg.aspx.vb" Inherits="TaoNiu.reg1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentContent" runat="server">
    <div id="regMallNav">
        <div>开设商城 >></div>
    </div>
    <div class="setupBox">
        <h2>申请流程</h2>
        <h3>轻松开设商城，只需4步就能搞定！</h3>
        <ul id="steupList" class="afterClear">
            <li id="setup1">
                <h4>做些准备</h4>
                <p>需要准备好商城的支付宝账号，企业工商执照复印件。</p>
            </li>
            <li id="setup2">
                <h4>在线申请并签约</h4>
                <p>在线考试、填写企业信息、并签订服务协议.</p>
            </li>
            <li id="setup3">
                <h4>提交资料并等待审核</h4>
                <p>线下提交公司及品牌资料,等待桃牛工作人员审核</p>
            </li>
            <li id="setup4">
                <h4>商城开设成功</h4>
                <p>商城开通，开张大吉</p>
            </li>
        </ul>
    </div>
    <div class="setupBox">
        <h2>在线申请</h2>
        <div id="regForm">
            <form method="post" action="servicePage/main_actions.aspx?action=reg" enctype="multipart/form-data">
                <ul class="afterClear">
                    <li><span class="titles">商城名称：</span><input type="text" id="mall_title" name="mall_title" /></li>
                    <li><span class="titles">商城工商执照：</span><input type="file" id="mall_License" name="mall_License"/></li>
                    <li><span class="titles">商城地址：</span><input type="text" id="mall_address" name="mall_address" /></li>
                    <li id="agreeAgreement"><input type="checkbox" value="agree" />同意并愿意遵守<a href="#" id="theAgreement">《桃牛商城开设协议》</a></li>
                    <li class="hidden" id="agreementBox">
                        <div>
                            <h3>桃牛商城开设协议</h3>
                            <p>sdafsdafsadf</p>
                        </div>
                    </li>
                    <li class="subLine"><input type="submit" value="提交" /></li>
                </ul>
            </form>
        </div>
    </div>
    <div class="setupBox">
        <h2 class="div_question">常见问题</h2>
        <div id="questions">
            <ul>
                <li class="theQ"><span class="qNo">Q1> </span>【最新】我公司在商城已有一家店铺，目前计划申请第二家，商城对此类申请什么样的标准？</li>
                <li class="theA">
                    <ul>
                        <li>第二家店铺的开店标准： </li>
                        <li>1、商城已有店铺需要通过试运营；如早期店铺未参加试运营考核，则按照现行试运营的标准进行考核；</li>
                        <li>2、产品重合度：要求店铺间经营的品牌及商品不得重复。</li>
                        <li>备注：</li>
                        <li>1、若申请第三家店铺，则前两家店铺均需满足以上标准，依此类推；</li>
                        <li>2、一个类目下专营店只能申请一家。</li>
                    </ul>
                </li>
                <li class="theQ"><span class="qNo">Q2> </span> 【最常问】入驻服饰、童装、家纺类目，除基本资质以外，还需哪些资质？</li>
                <li class="theA">
                    <p>申请服饰、家纺、童装类目的旗舰店、专卖店、专营店，每个品牌下需提交至少一份权威机构出具的质检报告。更多具体商品检测信息。</p>
                </li>

            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="server">
</asp:Content>
