Imports System.Net
Imports System.Xml

Namespace tnPay
    Public Class orde
        Public Function add(ByVal order As Hashtable) As String
            Dim payCon As New AlipayClass.AlipayConfig
            Dim partner As String = payCon.Partner
            Dim key As String = payCon.Key
            Dim seller_email As String = payCon.Seller_email
            Dim input_charset As String = payCon.Input_charset
            Dim notify_url As String = payCon.Notify_url
            Dim return_url As String = payCon.Return_url
            Dim show_url As String = payCon.Show_url & "/" & order("gid")
            Dim sign_type As String = payCon.Sign_type
            Dim out_trade_no As String = order("id") '订单编号
            Dim subject As String = order("title")  '订单名称
            Dim body As String = order("body")  '订单描述
            Dim price As String = order("price")  '订单价格
            Dim logistics_fee As String = "0.00" '物流费用
            Dim logistics_type As String = "EXPRESS" '物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）
            Dim logistics_payment As String = "SELLER_PAY" '物流支付方式，两个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）
            Dim quantity As String = order("number")  '商品数量
            Dim receive_name As String = order("name")  '收货人姓名
            Dim receive_address As String = order("address")  '收货人地址
            Dim receive_zip As String = order("zip")  '收货人邮编
            Dim receive_phone As String = "0571-81234567"
            Dim receive_mobile As String = order("phone")  '收货人电话
            Dim logistics_fee_1 As String = ""  '物流费用，即运费。
            Dim logistics_type_1 As String = ""  '物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）
            Dim logistics_payment_1 As String = ""  '物流支付方式，两个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）
            Dim logistics_fee_2 As String = ""
            Dim logistics_type_2 As String = ""
            Dim logistics_payment_2 As String = ""
            Dim buyer_email As String = ""  '默认买家支付宝账号
            Dim discount As String = ""  '折扣，是具体的金额，而不是百分比。若要使用打折，请使用负数，并保证小数点最多两位数
            Dim payService As New AlipayClass.AlipayService(partner,
            seller_email,
            return_url,
            notify_url,
            show_url,
            out_trade_no,
            subject,
            body,
            price,
            logistics_fee,
            logistics_type,
            logistics_payment,
            quantity,
            receive_name,
            receive_address,
            receive_zip,
            receive_phone,
            receive_mobile,
            logistics_fee_1,
            logistics_type_1,
            logistics_payment_1,
            logistics_fee_2,
            logistics_type_2,
            logistics_payment_2,
            buyer_email,
            discount,
            key,
            input_charset,
            sign_type)
            Dim strRt As String = payService.Build_Form
            Return strRt
        End Function
        Public Function changeToSended(ByVal payId As String) As String
            Dim con As New AlipayClass.AlipayConfig
            Dim partner As String = con.Partner
            Dim key As String = con.Key
            Dim input_charset As String = con.Input_charset
            Dim sign_type As String = con.Sign_type
            '支付宝交易号。它是登陆支付宝网站在交易管理中查询得到，一般以8位日期开头的纯数字（如：20100419XXXXXXXXXX） 
            Dim trade_no As String = payId
            '物流公司名称
            Dim logistics_name As String = "taoniu"
            '物流发货单号
            Dim invoice_no As String = "10000000"
            '物流发货时的运输类型，三个值可选：POST（平邮）、EXPRESS（快递）、EMS（EMS）
            Dim transport_type As String = "EXPRESS"
            Dim seller_ip = "127.0.0.1"
            Dim aliService As New AlipayClass.AlipayService2(partner, trade_no, logistics_name, invoice_no, transport_type, seller_ip, _
             key, input_charset, sign_type)
            Dim sHtmlText As String = aliService.Build_Form

            Dim url As String = aliService.Create_url()
            Dim Reader As New XmlTextReader(url)
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(Reader)

            '解析XML，获取XML返回的数据，如：请求处理是否成功、商家网站唯一订单号、支付宝交易号、发货时间等
            Dim nodeIs_success As String = xmlDoc.SelectSingleNode("/alipay/is_success").InnerText
            Dim nodeOut_trade_no As String = ""
            Dim nodeTrade_no As String = ""
            Dim nodeTrade_status As String = ""
            Dim nodeLast_modified_time As String = ""
            Dim nodeError As String = ""

            If nodeIs_success = "T" Then
                nodeOut_trade_no = xmlDoc.SelectSingleNode("/alipay/response/tradeBase/out_trade_no").InnerText
                nodeTrade_no = xmlDoc.SelectSingleNode("/alipay/request").ChildNodes(2).InnerText
                nodeTrade_status = xmlDoc.SelectSingleNode("/alipay/response/tradeBase/trade_status").InnerText
                nodeLast_modified_time = xmlDoc.SelectSingleNode("/alipay/response/tradeBase/last_modified_time").InnerText
            Else
                nodeError = xmlDoc.SelectSingleNode("/alipay/error").InnerText
            End If

            Return sHtmlText
        End Function
    End Class
End Namespace

