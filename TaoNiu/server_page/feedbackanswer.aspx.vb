Public Class feedbackanswer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myProduct As New Hashtable
        myProduct("@uid") = Session("id") '-用户主健
        myProduct("@questiontype") = Request.Form("questiontype") '-反馈问题类型 0:不满意  1:满意
        myProduct("@questiont") = Request.Form("questiont") '-反馈题目
        myProduct("@address") = Request.Form("address") '-反馈网址
        myProduct("@contents") = Request.Form("contents") '-反馈内容
        myProduct("@ceateon") = DateTime.Now '-反馈时间
        Dim myHelper As New dbHelper
        '传入存储过程插入一条语句到feebackanswer表里
        If myHelper.insertQuery("feedbackanswer_add", myHelper.hashtableToInquery(myProduct)) Then
            Response.Write("<script>alert('添加成功')</script>")
        Else
            Response.Write("<script>alert('添加失败')</script>")
        End If
    End Sub

End Class