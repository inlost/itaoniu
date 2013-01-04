Imports System.Web
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO
Imports System.Net
Imports System.Collections.Generic
Imports System.Xml

Namespace AlipayClass
    ''' <summary>
    ''' 功能：支付宝接口公用函数类
    ''' 详细：该类是请求、通知返回两个文件所调用的公用函数核心处理文件，不需要修改
    ''' 版本：3.1
    ''' 修改日期：2010-10-29
    ''' 说明：
    ''' 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    ''' 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    ''' </summary>
    Public Class AlipayFunction
        Public Sub New()
        End Sub

        ''' <summary>
        ''' 生成签名结果
        ''' </summary>
        ''' <param name="sArray">要签名的数组</param>
        ''' <param name="key">安全校验码</param>
        ''' <param name="sign_type">签名类型</param>
        ''' <param name="_input_charset">编码格式</param>
        ''' <returns>签名结果字符串</returns>
        Public Shared Function Build_mysign(ByVal dicArray As Dictionary(Of String, String), ByVal key As String, ByVal sign_type As String, ByVal _input_charset As String) As String
            Dim prestr As String = Create_linkstring(dicArray)
            '把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            '去掉最後一個&字符
            Dim nLen As Integer = prestr.Length
            prestr = prestr.Substring(0, nLen - 1)

            prestr = prestr & key
            '把拼接后的字符串再与安全校验码直接连接起来
            Dim mysign As String = Sign(prestr, sign_type, _input_charset)
            '把最终的字符串签名，获得签名结果
            Return mysign
        End Function

        ''' <summary>
        ''' 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        ''' </summary>
        ''' <param name="sArray">需要拼接的数组</param>
        ''' <returns>拼接完成以后的字符串</returns>
        Public Shared Function Create_linkstring(ByVal dicArray As Dictionary(Of String, String)) As String
            Dim prestr As New StringBuilder()
            For Each temp As KeyValuePair(Of String, String) In dicArray
                prestr.Append(temp.Key & "=" & temp.Value & "&")
            Next
            Return prestr.ToString()
        End Function

        ''' <summary>
        ''' 除去数组中的空值和签名参数并以字母a到z的顺序排序
        ''' </summary>
        ''' <param name="dicArrayPre">过滤前的参数组</param>
        ''' <returns>过滤后的参数组</returns>
        Public Shared Function Para_filter(ByVal dicArrayPre As SortedDictionary(Of String, String)) As Dictionary(Of String, String)
            Dim dicArray As New Dictionary(Of String, String)()
            For Each temp As KeyValuePair(Of String, String) In dicArrayPre
                If temp.Key.ToLower() <> "sign" AndAlso temp.Key.ToLower() <> "sign_type" AndAlso temp.Value <> "" Then
                    dicArray.Add(temp.Key.ToLower(), temp.Value)
                End If
            Next

            Return dicArray
        End Function

        ''' <summary>
        ''' 签名字符串
        ''' </summary>
        ''' <param name="prestr">需要签名的字符串</param>
        ''' <param name="sign_type">签名类型</param>
        ''' <param name="_input_charset">编码格式</param>
        ''' <returns>签名结果</returns>
        Public Shared Function Sign(ByVal prestr As String, ByVal sign_type As String, ByVal _input_charset As String) As String
            Dim sb As New StringBuilder(32)
            If sign_type.ToUpper() = "MD5" Then
                Dim md5 As MD5 = New MD5CryptoServiceProvider()
                Dim t As Byte() = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(prestr))
                For i As Integer = 0 To t.Length - 1
                    sb.Append(t(i).ToString("x").PadLeft(2, "0"c))
                Next
            End If
            Return sb.ToString()
        End Function

        ''' <summary>
        ''' 写日志，方便测试（看网站需求，也可以改成把记录存入数据库）
        ''' </summary>
        ''' <param name="sPath">日志的本地绝对路径</param>
        ''' <param name="sWord">要写入日志里的文本内容</param>
        Public Shared Sub log_result(ByVal sPath As String, ByVal sWord As String)
            Dim fs As New StreamWriter(sPath, False, System.Text.Encoding.[Default])
            fs.Write(sWord)
            fs.Close()
        End Sub

        ''' <summary>
        ''' 用于防钓鱼，调用接口query_timestamp来获取时间戳的处理函数
        ''' 注意：远程解析XML出错，与IIS服务器配置有关
        ''' </summary>
        ''' <param name="partner">合作身份者ID</param>
        ''' <returns>时间戳字符串</returns>
        Public Shared Function Query_timestamp(ByVal partner As String) As String
            Dim url As String = "https://mapi.alipay.com/gateway.do?service=query_timestamp&partner=" & partner
            Dim encrypt_key As String = ""

            Dim Reader As New XmlTextReader(url)
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(Reader)

            encrypt_key = xmlDoc.SelectSingleNode("/alipay/response/timestamp/encrypt_key").InnerText

            Return encrypt_key
        End Function
    End Class
End Namespace
