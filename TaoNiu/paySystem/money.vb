Namespace tnPay
    Public Class money
        Public Function getMoney(ByVal uid As Integer) As Hashtable
            Dim theMoney As New Hashtable
            Dim myHelper As New dbHelper
            Dim strSql As String = "select * from money where uid=" & uid
            Dim myDs As New DataSet, myType As New typeHelper
            myDs = myHelper.getQuerySql(strSql, "money")
            theMoney = myType.dsToHashSingle(myDs, "money")
            Return theMoney
        End Function
        Public Function addWaitMoney(ByVal uid As Integer, ByVal money As Double, ByVal log As String) As Boolean
            Dim theMoney As New Hashtable, myHelper As New dbHelper
            theMoney = getMoney(uid)
            If theMoney("status") = "noData" Then
                Dim myMoney As New Hashtable
                myMoney("@uid") = uid
                myMoney("@money") = 0
                myMoney("@wait_money") = money
                Dim myLog As New Hashtable
                myLog("@uid") = uid
                myLog("@type") = 0
                myLog("@money") = money
                myLog("@remark") = log
                Try
                    myHelper.noneQuery("money_new", myHelper.hashtableToInquery(myMoney))
                    myHelper.noneQuery("money_log_add", myHelper.hashtableToInquery(myLog))
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            Else
                Dim waitMoney As Double = theMoney("wait_money") + money
                Dim strSql As String = "update money set wait_money=" & waitMoney & " where uid=" & uid
                Try
                    myHelper.querySql(strSql)
                    Dim myLog As New Hashtable
                    myLog("@uid") = uid
                    myLog("@type") = 0
                    myLog("@money") = waitMoney
                    myLog("@remark") = log
                    myHelper.noneQuery("money_log_add", myHelper.hashtableToInquery(myLog))
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End If
        End Function
        Public Function moneyLogGet(ByVal uid As Integer, ByVal page As Integer, ByVal prePage As Integer) As Hashtable()
            Dim mGet As New Hashtable
            mGet("@uid") = uid
            mGet("@page") = page
            mGet("@prePage") = prePage
            Dim myDs As New DataSet, myHelper As New dbHelper, myType As New typeHelper
            myDs = myHelper.getQuery("money_log_get", myHelper.hashtableToInquery(mGet), "logs")
            Return myType.dsToHash(myDs, "logs")
        End Function
        Public Function moneyLogCount(ByVal uid As Integer) As Integer
            Dim strSql As String = "select count(*) form money_log where uid=" & uid
            Dim myDs As New DataSet, myHelper As New dbHelper
            Try
                myDs = myHelper.getQuerySql(strSql, "ct")
                Return myDs.Tables("ct").Rows(0)(0)
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Public Function actWaitMoney(ByVal uid As Integer, ByVal money As Double) As Boolean
            Dim theMoney As New Hashtable, myHelper As New dbHelper
            theMoney = getMoney(uid)
            If theMoney("status") = "noData" Then
                Return False
            Else
                Dim waitMoney As Double = theMoney("wait_money") - money
                Dim actMoney As Double = theMoney("money") + money
                Dim strSql As String = "update money set wait_money=" & waitMoney & ",money=" & actMoney & " where uid=" & uid
                Try
                    myHelper.querySql(strSql)
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End If
        End Function
        Private Function moveMoney(ByVal uid As Integer, ByVal money As Double) As Boolean
            Dim theMoney As New Hashtable, myHelper As New dbHelper
            theMoney = getMoney(uid)
            If theMoney("status") = "noData" Then
                Return False
            Else
                Dim actMoney As Double = theMoney("money") - money
                Dim strSql As String = "update money set money=" & actMoney & " where uid=" & uid
                Try
                    myHelper.querySql(strSql)
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End If
        End Function
        Public Function addMoveMoney(ByVal uid As Integer, ByVal money As Integer, ByVal payId As String) As Boolean
            Dim myLog As New Hashtable, myHelper As New dbHelper
            myLog("@uid") = uid
            myLog("@type") = 1
            myLog("@money") = money
            myLog("@remark") = payId
            Try
                moveMoney(uid, money + money * 0.012)
                myHelper.noneQuery("money_log_add", myHelper.hashtableToInquery(myLog))
                moneyLogAdd(uid, money, "提现" & money & "元，手续费" & money * 0.012 & "元")
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function waitSendMoney(ByVal uid As Integer) As Integer
            Dim strSql As String = "select sum(money) from money_log where type=1 and uid=" & uid
            Dim myHelper As New dbHelper, myDs As New DataSet
            Try
                myDs = myHelper.getQuerySql(strSql, "mn")
                Return myDs.Tables("mn").Rows(0)(0)
            Catch ex As Exception
                Return 0
            End Try
        End Function
        Public Function moneyLogAdd(ByVal uid As Integer, ByVal money As Integer, ByVal log As String) As Boolean
            Try
                Dim myLog As New Hashtable, myHelper As New dbHelper
                myLog("@uid") = uid
                myLog("@type") = 3
                myLog("@money") = money
                myLog("@remark") = log
                Try
                    myHelper.noneQuery("money_log_add", myHelper.hashtableToInquery(myLog))
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function payAdd(ByVal uid As Integer, ByVal money As Integer) As String
            Dim payCon As New AlipayClass.AlipayConfig
            Dim partner As String = payCon.Partner
            Dim key As String = payCon.Key
            Dim seller_email As String = payCon.Seller_email
            Dim input_charset As String = payCon.Input_charset
            Dim notify_url As String = payCon.Notify_url_money
            Dim return_url As String = payCon.Return_url_money
            Dim show_url As String = payCon.Show_url & "/pay-" & money
            Dim sign_type As String = payCon.Sign_type
            Dim out_trade_no As String = DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Date & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & uid   '订单编号
            Dim subject As String = "桃牛充值"  '订单名称
            Dim body As String = uid   '订单描述
            Dim price As String = money   '订单价格
            Dim logistics_fee As String = "0.00" '物流费用
            Dim logistics_type As String = "EXPRESS" '物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）
            Dim logistics_payment As String = "SELLER_PAY" '物流支付方式，两个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）
            Dim quantity As String = 1  '商品数量
            Dim receive_name As String = "桃牛网"  '收货人姓名
            Dim receive_address As String = "柳州市城中区桃牛网络技术有限责任公司"  '收货人地址
            Dim receive_zip As String = "545006"  '收货人邮编
            Dim receive_phone As String = "0571-81234567"
            Dim receive_mobile As String = "13977266658"  '收货人电话
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
    End Class
End Namespace

