Imports TaoNiu.AlipayClass
Public Class notify
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sArrary As SortedDictionary(Of String, String) = GetRequestPost()
        Dim con As New AlipayConfig()
        Dim partner As String = con.Partner
        Dim key As String = con.Key
        Dim input_charset As String = con.Input_charset
        Dim sign_type As String = con.Sign_type
        Dim transport As String = con.Transport

        If sArrary.Count > 0 Then
            '判断是否有带返回参数
            Dim aliNotify As New AlipayNotify(sArrary, Request.Form("notify_id"), partner, key, input_charset, sign_type, _
             transport)
            Dim responseTxt As String = aliNotify.ResponseTxt
            '获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            Dim sign As String = Request.Form("sign")
            '获取支付宝反馈回来的sign结果
            Dim mysign As String = aliNotify.Mysign
            '获取通知返回后计算后（验证）的签名结果
            '写日志记录（若要调试，请取消下面两行注释）
            Dim sWord As String = (("responseTxt=" & responseTxt & vbLf & " notify_url_log:sign=") + Request.Form("sign") & "&mysign=" & mysign & vbLf & " notify回来的参数：") + aliNotify.PreSignStr
            AlipayFunction.log_result(Server.MapPath("log/" & DateTime.Now.ToString().Replace(":", "")) & ".txt", sWord)
            Dim myOrder As New orders, theOrder As New Hashtable
            '判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
            'responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            'mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            If responseTxt = "true" AndAlso sign = mysign Then
                '验证成功
                '请在这里加上商户的业务逻辑程序代码

                '——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                '获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表
                Dim trade_no As String = Request.Form("trade_no")
                '支付宝交易号
                Dim order_no As String = Request.Form("out_trade_no")
                theOrder = myOrder.order_get_byId(order_no)
                '获取订单号
                Dim total_fee As String = Request.Form("price")
                '获取总金额
                Dim subject As String = Request.Form("subject")
                '商品名称、订单名称
                Dim body As String = Request.Form("body")
                '商品描述、订单备注、描述
                Dim buyer_email As String = Request.Form("buyer_email")
                '买家支付宝账号
                Dim trade_status As String = Request.Form("trade_status")
                '交易状态
                If Request.Form("trade_status") = "WAIT_BUYER_PAY" Then
                    '该判断表示买家已在支付宝交易管理中产生了交易记录，但没有付款
                    '判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                    '如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    '如果有做过处理，不执行商户的业务程序
                    myOrder.orderStatusChange(theOrder("id"), 0, 1)
                    '请不要修改或删除
                    Response.Write("success")
                ElseIf Request.Form("trade_status") = "WAIT_SELLER_SEND_GOODS" Then
                    '该判断示买家已在支付宝交易管理中产生了交易记录且付款成功，但卖家没有发货
                    '判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                    '如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    '如果有做过处理，不执行商户的业务程序
                    myOrder.orderStatusChange(theOrder("id"), 0, 2)
                    '请不要修改或删除
                    Response.Write("success")
                ElseIf Request.Form("trade_status") = "WAIT_BUYER_CONFIRM_GOODS" Then
                    '该判断表示卖家已经发了货，但买家还没有做确认收货的操作
                    '判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                    '如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    '如果有做过处理，不执行商户的业务程序
                    myOrder.orderStatusChange(theOrder("id"), 1, 3)
                    '请不要修改或删除
                    Response.Write("success")
                ElseIf Request.Form("trade_status") = "TRADE_FINISHED" Then
                    '该判断表示买家已经确认收货，这笔交易完成
                    '判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                    '如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    '如果有做过处理，不执行商户的业务程序
                    Dim myMoney As New TaoNiu.tnPay.money
                    If theOrder("orderType") = 0 Then
                        myMoney.actWaitMoney(theOrder("ownerId"), total_fee - total_fee * 0.012)
                    Else
                        myMoney.actWaitMoney(theOrder("ownerId"), total_fee - total_fee * 0.022)
                    End If
                    myOrder.orderStatusChange(theOrder("id"), 2, 4)
                    myOrder.order_ok(theOrder("customer"), theOrder("id"), theOrder("good"), 1, "这是一笔由支付宝担保支付的交易")
                    '请不要修改或删除
                    Response.Write("success")
                Else
                    '其他状态判断。普通即时到帐中，其他状态不用判断，直接打印success。
                    Response.Write("success")
                End If
            Else
                '验证失败
                Response.Write("fail")
            End If
        Else
            Response.Write("无通知参数")
        End If
    End Sub
    ''' <summary>
    ''' 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
    ''' </summary>
    ''' <returns>request回来的信息组成的数组</returns>
    Public Function GetRequestPost() As SortedDictionary(Of String, String)
        Dim i As Integer = 0
        Dim sArray As New SortedDictionary(Of String, String)()
        Dim coll As NameValueCollection
        'Load Form variables into NameValueCollection variable.
        coll = Request.Form

        ' Get names of all forms into a string array.
        Dim requestItem As [String]() = coll.AllKeys

        For i = 0 To requestItem.Length - 1
            sArray.Add(requestItem(i), Request.Form(requestItem(i)))
        Next

        Return sArray
    End Function
End Class