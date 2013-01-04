Public Class money
    Inherits System.Web.UI.Page
    Dim userInfo As New Hashtable, myUser As New user
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Len(Trim(Session("id"))) = 0 Then Exit Sub
        userInfo = myUser.getUserInfo(Trim(Session("id")))
        Select Case Request.Params("action")
            Case "moveMoney"
                If moveMoney() Then
                    Response.Redirect("../user_center_money.aspx?at=money&msg=ok")
                Else
                    Response.Redirect("../user_center_money.aspx?at=money&msg=error")
                End If
            Case "addNewMoney"
                addNewMoney()
            Case Else
                Response.Write("Good Luck~")
        End Select
    End Sub
    Private Function moveMoney() As Boolean
        Try
            Dim money As Integer = Request.Form("money")
            Dim payId As String = Request.Form("payId")
            Dim myMoney As New tnPay.money
            Dim myMain As New main
            If myMain.checkIsNotNull(payId) = False Or myMain.checkIsNotNull(money) = False Or myMoney.getMoney(Session("id"))("money").ToString < money + money * 0.012 Then
                Return False
            End If
            If payId.IndexOf("@") <> -1 And payId.IndexOf(".") <> -1 Then
                If payId.IndexOf("@") > payId.IndexOf(".") Then Return False
            Else
                Return False
            End If
            Return myMoney.addMoveMoney(Session("id"), money, payId)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub addNewMoney()
        Dim payorder As New tnPay.money
        Dim orderpay As New Hashtable
        Dim strwite As String = payorder.payAdd(Session("id"), Request.Form("money"))
        strwite = strwite.Replace("'", """")
        Dim struri = "../paysystem/payto.aspx?fm=" & strwite
        Response.Redirect(struri)
    End Sub
End Class