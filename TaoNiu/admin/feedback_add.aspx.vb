Public Class feedback_add
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    ''' <summary>
    ''' 提交数据
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSumbit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSumbit.Click
        Dim myProduct As New Hashtable
        myProduct("@uid") = Session("id") '-用户主健
        myProduct("@questiontype") = questiontype.SelectedValue '-反馈问题类型 0:不满意  1:满意
        myProduct("@questiont") = questiont.Text '-反馈题目
        myProduct("@ceateon") = DateTime.Now '-发布时间
        Dim myHelper As New dbHelper
        '传入存储过程插入一条语句到feebackquestion表里
        If myHelper.insertQuery("feedbackquestion_add", myHelper.hashtableToInquery(myProduct)) Then
            Response.Write("<script>alert('添加成功')</script>")
        Else
            Response.Write("<script>alert('添加失败')</script>")
        End If

    End Sub
End Class