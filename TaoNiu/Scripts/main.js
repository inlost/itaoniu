$(document).ready(function () {
	tn.regFormHover();
	tn.regInputClick();
	tn.regRenewVdImg();
	tn.regInputChange();
	tn.setDisplay();
	tn.regSubCheck();
});
vdForm={
	 valPassword:function (s){
			 var regu="^[a-zA-Z][0-9a-zA-Z_]{6,15}$";
			 re=new RegExp(regu);
			if (!re.test(s)) 
			{
				return false;
			}
			else
			{
				return true;
			} 
	   },
     valEmail:function(strEmail){
			if (strEmail.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) == -1)
			{
				return false;
			}
			else
			{
				return true;
			}
	   },
	valId:function(s){
		var regu = "^[0-9a-zA-Z]{5,16}$";
		var re = new RegExp(regu);
		if (!re.test(s)) 
		{
			return false;
		}
		else
		{
			return true;
		}
	},
	valRePass:function(p,r){
		if(p=="")return false;
		if(p==r){
			return true;
		}else{
			return false;
		}
	},
	valNumber:function(n){
		var patrn="^[0-9]{1,20}$";
		var re = new RegExp(patrn);
		return re.test(n)?true:false;
	}
};
tn={
	setElmHoverBg:function(elm,bgColor){
		if($(elm).length==0){return;}
		var tpColor='#fff';
		$(elm).hover(function(){
			$(this).css('background',bgColor);
		} ,function(){
			$(this).css('background',tpColor);
		});
	},
	regFormHover:function(){
		if($("#R_password").length==0){return;}
		var formList=$("#content li");
		$(formList).hover(function(){
			$(this).addClass("show_li");
		},function(){
			$(this).removeClass("show_li");
		});
	},
	regInputClick:function(){
		var netPro="false";
		$("#networkPro a").bind("click",function(){
			if(netPro=="false"){
				$("#J_TBAgreement").css("display","block");
				$(document).scrollTop(1000);
				netPro="ture";
			}else{
				$("#J_TBAgreement").css("display","none");
				netPro="false";
			}
		});
		if($("#R_password").length==0){return;}
		var inputList=$(".input input");
		var listClear=inputList.parent().parent();
		$(inputList).bind("click",function(){
			var fatherLi=$(this).parent().parent();
			listClear.find(".attention").css("display","none");
			if(fatherLi.find(".error").html()==""){
				fatherLi.find(".msg").css({"border":"1px solid #40b3ff","background":"#e5f5ff url(images/msg_bg.png) no-repeat left -145px"});
			}
			fatherLi.find(".attention").css("display","block");
		});
		$(inputList).bind("focusout",function(){
			if($(this).parent().parent().find(".error").html()==""){
				$(this).parent().parent().find(".msg").css({"border":"","background":""});
			}
		});
	},
	regRenewVdImg:function(){
		var rnLink=$("#renewVd");
		rnLink.bind("click",function(){
			var imgElm=$("#R_vdCode").find("img");
			imgElm.attr("src",imgElm.attr("src")+"?");
		});
	},
	regInputChange:function(){
		if($("#R_password").length==0){return;}
		var inputList=$(".input input");
		inputList.bind("keyup",regTest);
		inputList.bind("blur",regTest);
		function regTest(){
			var msgElm=$(this).parent().parent().find(".msg");
			switch ($(this).attr("name")){
				case "R_email":
					if(vdForm.valEmail($(this).val()) && $(this).val()!="")
					{
						$.ajax({
							type:"GET",
							url:"server_page/user_service.aspx?action=checkMail&mail=" +$(this).val(),
							success:function(msg){
								if(msg=="ok"){
									msgElm.css({"border":"","background":"url(images/msg_bg.png) no-repeat 0 -246px"});
									msgElm.find(".error").html("输入正确");
									msgElm.find(".error").css("display","block");
									msgElm.find(".error").removeClass("ckError");								
								}else{
									msgElm.css({"border":"1px solid #ff8080","background":"#fff2f2 url(images/msg_bg.png) no-repeat 0 0"});
									msgElm.find(".error").html("您输入的E-mail地址已经被注册。");
									msgElm.find(".error").css("display","block");
									msgElm.find(".error").addClass("ckError");								
								}
							},
							error:function(msg){
								alert(msg.status);
							}						
						});
					}else{
						msgElm.css({"border":"1px solid #ff8080","background":"#fff2f2 url(images/msg_bg.png) no-repeat 0 0"});
						msgElm.find(".error").html("请输入正确的E-mail地址");
						msgElm.find(".error").css("display","block");
						msgElm.find(".error").addClass("ckError");
					}
					break;
				case "R_password":
					if(vdForm.valPassword($(this).val()) && $(this).val()!="")
					{
						msgElm.css({"border":"","background":"url(images/msg_bg.png) no-repeat 0 -246px"});
						msgElm.find(".error").html("输入正确");
						msgElm.find(".error").css("display","block");
						msgElm.find(".error").removeClass("ckError");
					}else{
						msgElm.css({"border":"1px solid #ff8080","background":"#fff2f2 url(images/msg_bg.png) no-repeat 0 0"});
						msgElm.find(".error").html("请输入符合要求的密码");
						msgElm.find(".error").css("display","block");
						msgElm.find(".error").addClass("ckError");
					}
					break;
				case "R_id":
					if($(this).val().length>4)
					{
						$.ajax({
							type:"GET",
							url:"server_page/user_service.aspx?action=checkName&name=" +$(this).val(),
							success:function(msg){
								if(msg=="ok"){
									msgElm.css({"border":"","background":"url(images/msg_bg.png) no-repeat 0 -246px"});
									msgElm.find(".error").html("输入正确");
									msgElm.find(".error").css("display","block");
									msgElm.find(".error").removeClass("ckError");							
								}else{
									msgElm.css({"border":"1px solid #ff8080","background":"#fff2f2 url(images/msg_bg.png) no-repeat 0 0"});
									msgElm.find(".error").html("您输入的ID已经被别人注册了");
									msgElm.find(".error").css("display","block");
									msgElm.find(".error").addClass("ckError");								
								}
							},
							error:function(){
								alert(msg.status);
							}
						});
					}else{
						msgElm.css({"border":"1px solid #ff8080","background":"#fff2f2 url(images/msg_bg.png) no-repeat 0 0"});
						msgElm.find(".error").html("请输入符合要求的ID");
						msgElm.find(".error").css("display","block");
						msgElm.find(".error").addClass("ckError");
					}
					break;
				case "R_rePassword":
					if(vdForm.valRePass($(this).val(),$("#R_password").find("input").val()))
					{
						msgElm.css({"border":"","background":"url(images/msg_bg.png) no-repeat 0 -246px"});
						msgElm.find(".error").html("输入正确");
						msgElm.find(".error").css("display","block");
						msgElm.find(".error").removeClass("ckError");
					}else{
						msgElm.css({"border":"1px solid #ff8080","background":"#fff2f2 url(images/msg_bg.png) no-repeat 0 0"});
						msgElm.find(".error").html("两次输入的密码不一致");
						msgElm.find(".error").css("display","block");
						msgElm.find(".error").addClass("ckError");
					}
					break;
				default:
					if($(this).val()!="")			
					{
						msgElm.css({"border":"","background":"url(images/msg_bg.png) no-repeat 0 -246px"});
						msgElm.find(".error").html("已输入");
						msgElm.find(".error").css("display","block");
						msgElm.find(".error").removeClass("ckError");
					}else{
						msgElm.css({"border":"1px solid #ff8080","background":"#fff2f2 url(images/msg_bg.png) no-repeat 0 0"});
						msgElm.find(".error").html("请输入验证码");
						msgElm.find(".error").css("display","block");
						msgElm.find(".error").addClass("ckError");
					}
					break;
			}
		}
	},
	regSubCheck:function(){
		$("form").submit(function(){
			$(".input input").keyup();
			if($(".ckError").length==0){
				return true;
			}else{
				return false;
			}
		});
	},
	setNavHover:function(){
		$(".catItems").bind("mouseover",function(){
			var pos = $(this).position();
			var topPos=$("#catList").position();
			var navShow=$(this).find(".catNavShow");
			var navLeft=navShow.find(".catNavMainLeft");
			var navRight=navShow.find(".catNavMainRight");
			$("#floorBox").hover(function(){
				$(".catNavShow").css("display","none");
			});
			$(".catNavShow").css("display","none")
			navShow.css("display","block");
			navShow.css("top",topPos.top-19);
			navShow.css("left",pos.left+120);
			navShow.bind("mouseout",function(){
				navShow.css("display","none");
			});
			if ($.browser.msie && $.browser.version=="6.0"){
				navLeft.css("top",pos.top-135);
			}else{
				navLeft.css("top",pos.top-144);
			}
		}); 
	},
	setHdFloat:function(){
		var titleList=$("#hd_titleList li a");
		var nowItem=1;
		var toItem=2;
		$("a[tohere='1']").css("background","#000");
		var interval=setInterval(function(){
				tn.hdInterval(nowItem,toItem);
				nowItem=toItem;
				toItem+1>4?toItem=1:toItem++;
			},3000);
		titleList.bind("mouseover",function(){
			clearInterval(interval);
			$("#hd_imgList").stop();
			toItem=$(this).attr("tohere");
			tn.hdInterval(nowItem,toItem);
			nowItem=toItem;
			toItem+1>4?toItem=1:toItem++;
			$(this).css("background","#000");
		});
		titleList.bind("mouseout",function(){
			interval=setInterval(function(){
				tn.hdInterval(nowItem,toItem);
				nowItem=toItem;
				toItem+1>4?toItem=1:toItem++;
			},3000);
		});
	},
	hdInterval:function(nowIs,toIs){
		var imgListBox=$("#hd_imgList");
		var posPx=-500*(toIs-1);
		$("#hd_titleList li a").css("background","#151515");
		$("a[tohere='"+toIs+"']").css("background","#000");
		imgListBox.animate({ 
			marginLeft:posPx
		},600);
	},
	setSearchBox:function(){
		var searchBox=new Array($("#queryStr"),$("#sRange2"));
		var btn=$("#searchBox form button");
		for(s in searchBox){
			searchBox[s].css("color","#ccc");
			searchBox[s].focusin(function(){
				$(this).val("");
				$(this).css("color","#000");
			});
		};
		searchBox[0].focusout(function(){
			if($(this).val().length==0){
				$(this).val("输入您想寻找的宝贝");
				$(this).css("color","#ccc");
			}
		});
		searchBox[1].focusout(function(){
			if($(this).val().length==0){
				$(this).val("地点");
				$(this).css("color","#ccc");
			}
		});
		btn.hover(function(){
			$(this).css("backgroundPosition","right 0");
		},function(){
			$(this).css("backgroundPosition","right bottom");
		});
	},
	setSearchOrder:function(){
		var hSelect=$("#J_FilterToolbar .hoverMenu");
		var hList=$("#J_OrderByList");
		var hList_li=$("#J_OrderByList li");
		var orderTitle=$("#J_OrderSelector span span");
		var spanAddClass="";
		var bool="false";
		hSelect.hover(function(){
			hList.css("display","block");
			hList_li.hover(function(){
				$(this).css("backgroundColor","#eee");
			},function(){
				$(this).css("backgroundColor","#fff");

			});
		},function(){
			if($.browser.version=="6.0"){
				$(document.body).click(function(){
					hList.css("display","none");
				});
			}else{
				hList.css("display","none");
			}
		});
		(function order(){
			var ads=location.href;
			var adsLength=ads.split("=");
			var href=adsLength[adsLength.length-2];
			var tit=$("#J_FilterToolbar .mode a span").html();
			var url="";
			if(tit=="切换到列表"){
				$("#J_FilterToolbar .mode a").addClass("modeList");
			}
			if(href.indexOf("goods_prize") == 0){
				url="one";
			}else if(href.indexOf("goods_pj_good") == 0){
				url="two";
			}else if(href.indexOf("goods_number_now") == 0){
				url="three";
			}else{
				return ;
			}
			switch(url){
				case "one":
					if(adsLength[adsLength.length-1] == "asc"){
						orderTitle.text("价格从低到高").addClass("by-price-asc");
					}else{
						orderTitle.text("价格从高到低").addClass("by-price-desc");
					}
					break;
				case "two":
					orderTitle.text("信用从高到低").addClass("by-credit-desc");
					break;
				case "three":
					orderTitle.text("销量从高到低").addClass("by-sale-desc");
					break;
			}
		})();
	},
	setFloorHover:function(){
		var floors=$("#floorBox > div");
		floors.find("li").css("opacity","0.6");
		floors.hover(function(){
			$(this).css("background-position","-960px 0");
			$(this).find("li").css("opacity","0.8");
		},function(){
			$(this).css("background-position","0 0");
			$(this).find("li").css("opacity","0.6");
		});
		floors.find("li").hover(function(){
			$(this).css("opacity","1");
			$(this).css("border","1px solid #741d26");
			$(this).css("background","#eee");
			$(this).find("h3").css("opacity","0.9");
		},function(){
			$(this).css("opacity","0.8");
			$(this).css("border","1px solid #fff");
			$(this).css("background","#fff");
		});
	},
	setFloor0:function(obj1,obj2){
		var scrtime;
		$("#bbs-new p a:odd").css("color","#0082CB");
        $(obj1).hover(function(){
                clearInterval(scrtime);
        },function(){
			scrtime = setInterval(function(){
                var $ul = $(obj2);
                var liHeight = $ul.find("li:last").height();
                $ul.animate({marginTop : liHeight +"px"},1000,function(){
					$ul.find("li:last").prependTo($ul)
					$ul.find("li:first").hide();
					$ul.find("li:first").fadeIn(500);
					$ul.css({marginTop:0});
                });
			},5000);
        }).trigger("mouseleave");
	},
	checkNumber:function(){
		var elmInput=$("#number");
		$("#order").submit(function(){
			if(vdForm.valNumber(elmInput.val()) && Number(elmInput.val())<Number(elmInput.attr("rel"))){
				return true;
			}else{
				alert("请输入正确的购买数量");
				elmInput.focus();
				return false;
			}
		});
	},
	checkOrder:function(){
		var elmForm=$("#order");
		elmForm.submit(function(){
			if(!vdForm.valNumber($("#number").val())){
				alert("请输入正确的数量");
				return false;
			}
			if($("#recName").val().length<2){
				alert("请输入正确的收货人姓名");
				return false;			
			}
			if($("#recAddress").val().length<7){
				alert("请输入详细的收获地址");
				return false;			
			}
			if(!vdForm.valNumber($("#recPhone").val()) || $("#recPhone").val().length!=11){
				alert("请输入正确的手机号码");
				return false;			
			}
			return true;
		});
	},
	setGoodInfoTab:function(){
		var linkList=$("#funList li a");
		linkList.bind("click",function(){
			var action;
			var strHref=$(this).attr("href");
			switch(strHref)
			{
				case "#info":
					action="info";
					break;
				case "#comments":
					action="pj";
					break;
				case "#log":
					action="orders";
					break;
				case "#service":
					action="services";
					break;			
				case "#liuYan":
					action="liuYan";
					break;
			}
			linkList.removeClass("selected");
			$(this).addClass("selected");
			var divShow=$("#info");
			$.ajax({
				type:"GET",
				url:"server_page/shop_goods.aspx?action=" + action +"&gid=" +divShow.attr("rel"),
				success:function(msg){
					divShow.html(msg)
				},
				error:function(msg){
					alert(msg.status);
				}
			});
		});
	},
	setSwitchHome:function(){
		var switchItems=$("#tt a");
		switchItems.hover(function(){
			switchItems.removeClass("selected");
			var linkNow=$(this);
			linkNow.addClass("selected");
			var boxActed="#"+linkNow.attr("id");
			boxActed=boxActed.slice(0,boxActed.length-1);
			var boxes=$(".homeSwitch");
			boxes.addClass("hidden");
			boxActed=$(boxActed);
			boxActed.removeClass("hidden");
		});
	},
	setSearchBy:function(){
		var eachBy=$("#searchBy li");
		eachBy.click(function(){
			eachBy.removeClass("selected");
			$(this).addClass("selected");
		});
	},
	setRegFom:function(data){
		if(data!=false){
			var email=$("#R_email input");
			var niceName=$("#R_id input");
			email.val(data.uin+"@qq.com");
			niceName.val(data.nick);
		}
	},
	setSearchList:function(){
		var shanjiao=$("#shanjiao");
		var sRange1=$("#sRange1");
		var sRange2=$("#sRange2");
		shanjiao.bind("click",function(){
			sRange1.css("display","block").hover(function(){
				$(this).css("display","block").children("li").hover(function(){
					$(this).bind("click",function(){
						sRange2.val($(this).text()).css("color","#000");
					}).css({"background":"#144899","color":"#fff"});
				},function(){
					$(this).css({"background":"#fff","color":"#000"});
				});
			},function(){
				$(this).css("display","none");
			});
		});
	},
	setRegFormInfo:function(){
		$.getScript('http://newdata.qq.com/newLoginFull.q?callback=tn.setRegFom',function(){});	
	},
	liAddClass:function(){ //模版导航插入类
		$("#inCatNav li").hover(function(){
			$(this).addClass("li_class");
		},function(){
			$(this).removeClass("li_class");
		});
	},
	setOnline:function(deep){
		var servicePagePath=deep==1?"server_page/alive.aspx":"../server_page/alive.aspx";
		setInterval(function(){
			$.ajax({
				type:'POST',
				url:servicePagePath,
				data:'action=imOnLine',
				success:function(data){},
				error:function(e){}
			});		
		},30000);
	},
	setDisplay:function(){
		var brv=$.browser.version;
		if(brv=="6.0"){
			$("#funNav").css("display","none");
		}else if(brv=="8.0"|| brv=="7.0"){
			$("#funNav").css("visibility","hidden");
		}
	},
	feedback:function(){
		var here=$("#here");
		var offset=here.offset();
		if($.browser.version=="6.0"){
			$(window).scroll(function(){
				var feedT=$(document).scrollTop();
				here.css("top",feedT+offset.top);
			});
		}
		$("#here").bind("click",function(){
			var feedTop=$(document).scrollTop();
			var feedback=$("#feedback");
			feedTop=feedback.height()+feedTop;
			feedback.css({"display":"block","top":feedTop});
			$("#feedClose").bind("click",function(){
				feedback.css("display","none");
			}).hover(function(){
				$(this).css("backgroundPosition","-37px -30px");
			},function(){
				$(this).css("backgroundPosition","0 -30px");
			});
			
		});
	}
};
userCenter={
	setGoodsCat1:function(){
		userCenter.setGoodsCat(1,0,"#goods_add_1 #cat1");
	},
	setGoodsCat:function(deep,father,elm){
		$.ajax({
			type:'POST',
			url:'server_page/shop_goods.aspx?action=goods_cat',
			data:"father=" + father +"&deep=" +deep,
			success:function(msg){
				$(elm).empty();
				$(elm).append(msg);
				userCenter.setCatChange(deep,elm);
			},
			error:function(msg){
				alert(msg.status);
			}
		});
	},
	setCatChange:function(deep,elm){
		if(deep==4){return;}
		var items=$(elm);
		items.bind("change",function(){
			if(deep==1){
				$("#goods_add_1 #cat3").empty();
			}
			if($(this).find("option").length==0){return;}
			var fatherCat=$(this).val();
			var deepNew=deep+1;
			var elmNew="#goods_add_1 #cat" +deepNew;
			$(elmNew).empty();
			userCenter.setGoodsCat(deepNew,fatherCat,elmNew);
		});
	},
	catReadyAdd:function(){
		var cat3=$("#cat3");
		var cat4=$("#cat4");
		var textInput=$("#new_cat");
		var linkSub=$("#add_new_cat");
		linkSub.bind("click",function(){
			if(cat3.attr("selectedIndex")==-1){
				alert("您还没有选择三级分类");
			}else{
				if(textInput.val().length==0){
					alert("分类名称不能为空");
					return false;
				}
				$.ajax({
					type:"POST",
					url:"server_page/shop_goods.aspx?action=cat_ready_add",
					data:"catName=" +textInput.val()+"&father="+cat3.val(),
					success:function(msg){
						cat4.append("<option value='" +msg +"'>"+textInput.val() +"</option>");
						if(msg==-1){alert("分类审核中");}
						textInput.val("");
					},
					error:function(msg){
						alert(msg.status);
					}
				});
			}
			return false;
		});
	},
	setButtomCatOk:function(){
		//添加商品-下一步按钮
		var bt=$("#catOk");
		var strCat;
		var check;
		bt.bind("click",function(){
			strCat="";
			check=true;
			$(".goods_cat").each(function(){
				var strNow=$(this).attr("selectedIndex");
				if(strNow==-1){
					if($(this).find("option").length!=0){
						alert("请选择完整的分类");
						check=false;
						return;
					}
				}else{
					strNow=this.options[strNow].value;
					strCat.length==0?strCat+=strNow:strCat+="-" +strNow;				
				}
			});	
			if(!check){return;}
			$("#goods_cat").attr("value",strCat);
			$("#goods_add_1").addClass("hidden");
			$("#goods_add_2").removeClass("hidden");
			var shelfBox=$("#shelf");
			$("#back").bind('click',function(){
				$("#goods_add_1").removeClass("hidden");
				$("#goods_add_2").addClass("hidden");			
			});
			$.ajax({
				type:"POST",
				url:"server_page/shop_goods.aspx?action=shelfList",
				data:"shopId=" +$("#shopId").val(),
				success:function(msg){
					shelfBox.empty();
					shelfBox.append(msg);
				},
				error:function(msg){
					alert(msg.status);
				}
			});
			var goodPropertyBox=$("#goods_property");
			var goodsCat=$("#goods_cat").val();
			$.ajax({
				type:"POST",
				url:"server_page/shop_goods.aspx?action=getProperty",
				data:"cat=" +goodsCat,
				success:function(msg){
					goodPropertyBox.empty();
					goodPropertyBox.append(msg);
				},
				error:function(msg){
					alert(msg.status);
				}
			});
		});
		userCenter.setAddGoodsListen();
		userCenter.setChuChuang();
	},
	setChuChuang:function(){
		$.ajax({
				type:"POST",
				url:"server_page/shop_goods.aspx?action=chuchuang",
				data:"shopId=" +$("#shopId").val(),
				success:function(msg){
					$("#goods_tuijian").append(msg);
				},
				error:function(msg){
					alert(msg.status);
				}			
		});
	},
	setRangeChangeListen:function(){
		var range1=$("#range1");
		var range2=$("#range2");
		var rangeTime=$("#rangeTime");
		var range=$("#goods_range");
		var range_add=$("#range_add");
		var range_reSet=$("#range_reset");
		/*
		range1.bind("change",function(){
			var father=range1.val();
			$.ajax({
				type:"get",
				url:"server_page/shop_goods.aspx?action=getRange&father=" + father,
				success:function(msg){
					range2.empty();
					range2.append(msg);
				},
				error:function(msg){
					alert(msg.status);
				}							
			});
		});
		*/
		range_add.bind("click",function(){
			if (range2.val().length<1 ){
				alert("请填写完整的范围");
			}else{
				range.val(range.val()+range1.val()+"-"+range2.val()+":"+rangeTime.val()+"|")
				range.after("<p class='ranges halfWidth'><span class='infoTitle'></span><span class='info'>"+range1.find("option:selected").text()+"-"+range2.val()+":"+rangeTime.find("option:selected").text()+"</span></p>");
				range2.val("");			
			}
		});
		range_reSet.bind("click",function(){
			range.val("");
			$(".ranges").remove();
		});
	},
	setAddGoodsListen:function(){
		var discountList=$(".discount");
		if (discountList.length==0){return;}
		//折扣下拉列表变化
		discountList.bind("change",function(){
			var inputDiscount=$("#goods_discount");
			var dsTp = $("#discount_1").val()+$("#discount_2").val();
			inputDiscount.val(dsTp);
		});
		var infoAdd=$("#infoAdd");
/*-----------------添加商品属性-------------------------/
		var infoAdd=$("#infoAdd");
		infoAdd.bind("click",function(){
			var elmKey=$("#info_key");
			var elmValue=$("#info_value");
			if (elmKey.val().length==0 || elmValue.val().length==0){
				alert("属性名称与属性的值均不能为空，请检查您的输入")
			}else{
				var infoBox=$("#infoList");
				var infoTp="<p class='halfWidth'><span class='infoTitle_key' class='infoTitle'>" + elmKey.val() +"：</span><span class='infoTitle_value' class='info'>" +elmValue.val() +"</span></p>";
				infoBox.append(infoTp);
				var infoRelPost=$("#goods_info");
				if (infoRelPost.val().length==0){
					infoTp=elmKey.val() +"=" +elmValue.val();
				}else{
					infoTp=infoRelPost.val()+"-"+elmKey.val() +"=" +elmValue.val();
				}
				infoRelPost.val(infoTp);
				elmKey.val("");
				elmValue.val("");
			}
		});
/-------------------------------------------------------------*/
		var sale_time_type_list=$("#on_sale_date_type");
		sale_time_type_list.bind("change",function(){
			var sale_year=$("#sale_year");
			var sale_month=$("#sale_month");
			var sale_day=$("#sale_day");
			var sale_hour=$("#sale_hour");
			var sale_min=$("#sale_min");
			var tips=$(".tips");
			var saleTime=$(".saleTime");
			var saleTimeElm=$("#goods_on_sale_date");
			if($(this).val()!="now"){
				saleTime.removeClass('hidden');
				tips.removeClass('hidden');
				saleTime.bind("change",function(){
					var timeTp;
					timeTp=sale_year.val() + "/" + sale_month.val() + "/" + sale_day.val() +" " + sale_hour.val() + ":" + sale_min.val() +":00";
					saleTimeElm.attr("oldTime",saleTimeElm.val());
					saleTimeElm.val(timeTp);
				});
			}else{
				saleTimeElm.val(saleTimeElm.attr("oldTime"));
				saleTime.addClass('hidden');
				tips.addClass('hidden');		
			}
		});
	},
	setGoodsType:function(){
		//商品类型初始化
		var elmType=$("#goods_type");
		$.ajax({
			type:'GET',
			url:'server_page/shop_goods.aspx?action=goods_type',
			success:function(msg){
				elmType.append(msg);
			},
			error:function(msg){
				alert(msg.status);
			}
		});
		elmSaleType=$("#goods_sale_type");
		$.ajax({
			type:'GET',
			url:'server_page/shop_goods.aspx?action=goods_sale_type',
			success:function(msg){
				elmSaleType.append(msg);
			},
			error:function(msg){
				alert(msg.status);
			}
		});
	},
	addShopInfoListen:function(){
		//监听商店类表变化
		var shopList=$("#shopList");
		if(shopList.length==0){return;}
		shopList.bind("change",function(){
			userCenter.setShopInfo();
		});
		var shelfAdd = $("#addShelf");
		shelfAdd.bind("click",function(){
			var shopList=$("#shopList");
			var shopNow=shopList.val();
			var shelfName=$("#shelfName").val();
			$.ajax({
				type:"POST",
				url:"server_page/shop_goods.aspx?action=add_shop_shelf",
				data:"shopId=" +shopNow +"&shelfName=" +shelfName,
				success:function(msg){
					if(msg==0){
						alert("无法与服务器交换数据");
					}else if(msg=="infoError"){
						alert("请检查您输入信息");
					}else{
						var listBox=$("#inShelfList");
						listBox.append(msg);
						$("#shelfName").val("");
						userCenter.setDelShelfLink();
					}
				},
				error:function(msg){
					alert(msg.status);			
				}
			});
		});
	},
	setShopInfo:function(){
		//获取当前商店的信息
		var shopList=$("#shopList");
		if(shopList.length==0){return;}
		var shopNow=shopList.val();
		$.ajax({
			type:"POST",
			url:"server_page/shop_goods.aspx?action=getShopInfo",
			data:"shopId=" + shopNow,
			success:function(msg){
				$("#shopInfo").empty();
				$("#shopInfo").append(msg);
			},
			error:function(msg){
				alert(msg.status);			
			}
		});
		$.ajax({
			type:"POST",
			url:"server_page/shop_goods.aspx?action=getShopShelf",
			data:"shopId=" + shopNow,
			success:function(msg){
				$("#inShelfList").empty();
				$("#inShelfList").append(msg);
				userCenter.setDelShelfLink();
				userCenter.setShelfChangeListen();
			},
			error:function(msg){
				alert(msg.status);			
			}
		});
	},
	setDelShelfLink:function(){
		//初始化删除货架连接
		var linkList=$("#inShelfList .subBt");
		if(linkList.length==0){return;}
		linkList.each(function(){
			var elmSelect=$(this).next(".moveToShelf");
			$(this).attr("oldLink",$(this).attr("href"));
			$(this).attr("href",$(this).attr("href")+elmSelect.val());
		});
	},
	setShelfChangeListen:function(){
		//删除货架连接下拉框改变触发
		var slList=$("#inShelfList .moveToShelf");
		if(slList.length==0){return;}
		slList.each(function(){
			var elmLink=$(this).prev(".subBt");
			$(this).bind("change",function(){
				elmLink.attr("href",elmLink.attr("oldLink")+$(this).val());
			});
		});
	},
	mySubmit:function(){
		//商品发布验证检查
		$("#catOk").bind("click",function(){
			$check_val=$("#goods_sale_type input:checked").next().html();
			$discount_1=$("#discount_1");
			$discount_2=$("#discount_2");
			$goods_prize=$("#goods_prize");
			$select_option=$("#discount_1 option");
			
			var input_disabled={
				dis_true:function(){
					$discount_1.attr("disabled",true);
					$discount_2.attr("disabled",true);
				},
				dis_false:function(){
					$discount_1.attr("disabled",false);
					$discount_2.attr("disabled",false);
					$goods_prize.val('').attr("disabled",false);
				},
				option_none_1:function(){
					for(var i=0;i<5;i++){
						$select_option.eq(i).css("display","none");
						$select_option.eq(5).attr("selected",true);
					}
				},
				option_none_2:function(){
					for(var i=5;i<10;i++){
						$select_option.eq(i).css("display","none");
						$select_option.eq(0).css("display","none");
						$select_option.eq(1).attr("selected",true);
					}
				},
				option_block:function(){
					for(var i=0;i<10;i++){
						$select_option.eq(i).css("display","block");
						$select_option.eq(0).attr("selected",true);
					}
				},
				goods_block:function(){
					$goods_prize.val('').css("display","block");
					$(".teshu").css("visibility","visible");
				}			
			};
			switch($check_val){
				case '一口价':
					input_disabled.option_block();
					input_disabled.dis_true();
					input_disabled.goods_block();
					break;
				case '折扣（可选5-9折）':
					input_disabled.dis_false();
					input_disabled.option_block();
					input_disabled.option_none_1();
					input_disabled.goods_block();
					break;
				case '秒杀':
					input_disabled.dis_false();
					input_disabled.option_block();
					input_disabled.option_none_2();
					input_disabled.goods_block();
					break;
				case '免费':
					input_disabled.option_block();
					input_disabled.dis_true();
					$goods_prize.val('0').css("display","none");
					$(".teshu").css("visibility","hidden");
					break;
				case '甩卖（可选1-4折）':
					input_disabled.dis_false();
					input_disabled.option_block();
					input_disabled.option_none_2();
					input_disabled.goods_block();
					break;
			}
		});
		$("#subForm").bind("click",function(){
			var goods_title=$("#goods_title");
			var goods_prize=$("#goods_prize");
			// var info_key=$("#info_key");
			// var info_value=$("#info_value");
			var imgFileType=$("#imgFile");
			var goods_introduce=$("#goods_introduce");
			var goods_number=$("#goods_number");
			var goods_range=$("#goods_range");
			var goods_warranty_other=$("#goods_warranty_other");
			var infotitle_key=$(".infoTitle_key");
			var str="";
			var strCh="";
			var strSe="";
			var strChHi="";
			var strSeHi=""
			var myS={
				formVal:function (txt){
					if(txt.val()=="" || txt.val()==null){
						return true;
					}
				},
				formAlt:function (val){
					alert("请输入" + val.parent().parent().children().text().split("：")[0] + "!");
					val.focus();
					return false;
				}
			};
			if($(".goodInfos select option:selected").length >0 ){
				$(".goodInfos select option:selected").each(function(){
					strSeHi="-"+$(this).parent().parent().children("input:hidden").val();
					strSe +=strSeHi+"="+$(this).val();
				});
			}
			if($(".goodInfos input:checkbox").length > 0){
				if($(".goodInfos input:checked").length > 0){
					$(".goodInfos input:checked").each(function(){
						strChHi=$(this).parent().children("input:hidden").val();
						strCh +="、"+$(this).val();
					});
					strCh=strChHi+"="+strCh.substring(1);
				}else{
					strSe=strSe.substring(1);
				}
			}
			str=strCh+strSe;
			if(str != ""){
				$("#goods_info").val(str);
			}
			if(myS.formVal(goods_title)){
				return myS.formAlt(goods_title);
			}
			if(myS.formVal(goods_prize)){
				return myS.formAlt(goods_prize);
			}
			if(myS.formVal(imgFileType)){
				alert("请选择商品图片！");
				imgFileType.focus();
				return false;
			}
			
			if(imgFileType.val() != "" || imgFileType.val() != null){
				if(!userCenter.imgType(imgFileType)){
					return false;
				}else 
				// if(infotitle_key.text()=="" || infotitle_key.text()==null){
					// if(myS.formVal(info_key)){
						// return myS.formAlt(info_key);
					// }else if(myS.formVal(info_value)){
						// alert("商品属性值不能为空！");
						// info_value.focus();
						// return false;
					// }else{
						// alert("商品属性尚未添加，请点击添加按钮！");
						// return false;
					// }
				// }else
				if(myS.formVal(goods_introduce)){
					alert("商品介绍不能为空！");
					goods_introduce.focus();
					return false;
				}else if(myS.formVal(goods_number)){
					return myS.formAlt(goods_number);
				}else if(myS.formVal(goods_range)){
					alert("送货范围不能为空！");
					goods_range.focus();
					return false;
				}else if(myS.formVal(goods_warranty_other)){
					alert("保修条款不能为空！");
					goods_warranty_other.focus();
					return false;
				}
			}
		});
	},
	imgType:function(imgVal){
		var imgAunt=imgVal.val().split(".");
		var imgtype=imgAunt[imgAunt.length - 1];
		switch(imgtype.toLowerCase()){
			case "gif":return true;	break;
			case "jpg":return true;	break;
			case "jpeg":return true; break;
			case "png":return true;	break;
			case "bmp":return true;	break;
			default:
				alert("商品图片格式不正确,请重新选择！");
				imgVal.val("").focus();
				return false;
			}
	},
	sale_choseGood:function(){
		var typeVal=$("#shareType").val();
		if(typeVal=="all"){
			$("#choseGood").css("display","none");
		}else{
			$("#choseGood").css("display","block");
		}
		$("#shareType").bind("change",function(){
			if($(this).val()=="all"){
				$("#choseGood").css("display","none");
			}else{
				$("#choseGood").css("display","block");
			}
		});
		$("#choseGood").bind("click",function(){
			if($.browser.version !== "6.0"){
					$("#div_opacity").css({"width":$(window).width(),"height":$("body").height()});
			}
			$("#shelfChoice").fadeIn("fast");	
			var sale_shop=$("#ctl00_ContentContent_shop").val();
			$.ajax({
				type:"POST",
				url:"server_page/shop_goods.aspx?action=shelfList",
				data:"shopId=" + sale_shop,
				success:function(msg){
					$("#sale_S_one").empty().append(msg);
					userCenter.sale_S_change(sale_shop);
					$(".btn_close .btn_img").bind("click",function(){
						$("#shelfChoice").css("display","none");
						$("input[name=gid]").val(-1);
						$("#sale_S_two").html('');
						$("#choseGood").text("点击选择商品");
					});
					userCenter.shelf_val();
					$(".btn_cancell").bind("click",function(){
						$("#shelfChoice").css("display","none");
						$("input[name=gid]").val(-1);
						$("#sale_S_two").html('');
						$("#choseGood").text("点击选择商品");
					});
					$(".btn_submit").bind("click",function(){
						var s_two_val=$("#sale_S_two").val();
						if(s_two_val=="" || s_two_val==null){
							$(".alter_font").css("visibility","visible");
						}else{
							$(".alter_font").css("visibility","hidden");
							$("#shelfChoice").css("display","none");
							var textVal=$("#sale_S_two :selected").text();
							$("#choseGood").text(textVal).attr("title",textVal);
						}
					});
					
				},
				error:function(msg){
					alert(msg.status);
				}
			});
		});
	},
	sale_S_change:function(shopVal){
		$("#sale_S_one").bind("change",function(){
			$(".alter_font").css("visibility","hidden");
			$.ajax({
				type:"POST",
				url:"server_page/shop_goods.aspx?action=goodList",
				data:"shopId=" + shopVal + "&shelfId=" + $(this).val(),
				success:function(msg){
					$("#sale_S_two").empty().append(msg);
				},
				error:function(msg){
					alert(msg.status);
				}
			});		
		});
	},
	shelf_val:function(){
		$("#sale_S_two").bind("change",function(){
			$("input[name=gid]").val($(this).val());
		});
	},
	setDifPrice:function(){
		/*同款异价*/
		var isDifPrice=$("#isDifPrice");
		var difPriceBox=$("#difPrice");
		isDifPrice.bind("click",function(){
			if(isDifPrice.attr("checked")==true){
				difPriceBox.removeClass("hidden");
			}else{
				difPriceBox.addClass("hidden");
			}
		});
		var colors=$("input[name=difPrice_color]");
		var sizes=$("input[name=difPrice_size]");
		var packages=$("input[name=difPrice_packge]");
		var checkBoxs=$("#difPrice input[type=checkbox]");
		var priceInputBox=$("#difPrice_price");
		checkBoxs.bind("click",function(){
			var hasChecked=new Array();
			var colorss=colors.filter(function(index){
				return colors.eq(index).attr("checked")==true?true:false;
			});
			if (colorss.length>0){hasChecked.push(colorss);}
			var sizess=sizes.filter(function(index){
				return sizes.eq(index).attr("checked")==true?true:false;
			});
			if (sizess.length>0){hasChecked.push(sizess);}
			var packagess=packages.filter(function(index){
				return packages.eq(index).attr("checked")==true?true:false;
			});
			if (packagess.length>0){hasChecked.push(packagess);}
			if (hasChecked.length==0){priceInputBox.html("");return;}
			var strWite="";
			switch(hasChecked.length){
				case 1:
					hasChecked[0].each(function(){
						strWite+="<p>\""+$(this).val()+ "\"&nbsp;价格：<input type='text'/>元</p>";
					});
					break;
				case 2:
					hasChecked[0].each(function(){
						var outside1=$(this).val();
						hasChecked[1].each(function(){
							strWite+="<p>\""+outside1+ "-" + $(this).val() +"\"&nbsp;价格：<input type='text'/>元</p>";	
						});
					});			
					break;
				case 3:
					hasChecked[0].each(function(){
						var outside1=$(this).val();
						hasChecked[1].each(function(){
							var outside2=$(this).val();
							hasChecked[2].each(function(){
								strWite+="<p>\""+ outside1+ "-" +outside2+"-"+$(this).val()+"\"&nbsp;价格：<input type='text'/>元</p>";	
							});
						});
					});								
					break;
			}
			priceInputBox.html(strWite);
		});
	}
};
buySale={
	favorites_good:function(){
		var link=$("#sc_good");
		link.bind("click",function(){
			$.ajax({
				type:'get',
				url:link.attr("href")+ "&by=ajax",
				success:function(msg){
					if(msg.length>10){
						window.location.href=link.attr("href");
					}else{
						alert("收藏成功");
					}
				},error:function(msg){
					alert(msg.status);
				}
			});
			return false;
		});
	},
	favorites_shop:function(){
		var link=$("#sc_shop");
		link.bind("click",function(){
			$.ajax({
				type:'get',
				url:link.attr("href")+ "&by=ajax",
				success:function(msg){
					if(msg.length>10){
						window.location.href=link.attr("href");
					}else{
						alert("收藏成功");
					}
				},error:function(msg){
					alert(msg.status);
				}
			});
			return false;
		});
	},
	gwc_add:function(){
		var link=$("#toBox");
		link.bind("click",function(){
			$.ajax({
				type:'POST',
				url:link.attr("href")+ "&by=ajax",
				data:"number=" + $("#number").val(),
				success:function(msg){
					alert("加入购物车成功");
					var numberSpan=$("#gwc span");
					if (numberSpan.html()==""){
						numberSpan.html("1");
					}else{
						numberSpan.html(parseInt(numberSpan.html())+1);
					}
				},error:function(msg){
					alert(msg.status);
				}
			});
			return false;
		});		
	},
	gwc_number_modify:function(){
		var modifyLinks=$(".number a");
		modifyLinks.bind("click",function(){
			var link=$(this);
			var inputBox=link.parent().find("input");
			$.ajax({
				type:"POST",
				url:link.attr("uri"),
				data:"number=" + inputBox.val(),
				success:function(msg){
					alert("修改成功");
				},error:function(msg){
					alert(msg.status);
				}
			});
			return false;
		});
	},
	gwc_del:function(){
		var modifyLinks=$(".gwc_del");
		modifyLinks.bind("click",function(){
			var link=$(this);
			var theBox=link.parent().parent();
			$.ajax({
				type:"get",
				url:link.attr("uri"),
				success:function(msg){
					theBox.slideUp("fase",function(){
						theBox.remove();		
					});
					var numberSpan=$("#gwc span");
					if (numberSpan.html()==1){
						numberSpan.html("");
					}else{
						numberSpan.html(parseInt(numberSpan.html())-1);
					}
				},error:function(msg){
					alert(msg.status);
				}
			});
			return false;
		});	
	},
	buySelect:function(){
		var price_select=$("#price_select");
		var price_select_inOne=$("#price_select_inOne");
		var price=$("#price").html();
		var tit="";
		var str="";
		var strT="";
		var changeT="";
		price_select.children("ul").children(":first-child").each(function(){
			strT=$(this).text();
			str +=" \""+strT.split("：")[0]+"\"";
			$(".tp").text("请选择："+str);
		});
		price_select.children("ul").children("li").children("a").bind("click",fun);
		function fun(){
			$(".tp").text("已选择：");
			$(this).parent().parent().children("li").each(function(){
				if($(this).children("a").hasClass("selected")){
					$(this).children("a").removeClass("selected");
				}
			});
			$(this).addClass("selected");
			tit=$(this).parent().parent().attr("id").split("_");
			if($("#select ."+tit[tit.length-1]).length>0){
				$("#select ."+tit[tit.length-1]).text(" \""+$(this).text()+"\"");
			}else{
				$("#select").append("<span class="+tit[tit.length-1]+">&nbsp;\""+$(this).text()+"\"</span>");
			}
				changeT=$(this).text();
				switch(changeT){
					case "套餐一":
							$("#price").html(price-parseInt(price/$(this).attr("cs")));
							// alert($(this).attr("cs"));
						break;
					case "套餐二":
							$("#price").html(price-parseInt(price/$(this).attr("cs")));
							// alert($(this).attr("cs"));
						break;
					case "套餐三":
							$("#price").html(price-parseInt(price/$(this).attr("cs")));
							// alert($(this).attr("cs"));
						break;
				}
			
		}
	},
	metareturn:function(){
		var txt=$("#shop_style").children();
		var arr=[];
		var deTx;
		var descript=$("meta[name=description]");
		for(var i=1;i<txt.length;i++){
			var nTxt=txt.children("a").eq(i).html();
			arr.push(" "+nTxt);
		}
		for(var n=0;n<arr.length;n++){
			deTx += arr[n];
		}
		deTx = deTx.replace(/undefined/,"");
		return deTx;
	},
	meatSetion:function(){
		buySale.setMeat();
		buySale.titleShop();
	},
	shopTitle:function(){
		buySale.titleShop();
		buySale.meatSetion();
	},
	titleShop:function(){
		var indexTx=buySale.metareturn();
		var tle=$("#ownerInfo li .info").html();
		var ntle = tle + " - 桃牛网";
		$("title").html(ntle);
	},
	setMeat:function(){
		var descript=$("meta[name=description]");
		var keyW=$("meta[name=Keywords]");
		var tledes=$("#ownerInfo li .info").html();
		var key;
		var ndeTx;
		var Tx=buySale.metareturn();
		ndeTx ="欢迎来到桃牛网 - " + tledes + "店铺" + Tx;
		key = "桃牛,桃牛网,网上购物,店铺," + Tx;
		descript.attr("content",ndeTx);
		keyW.attr("content",key);
	}

	
};
pCenter={
	pageNow:0,
	searchMusic:function(){
		if(!document.getElementById("search_music")){return;}
		var textBox=$("#pb-audio-search-input");
		var start=$("#search_music");
		if (arguments.length==1){
			loadPage(arguments[0]);
			return;
		}
		start.bind("click",loadPage);		
		function loadPage (){
			pCenter.pageNow=typeof arguments[0] =="object"?pCenter.pageNow=1:arguments[0];
			if(textBox.val().length==0){return;}
			$.getScript('http://kuang.xiami.com/app/nineteen/search/key/'+textBox.val()+'/diandian/1/page/' +pCenter.pageNow+'?callback=pCenter.searchMusicBack',function(){})	
		}
	},
	searchMusicBack:function(data){
		if(data.total<1){return;}
		var ul=$("#song_rst ul");
		ul.empty();
		ul.parent().css("display","block");
		var a="";
		for (var i=0;i<data.results.length;i++){
			a="<li><a class='songs' href='"+ data.results[i].album_logo +"' sid='"+ data.results[i].song_id +"'>"+decodeURI(data.results[i].song_name +'-' +data.results[i].artist_name) +"</a></li>";
			ul.append(a);
		}
		var leftCount=data.total-pCenter.pageNow*8;
		if(pCenter.pageNow!=1){
			ul.append("<li class='funLi'><a  id='prv_page' href='#'>上一页</a></li>")		
		}
		if(leftCount>1){
			ul.append("<li class='funLi'><a id='net_page' href='#'>下一页</a></li>")
		}
		var songs=$(".songs");
		var musicHolder=$("#pb-audio-preview-holder");
		var playerHolder=musicHolder.find("#pb-audio-player");
		var logoHolder=musicHolder.find("#pb-audio-thumb");
		var postTitle=$("#m_title");
		var postPlayer=$("#m_palyer");
		var postImg=$("#m_img");
		songs.bind("click",function(){
			var player="<embed width='257' height='33' wmode='transparent' type='application/x-shockwave-flash' src='http://www.xiami.com/widget/0_"+$(this).attr("sid") +"/singlePlayer.swf'>"
			playerHolder.empty();
			playerHolder.append(player);
			logoHolder.attr("src",$(this).attr("href"));
			musicHolder.css("display","block");
			postTitle.val($(this).html());
			postPlayer.val(player);
			postImg.val($(this).attr("href"));
			$("#song_rst").css("display","none");
			$("#pb-audio-search-input").css("display","none");
			$("#search_music").css("display","none");
			return false;
		});
		var prvBt=$("#prv_page");
		var nextBt=$("#net_page");
		prvBt.bind("click",function(){
			pCenter.searchMusic(pCenter.pageNow-1);
			return false;
		});
		nextBt.bind("click",function(){
			pCenter.searchMusic(pCenter.pageNow+1);
			return false;
		});
		$("#pb-audio-repick-btn").bind("click",function(){
			$(this).parent().css("display","none");
			$("#pb-audio-search-input").css("display","inline");
			$("#search_music").css("display","inline");
			ul.parent().css("display","block");
		});
	},
	getVideo:function(){
		if(!document.getElementById("search_video")){return;}
		var textBox=$("#pb-audio-search-input");
		var start=$("#search_video");
		start.bind("click",loadPage);		
		function loadPage (){
			if(textBox.val().length==0){return;}
			start.html("Loading...");		
			$.ajax({
				type:"POST",
				url:'../server_page/uCenter_pc.aspx?action=getVideo&callBack=pCenter.videoBack',
				data:"uri=" + textBox.val(),				
				success:function(msg){
					eval(msg);
					start.html("确定");
				},error:function(msg){
					alert(msg.status);
					start.html("确定");
				}
			});
			return false;
		}
	},
	videoBack:function(data){
		var musicHolder=$("#pb-audio-preview-holder");
		var logoHolder=musicHolder.find("#pb-audio-thumb");
		var titleHolder=musicHolder.find("#pb-video-title");
		var postTitle=$("#v_title");
		var postPlayer=$("#v_palyer");
		var postImg=$("#v_img");		
		postTitle.val(data.title);
		postPlayer.val(data.player);
		postImg.val(data.img);
		musicHolder.css("display","block");
		logoHolder.attr("src",data.img);
		titleHolder.html(data.title);
    },
	showVideo:function(){
		var showImg=$(".video_hidden").parent().children("img");
		showImg.bind("click",function(){
			$(this).hide();
			$(this).next().addClass("video_show");
			$(this).next().children("embed").before("<strong class='green_hidden'><b></b>收起</strong>");
			
			$(".green_hidden").bind("click",function(){
				var pre=$(this).parent().prev();
				pre.show();
				pre.next().removeClass("video_show");
				$(this).remove().next().stop();
			});
		});
		//-----回到顶部---		
		$(window).scroll(
			setInterval(function backTop(){
				var backtop=$(document).scrollTop();
				var bt=$("#back_top");
				var wH=$(window).height();
				var href=location.href;
				var hsplit=href.split("/");
				var haunt=hsplit[hsplit.length-1];
				var url=haunt.indexOf("default");
				if(url == 0){
					if(backtop!=0){
						if($.browser.version=="6.0"){
							bt.css({"position":"absolute","top":backtop+wH-129});
						}
						bt.fadeIn("slow");
						bt.bind("mouseover",function(){
							$(this).bind("click",function(){
								$(document).scrollTop(0);
							}).attr("title","返回顶部");
						});
					}else{ bt.fadeOut("slow"); }
				}
		},1));
	}
};
mySns={
	submitTest:function(){
		var text=$("#pb-text-textarea");
		var file=$("#pb-photo-flash-holder #File");
		$("#Submit1").bind("click",function(){
			if(text.val()==""){
				alert("内容不能为空！");
				return false;
			}
			if(file.val() == ""){
				alert("请选择要发布的图片！");
				return false;
			}else{
				return userCenter.imgType(file);
			}
		});
	}
};
classified={
	setPostCat:function(){
		var cats=[$("[name='cat1']"),$("[name='cat2']"),$("[name='cat3']")];
		var strEmpty="<option>请选择分类</option>";
		cats[0].html(strEmpty);
		cats[1].html(strEmpty);
		cats[2].html(strEmpty);
		$.ajax({
			type:"get",
			url:"../server_page/classified.aspx?action=getCat&deep=1&father=-1",
			success:function(msg){
				cats[0].append(msg);
			},error:function(e){
				alert(e.status);
			}
		});
		for(var cat in cats){
			cats[cat].bind("change",function(){
				var deep=$(this).attr("deep");
				deep++;
				var father=cats[deep-2].val();
				$.ajax({
					type:"get",
					url:"../server_page/classified.aspx?action=getCat&deep=" +deep+ "&father=" +father,
					success:function(msg){
						cats[deep-1].html(strEmpty+msg);
					},error:function(e){
						alert(e.status);
					}			
				});
			});
		}
	}
};
var cat={
	hoverItem:function(){
		var itemDiv=$(".item");
		itemDiv.hover(function(){
			$(this).addClass("item_bgcolor");
		},function(){
			$(this).removeClass("item_bgcolor");
		});
	},
	postAct:function(){
		var otherInfo=$("#otherInfo").html();
		// var otherInfo="[{'出租方式':'整套出租'}]";
		var infos=$("#infos");
		var str="<table class='postTab'>";
		var json=eval("("+otherInfo+")");   // 转成json数据;
		for(var i=0;i<json.length;i++){
			for(var key in json[i]){
				str +="<tr><td class='postTdone'>"+key+"：</td>";
				str +="<td class='postTdtwo'>"+json[i][key].replace(/\|$/,"")+"</td></tr>";
			}
		}
		str +="</table>";
		infos.after(str);
	},
	postTle:function(){
		var txt=$("#nav li").eq(1).children().html();
		var postkeyW=$("meta[name=Keywords]");
		var itemT=$(".itemTitle");
		var nitem="";
		var num=itemT.length;
		var postDest="";
		$("title").html("柳州"+txt+"|柳州"+txt+"信息 - 柳州桃牛网");
		if(itemT){
			if(num>3){ num=3; }
			for(var i=0;i<num;i++){
				nitem +=" " + itemT.eq(i).children().html();
			}
			nitem=nitem.replace(/undefined/,"");
		}
		postkeyW.attr("content","柳州" + txt + nitem);
		postDest="柳州桃牛网 -- " + txt + "频道免费提供给您大量真实有效的柳州" + txt +"信息,同时您可以免费发布柳州" + txt + "信息。最好的柳州" + txt + "信息就在柳州桃牛网" + txt + "频道。";
		$("meta[name=Description]").attr("content",postDest);
	},
	postInformation:function(){
		var postInfo;
		var postInner=$("#postInner").html();
		var theEmrty=$("#theEmrty h2").html();
		var keyws=theEmrty + " 柳州分类信息 柳州免费发布信息";
		if(postInner.length >= 126){
			postInfo=postInner.substring(0,126);
		}
		$("title").html(theEmrty + " - 柳州桃牛网");
		$("meta[name=Keywords]").attr("content",keyws);
		$("meta[name=Description]").attr("content",postInfo);
	},
	lingshi:function(){
		$(".disHide").bind("click",function(e){	
			$("body").append("<div class='lingshi'></div><div class='lingimg'><span class='lingnow'>页面正在建设中......<b class='x'>X</b></span></div>");
			function ls(){
				var H=$(window).height();
				var h=$("body").height();
				var W=$(window).width();
				var lw=$(".lingimg").width();
				var lh=$(".lingimg").height();
				if(H>h){ h=H; }
				$(".lingimg").css({"left":(W-lw)/2.5,"top":(H-lh)/2.5});
				$(".lingshi").width($(window).width()).height(h);
			}
			ls();
			$(window).resize(function(){ ls(); });
			$(".x").bind("click",function(){
				$(".lingshi").remove();
				$(".lingimg").remove();
			});
		}).bind("contextmenu",function(e){
			return false;
		});
		function oDis(){
			var parentLing=$(".parentLing");
			var nowbody=$(".nowbody");
			var lingBody=$(".lingBody");
			var Ww=$(window).width();
			var Bh=$("body").height();
			var Wh=$(window).height();
			if(Wh>Bh){ Bh=Wh; }
			nowbody.css({"display":"block","height":Bh,"width":Ww});
			parentLing.css("display","block");
			if($.browser.version=="6.0"){
				lingBody.css({"left":Wh-630});
			}
			$(".subFeed").bind("click",function(){
				nowbody.css({"display":"none"});
				parentLing.css("display","none");
				$(window).unbind("resize");
			});
		}
		$(".disHome").bind("click",function(){
			oDis();
			$(".read").bind("click",function(){
				var thisN=$(this).parent().parent();
				if(thisN.hasClass("nob")){
					thisN.removeClass("nob");
					$(this).html("　收 起 ↑");
				}else{
					thisN.addClass("nob");				
					$(this).html("阅读更多↓");
				}
				var lingBody=$(".lingBody").height();
				if(lingBody>$("body").height()){
					$(".nowbody").css("height",lingBody+110);
				}else{
					$(".nowbody").css("height",$("body").height());
				}		
			});
			$(".nowRead").bind("click",function(){
				var nThis=$(this).parent();
				if(nThis.hasClass("nob")){
					nThis.removeClass("nob");
					$(this).children(".read").html("　收 起 ↑");
				}else{
					nThis.addClass("nob");
					$(this).children(".read").html("阅读更多↓");
				}
				var lingBody=$(".lingBody").height();
				if(lingBody>$("body").height()){
					$(".nowbody").css("height",lingBody+110);
				}else{
					$(".nowbody").css("height",$("body").height());
				}
				$(".read").unbind("click");		
			});
			$(window).bind("resize",function(){ oDis(); });
		}).bind("contextmenu",function(e){
			return false;
		});
		$(".disShop").bind("click",function(){
			function shopD(){
				var nowbody=$(".nowbody");
				var shopshi=$(".shopshi");
				var shopeaer=$(".shopeaer");
				var Bh=$("body").height();
				var Ww=$("body").width();
				var Wh=$(window).height();
				nowbody.css({"display":"block","height":Bh,"width":Ww});
				shopshi.css("display","block");
				if($.browser.version=="6.0"){
					shopeaer.css({"left":Wh-630});
				}
				$(".subFeed").bind("click",function(){
					nowbody.css({"display":"none"});
					shopshi.css("display","none");
					$(window).unbind("resize");
				});
			}
			shopD();
			$(window).bind("resize",function(){ shopD(); });
		}).bind("contextmenu",function(e){
			return false;
		});
		$(".disPing").bind("click",function(){
			function pingD(){
				var pingdai=$(".pingdai");
				var nowbody=$(".nowbody");
				var pingNav=$(".pingNav");
				var Bh=$("body").height();
				var Ww=$("body").width();
				var Wh=$(window).height();
				nowbody.css({"display":"block","height":Bh,"width":Ww});
				pingdai.css("display","block");
				if($.browser.version=="6.0"){
					pingNav.css({"left":Wh-630});
				}
				$(".subFeed").bind("click",function(){
					nowbody.css({"display":"none"});
					pingdai.css("display","none");
					$(window).unbind("resize");
				});
			}
			pingD();
			$(window).resize(function(){ pingD(); });
		}).bind("contextmenu",function(e){
			return false;
		});
		$(".disWei").bind("click",function(){
			gd();
		}).bind("contextmenu",function(e){
			return false;
		});
		$(".disLuDang").bind("click",function(){
			gd();
		}).bind("contextmenu",function(e){
			return false;
		});
		function gd(){
			function weiD(){
				var weipo=$(".weipo");
				var nowbody=$(".nowbody");
				var weiNav=$(".weiNav");
				var Bh=$("body").height();
				var Ww=$("body").width();
				var Wh=$(window).height();
				nowbody.css({"display":"block","height":Bh,"width":Ww});
				weipo.css("display","block");
				if($.browser.version=="6.0"){
					weiNav.css({"left":Wh-630});
				}
				$(".subFeed").bind("click",function(){
					nowbody.css({"display":"none"});
					weipo.css("display","none");
					$(window).unbind("resize");
				});
			}
			weiD();
			$(window).bind("resize",function(){ weiD(); });
		}
	}
};













