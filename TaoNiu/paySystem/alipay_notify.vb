Imports System.Web
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Collections.Generic

Namespace AlipayClass
    ''' <summary>
    ''' 类名：alipay_notify
    ''' 功能：付款过程中服务器通知类
    ''' 详细：该页面是通知返回核心处理文件，不需要修改
    ''' 版本：3.1
    ''' 修改日期：2010-10-29
    ''' '说明：
    ''' 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    ''' 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    ''' 
    ''' //////////////////////注意/////////////////////////////
    ''' 调试通知返回时，可查看或改写log日志的写入TXT里的数据，来检查通知返回是否正常 
    ''' </summary>
    Public Class AlipayNotify
        Private gateway As String = ""
        '网关地址
        Private _transport As String = ""
        '访问模式
        Private _partner As String = ""
        '合作身份者ID
        Private _key As String = ""
        '编码格式
        Private _input_charset As String = ""
        '编码格式
        Private _sign_type As String = ""
        '签名方式
        Private m_mysign As String = ""
        '签名结果
        Private m_responseTxt As String = ""
        '服务器ATN结果
        Private sPara As New Dictionary(Of String, String)()
        '要签名的参数组
        Private m_preSignStr As String = ""
        '待签名的字符串
        ''' <summary>
        ''' 获取通知返回后计算后（验证）的签名结果
        ''' </summary>
        Public ReadOnly Property Mysign() As String
            Get
                Return m_mysign
            End Get
        End Property

        ''' <summary>
        ''' 获取验证是否是支付宝服务器发来的请求结果
        ''' </summary>
        Public ReadOnly Property ResponseTxt() As String
            Get
                Return m_responseTxt
            End Get
        End Property

        ''' <summary>
        ''' 获取待签名的字符串（调试用）
        ''' </summary>
        Public ReadOnly Property PreSignStr() As String
            Get
                Return m_preSignStr
            End Get
        End Property

        ''' <summary>
        ''' 构造函数
        ''' 从配置文件中初始化变量
        ''' </summary>
        ''' <param name="inputPara">通知返回来的参数数组</param>
        ''' <param name="notify_id">验证通知ID</param>
        ''' <param name="partner">合作身份者ID</param>
        ''' <param name="key">安全校验码</param>
        ''' <param name="input_charset">编码格式</param>
        ''' <param name="sign_type">签名类型</param>
        ''' <param name="transport">访问模式</param>
        Public Sub New(ByVal inputPara As SortedDictionary(Of String, String), ByVal notify_id As String, ByVal partner As String, ByVal key As String, ByVal input_charset As String, ByVal sign_type As String, _
         ByVal transport As String)
            _transport = transport
            If _transport = "https" Then
                gateway = "https://www.alipay.com/cooperate/gateway.do?"
            Else
                gateway = "http://notify.alipay.com/trade/notify_query.do?"
            End If

            _partner = partner.Trim()
            _key = key.Trim()
            _input_charset = input_charset
            _sign_type = sign_type.ToUpper()

            sPara = AlipayFunction.Para_filter(inputPara)
            '过滤空值、sign与sign_type参数
            m_preSignStr = AlipayFunction.Create_linkstring(sPara)
            '获取待签名字符串（调试用）
            '获得签名结果
            m_mysign = AlipayFunction.Build_mysign(sPara, _key, _sign_type, _input_charset)

            '获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            m_responseTxt = Verify(notify_id)
        End Sub

        ''' <summary>
        ''' 验证是否是支付宝服务器发来的请求
        ''' </summary>
        ''' <returns>验证结果</returns>
        Private Function Verify(ByVal notify_id As String) As String
            Dim veryfy_url As String = ""
            If _transport = "https" Then
                veryfy_url = gateway & "service=notify_verify&partner=" & _partner & "&notify_id=" & notify_id
            Else
                veryfy_url = gateway & "partner=" & _partner & "&notify_id=" & notify_id
            End If

            Return Get_Http(veryfy_url, 120000)
        End Function

        ''' <summary>
        ''' 获取远程服务器ATN结果
        ''' </summary>
        ''' <param name="strUrl">指定URL路径地址</param>
        ''' <param name="timeout">超时时间设置</param>
        ''' <returns>服务器ATN结果</returns>
        Private Function Get_Http(ByVal strUrl As String, ByVal timeout As Integer) As String
            Dim strResult As String
            Try
                Dim myReq As HttpWebRequest = DirectCast(HttpWebRequest.Create(strUrl), HttpWebRequest)
                myReq.Timeout = timeout
                Dim HttpWResp As HttpWebResponse = DirectCast(myReq.GetResponse(), HttpWebResponse)
                Dim myStream As Stream = HttpWResp.GetResponseStream()
                Dim sr As New StreamReader(myStream, Encoding.[Default])
                Dim strBuilder As New StringBuilder()
                While -1 <> sr.Peek()
                    strBuilder.Append(sr.ReadLine())
                End While

                strResult = strBuilder.ToString()
            Catch exp As Exception

                strResult = "错误：" & exp.Message
            End Try

            Return strResult
        End Function
    End Class
End Namespace

