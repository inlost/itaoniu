Imports System.Web
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Collections.Generic

Namespace AlipayClass
    ''' <summary>
    ''' 类名：alipay_service
    ''' 功能：支付宝外部服务接口控制
    ''' 详细：该页面是请求参数核心处理文件，不需要修改
    ''' 版本：3.1
    ''' 修改日期：2010-11-25
    ''' 说明：
    ''' 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    ''' 该代码仅供学习和研究支付宝接口使用，只是提供一个参考
    ''' </summary>
    Public Class AlipayService
        Private gateway As String = ""
        '网关地址
        Private _key As String = ""
        '交易安全校验码
        Private _input_charset As String = ""
        '编码格式
        Private _sign_type As String = ""
        '签名方式
        Private mysign As String = ""
        '签名结果
        Private sPara As New Dictionary(Of String, String)()
        '要签名的字符串
        ''' <summary>
        ''' 构造函数
        ''' 从配置文件及入口文件中初始化变量
        ''' </summary>
        ''' <param name="partner">合作身份者ID</param>
        ''' <param name="seller_email">签约支付宝账号或卖家支付宝帐户</param>
        ''' <param name="return_url">付完款后跳转的页面 要用 以http开头格式的完整路径，不允许加?id=123这类自定义参数</param>
        ''' <param name="notify_url">交易过程中服务器通知的页面 要用 以http开格式的完整路径，不允许加?id=123这类自定义参数</param>
        ''' <param name="show_url">网站商品的展示地址，不允许加?id=123这类自定义参数</param>
        ''' <param name="out_trade_no">请与贵网站订单系统中的唯一订单号匹配</param>
        ''' <param name="subject">订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。</param>
        ''' <param name="body">订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里</param>
        ''' <param name="price">订单总金额，显示在支付宝收银台里的“商品单价”里</param>
        ''' <param name="logistics_fee">物流费用，即运费。</param>
        ''' <param name="logistics_type">物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）</param>
        ''' <param name="logistics_payment">物流支付方式，三个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）</param>
        ''' <param name="quantity">商品数量，建议默认为1，不改变值，把一次交易看成是一次下订单而非购买一件商品。</param>
        ''' <param name="receive_name">收货人姓名，如：张三</param>
        ''' <param name="receive_address">收货人地址，如：XX省XXX市XXX区XXX路XXX小区XXX栋XXX单元XXX号</param>
        ''' <param name="receive_zip">收货人邮编，如：123456</param>
        ''' <param name="receive_phone">收货人电话号码，如：0571-81234567</param>
        ''' <param name="receive_mobile">收货人手机号码，如：13312341234</param>
        ''' <param name="logistics_fee_1">第二组物流费用，即运费。</param>
        ''' <param name="logistics_type_1">第二组物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）</param>
        ''' <param name="logistics_payment_1">第二组物流支付方式，三个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）</param>
        ''' <param name="logistics_fee_2">第三组物流费用，即运费。</param>
        ''' <param name="logistics_type_2">第三组物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）</param>
        ''' <param name="logistics_payment_2">第三组物流支付方式，三个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）</param>
        ''' <param name="buyer_email">默认买家支付宝账号</param>
        ''' <param name="discount">折扣，是具体的金额，而不是百分比。若要使用打折，请使用负数，并保证小数点最多两位数</param>
        ''' <param name="key">安全检验码</param>
        ''' <param name="input_charset">字符编码格式 目前支持 gbk 或 utf-8</param>
        ''' <param name="sign_type">加密方式 不需修改</param>
        Public Sub New(ByVal partner As String, ByVal seller_email As String, ByVal return_url As String, ByVal notify_url As String, ByVal show_url As String, ByVal out_trade_no As String, _
         ByVal subject As String, ByVal body As String, ByVal price As String, ByVal logistics_fee As String, ByVal logistics_type As String, ByVal logistics_payment As String, _
         ByVal quantity As String, ByVal receive_name As String, ByVal receive_address As String, ByVal receive_zip As String, ByVal receive_phone As String, ByVal receive_mobile As String, _
         ByVal logistics_fee_1 As String, ByVal logistics_type_1 As String, ByVal logistics_payment_1 As String, ByVal logistics_fee_2 As String, ByVal logistics_type_2 As String, ByVal logistics_payment_2 As String, _
         ByVal buyer_email As String, ByVal discount As String, ByVal key As String, ByVal input_charset As String, ByVal sign_type As String)
            gateway = "https://www.alipay.com/cooperate/gateway.do?"
            _key = key.Trim()
            _input_charset = input_charset.ToLower()
            _sign_type = sign_type.ToUpper()
            Dim sParaTemp As New SortedDictionary(Of String, String)()

            '构造加密参数数组，以下顺序请不要更改（由a到z排序）
            sParaTemp.Add("_input_charset", _input_charset)
            sParaTemp.Add("body", body)
            sParaTemp.Add("buyer_email", buyer_email)
            sParaTemp.Add("discount", discount)
            sParaTemp.Add("logistics_fee", logistics_fee)
            sParaTemp.Add("logistics_fee_1", logistics_fee_1)
            sParaTemp.Add("logistics_fee_2", logistics_fee_2)
            sParaTemp.Add("logistics_payment", logistics_payment)
            sParaTemp.Add("logistics_payment_1", logistics_payment_1)
            sParaTemp.Add("logistics_payment_2", logistics_payment_2)
            sParaTemp.Add("logistics_type", logistics_type)
            sParaTemp.Add("logistics_type_1", logistics_type_1)
            sParaTemp.Add("logistics_type_2", logistics_type_2)
            sParaTemp.Add("notify_url", notify_url)
            sParaTemp.Add("out_trade_no", out_trade_no)
            sParaTemp.Add("partner", partner)
            sParaTemp.Add("payment_type", "1")
            sParaTemp.Add("price", price)
            sParaTemp.Add("quantity", quantity)
            sParaTemp.Add("receive_address", receive_address)
            sParaTemp.Add("receive_mobile", receive_mobile)
            sParaTemp.Add("receive_name", receive_name)
            sParaTemp.Add("receive_phone", receive_phone)
            sParaTemp.Add("receive_zip", receive_zip)
            sParaTemp.Add("return_url", return_url)
            sParaTemp.Add("seller_email", seller_email)
            sParaTemp.Add("service", "trade_create_by_buyer")
            sParaTemp.Add("show_url", show_url)
            sParaTemp.Add("subject", subject)
            '构造加密参数数组，以上顺序请不要更改（由a到z排序）

            sPara = AlipayFunction.Para_filter(sParaTemp)
            '获得签名结果
            mysign = AlipayFunction.Build_mysign(sPara, _key, _sign_type, _input_charset)
        End Sub

        ''' <summary>
        ''' 构造表单提交HTML
        ''' </summary>
        ''' <returns>输出 表单提交HTML文本</returns>
        Public Function Build_Form() As String
            Dim sbHtml As String

            'GET方式传递
            sbHtml = "<form id='alipaysubmit' name='alipaysubmit' action='" & gateway & "_input_charset=" & _input_charset & "' method='get'>"

            'POST方式传递（GET与POST二必选一）
            'sbHtml.Append("<form id=\"alipaysubmit\" name=\"alipaysubmit\" action=\"" + gateway + "_input_charset=" + _input_charset + "\" method=\"post\">");

            For Each temp As KeyValuePair(Of String, String) In sPara
                sbHtml += "<input type='hidden' name='" & temp.Key & "' value='" & temp.Value & "'/>"
            Next

            sbHtml += "<input type='hidden' name='sign' value='" & mysign & "'/>"
            sbHtml += "<input type='hidden' name='sign_type' value='" & _sign_type & "'/>"

            'submit按钮控件请不要含有name属性
            sbHtml += "<input type='submit' value='支付宝确认付款'></form>"

            sbHtml += "<script>document.forms['alipaysubmit'].submit();</script>"

            Return sbHtml
        End Function
    End Class
  Public Class AlipayService2
        Private gateway As String = ""
        '网关地址
        Private _key As String = ""
        '交易安全校验码
        Private _input_charset As String = ""
        '编码格式
        Private _sign_type As String = ""
        '签名方式
        Private mysign As String = ""
        '签名结果
        Private sPara As New Dictionary(Of String, String)()
        '要签名的字符串
        ''' <summary>
        ''' 构造函数
        ''' 从配置文件及入口文件中初始化变量
        ''' </summary>
        ''' <param name="partner">合作身份者ID</param>
        ''' <param name="trade_no">支付宝交易号。它是登陆支付宝网站在交易管理中查询得到，一般以8位日期开头的纯数字（如：20100419XXXXXXXXXX） </param>
        ''' <param name="logistics_name">物流公司名称</param>
        ''' <param name="invoice_no">物流发货单号</param>
        ''' <param name="transport_type">物流发货时的运输类型，三个值可选：POST（平邮）、EXPRESS（快递）、EMS（EMS）</param>
        ''' <param name="seller_ip">卖家本地电脑IP地址</param>
        ''' <param name="key">安全检验码</param>
        ''' <param name="input_charset">字符编码格式 目前支持 gbk 或 utf-8</param>
        ''' <param name="sign_type">加密方式 不需修改</param>
        Public Sub New(ByVal partner As String, ByVal trade_no As String, ByVal logistics_name As String, ByVal invoice_no As String, ByVal transport_type As String, ByVal seller_ip As String, _
         ByVal key As String, ByVal input_charset As String, ByVal sign_type As String)
            gateway = "https://www.alipay.com/cooperate/gateway.do?"
            _key = key.Trim()
            _input_charset = input_charset.ToLower()
            _sign_type = sign_type.ToUpper()
            Dim sParaTemp As New SortedDictionary(Of String, String)()

            '构造签名参数数组
            sParaTemp.Add("_input_charset", _input_charset)
            sParaTemp.Add("invoice_no", invoice_no)
            sParaTemp.Add("logistics_name", logistics_name)
            sParaTemp.Add("partner", partner)
            sParaTemp.Add("seller_ip", seller_ip)
            sParaTemp.Add("service", "send_goods_confirm_by_platform")
            sParaTemp.Add("trade_no", trade_no)
            sParaTemp.Add("transport_type", transport_type)

            sPara = AlipayFunction.Para_filter(sParaTemp)
            '获得签名结果
            mysign = AlipayFunction.Build_mysign(sPara, _key, _sign_type, _input_charset)
        End Sub

        ''' <summary>
        ''' 构造表单提交HTML
        ''' </summary>
        ''' <returns>输出 表单提交HTML文本</returns>
        Public Function Build_Form() As String
            Dim sbHtml As New StringBuilder()

            'GET方式传递
            'sbHtml.Append("<form id=\"alipaysubmit\" name=\"alipaysubmit\" action=\"" + gateway + "_input_charset=" + _input_charset + "\" method=\"get\">");

            'POST方式传递（GET与POST二必选一）
            sbHtml.Append("<form id=""alipaysubmit"" name=""alipaysubmit"" action=""" & gateway & "_input_charset=" & _input_charset & """ method=""post"">")

            For Each temp As KeyValuePair(Of String, String) In sPara
                sbHtml.Append(("<input type=""hidden"" name=""" + temp.Key & """ value=""") + temp.Value & """/>")
            Next

            sbHtml.Append("<input type=""hidden"" name=""sign"" value=""" & mysign & """/>")
            sbHtml.Append("<input type=""hidden"" name=""sign_type"" value=""" & _sign_type & """/>")

            'submit按钮控件请不要含有name属性
            sbHtml.Append("<input type=""submit"" value=""支付宝确认发货""></form>")

            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>")

            Return sbHtml.ToString()
        End Function

        ''' <summary>
        ''' 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        ''' </summary>
        ''' <param name="sArray">需要拼接的数组</param>
        ''' <returns>拼接完成以后的字符串</returns>
        Public Function Create_linkstring_urlencode(ByVal dicArray As Dictionary(Of String, String)) As String
            Dim prestr As New StringBuilder()
            For Each temp As KeyValuePair(Of String, String) In dicArray
                prestr.Append(temp.Key + "=" & HttpUtility.UrlEncode(temp.Value) & "&")
            Next
            Return prestr.ToString()
        End Function

        ''' <summary>
        ''' 构造请求URL
        ''' </summary>
        ''' <returns>请求url</returns>
        Public Function Create_url() As String
            Dim strUrl As String = gateway
            Dim arg As String = Create_linkstring_urlencode(sPara)
            '把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            strUrl = strUrl & arg & "sign=" & mysign & "&sign_type=" & _sign_type
            Return strUrl
        End Function
    End Class
End Namespace

