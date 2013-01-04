Public Class site_front_withSearch
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub getGoodRange()
        Dim myGoods As New goods
        Dim strWite As String = myGoods.good_send_range_get_str
        Response.Write(strWite)
    End Sub
    Public Sub callSetOnline()
        If Session("onTheWay") = "loginSuccess" Then
            Response.Write("tn.setOnline(1)")
        End If
    End Sub

    Public Sub getSatisfied()
        Dim myHash As Hashtable()
        Dim sql As String
        sql = "SELECT * FROM feedbackquestion where questiontype=1"
        Dim myDbhelper As New dbHelper
        Dim ds As New DataSet
        ds = myDbhelper.getQuerySql(sql, "FeedBack")
        Dim myType As New typeHelper
        myHash = myType.dsToHash(ds, "FeedBack")
        If myHash(0)("count") = 0 Then Exit Sub
        Dim strWite As String = ""
        For Each item As Hashtable In myHash
            strWite += "<li>"
            strWite += " <input type='checkbox' name='questiont' value='" & item("Id") & "' />" & item("Questiont")
            strWite += "</li>"
        Next
        Response.Write(strWite)

    End Sub

    Public Sub getNotSatisfied()
        Dim myHash As Hashtable()
        Dim sql As String
        sql = "SELECT * FROM feedbackquestion where questiontype=0"
        Dim myDbhelper As New dbHelper
        Dim ds As New DataSet
        ds = myDbhelper.getQuerySql(sql, "FeedBack")
        Dim myType As New typeHelper
        myHash = myType.dsToHash(ds, "FeedBack")
        If myHash(0)("count") = 0 Then Exit Sub
        Dim strWite As String = ""
        For Each item As Hashtable In myHash
            strWite += "<li>"
            strWite += " <input type='checkbox' name='questiont' value='" & item("Id") & "' />" & item("Questiont")
            strWite += "</li>"
        Next
        Response.Write(strWite)

    End Sub



End Class