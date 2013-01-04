Imports TaoNiu.AlipayClass
Public Class return_money
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sArrary As SortedDictionary(Of String, String) = GetRequestGet()
        Dim con As New AlipayConfig()
        Dim partner As String = con.Partner
        Dim key As String = con.Key
        Dim input_charset As String = con.Input_charset
        Dim sign_type As String = con.Sign_type
        Dim transport As String = con.Transport
        If sArrary.Count > 0 Then
            '判断是否有带返回参数
            Dim aliNotify As New AlipayNotify(sArrary, Request.QueryString("notify_id"), partner, key, input_charset, sign_type, _
             transport)
            Dim responseTxt As String = aliNotify.ResponseTxt
            '获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            Dim sign As String = Request.QueryString("sign")
            '获取支付宝反馈回来的sign结果
            Dim mysign As String = aliNotify.Mysign
            '获取通知返回后计算后（验证）的签名结果
            '判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
            'responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            'mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            If responseTxt = "true" AndAlso sign = mysign Then
                '验证成功
                '请在这里加上商户的业务逻辑程序代码
                Dim order As New Hashtable
                '——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                '获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                order("alipayId") = Request.QueryString("trade_no")
                '支付宝交易号
                order("id") = Request.QueryString("out_trade_no")
                '获取订单号
                order("fee") = Request.QueryString("price")
                '获取总金额
                order("title") = Request.QueryString("subject")
                '商品名称、订单名称
                Dim body As String = Request.QueryString("body")
                '商品描述、订单备注、描述
                order("payId") = Request.QueryString("buyer_email")
                '买家支付宝账号
                order("payName") = Request.QueryString("receive_name")
                '收货人姓名
                order("payAddress") = Request.QueryString("receive_address")
                '收货人地址
                order("payZip") = Request.QueryString("receive_zip")
                '收货人邮编
                order("payPhone") = Request.QueryString("receive_phone")
                '收货人电话
                order("payMobile") = Request.QueryString("receive_mobile")
                '收货人手机
                order("payStatus") = Request.QueryString("trade_status")
                '交易状态
                '判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                '如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                '如果有做过处理，不执行商户的业务程序
                If Request.QueryString("trade_status") = "WAIT_SELLER_SEND_GOODS" Then
                    Response.Redirect("../user_center_money.aspx?at=money")
                ElseIf Request.QueryString("trade_status") = "TRADE_FINISHED" Then
                    Dim myMoney As New tnPay.money
                    myMoney.addWaitMoney(body, order("fee"), "充值" & order("fee") & "元")
                    myMoney.actWaitMoney(body, order("fee"))
                    Response.Redirect("../user_center_money.aspx?at=money&msg=ok")
                Else
                    Response.Write("trade_status=" + Request.QueryString("trade_status"))
                    '——请根据您的业务逻辑来编写程序（以上代码仅作参考）——
                    Response.Redirect("../user_center_orders.aspx?at=orders")
                End If
            Else
                '验证失败
                Response.Redirect("../user_center_orders.aspx?at=orders")
            End If
        Else
            Response.Redirect("../user_center_myOrders.aspx?at=myorders")
        End If
    End Sub
    Public Function GetRequestGet() As SortedDictionary(Of String, String)
        Dim i As Integer = 0
        Dim sArray As New SortedDictionary(Of String, String)()
        Dim coll As NameValueCollection
        'Load Form variables into NameValueCollection variable.
        coll = Request.QueryString

        ' Get names of all forms into a string array.
        Dim requestItem As [String]() = coll.AllKeys

        For i = 0 To requestItem.Length - 1
            sArray.Add(requestItem(i), Request.QueryString(requestItem(i)))
        Next

        Return sArray
    End Function
End Class