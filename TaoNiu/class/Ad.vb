Public Class Ad
    Public Function ad_add(ByVal strTitle As String, ByVal strAd As String, ByVal adType As Integer)
        Dim myAd As New Hashtable, myHelper As New dbHelper
        myAd("@adTitle") = strTitle
        myAd("@ad") = strAd
        myAd("@adType") = adType
        Try
            myHelper.noneQuery("ad_add", myHelper.hashtableToInquery(myAd))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ad_del(ByVal adId As Integer) As Boolean
        Dim strSql As String, myHelper As New dbHelper
        strSql = "delete from advertisement where id=" & adId
        Return myHelper.querySql(strSql)
    End Function
    Public Function ad_get_uCenter() As String
        Dim myHelper As New dbHelper, strSql As String, myDs As New DataSet
        strSql = "select top 1 ad from advertisement where adType=1 order by id desc"
        myDs = myHelper.getQuerySql(strSql, "ad")
        Try
            Return myDs.Tables("ad").Rows(0)(0)
        Catch ex As Exception
            Return "没有新公告"
        End Try
    End Function
End Class
