Public Class classified_cat
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub addCat1_Click(sender As Object, e As EventArgs) Handles addCat1.Click
        Dim myClassified As New classifieds
        Dim newCat As New Hashtable
        newCat("@catDeep") = 1
        newCat("@catOrder") = 0
        newCat("@catFather") = -1
        newCat("@catName") = Trim(TextBox1.Text)
        newCat("@catImg") = 0
        Dim catId As Integer = myClassified.cat_add(newCat)
        myClassified.cat_order_set(catId, catId)
        Response.Redirect("classified_cat.aspx")
    End Sub

    Protected Sub addCat2_Click(sender As Object, e As EventArgs) Handles addCat2.Click
        If ListBox1.SelectedIndex >= 0 Then
            Dim myClassified As New classifieds
            Dim newCat As New Hashtable
            newCat("@catDeep") = 2
            newCat("@catOrder") = 0
            newCat("@catFather") = ListBox1.SelectedValue
            newCat("@catName") = Trim(TextBox2.Text)
            newCat("@catImg") = 0
            Dim catId As Integer = myClassified.cat_add(newCat)
            myClassified.cat_order_set(catId, catId)
            Response.Redirect("classified_cat.aspx")
        End If
    End Sub

    Protected Sub addCat3_Click(sender As Object, e As EventArgs) Handles addCat3.Click
        If ListBox2.SelectedIndex >= 0 Then
            Dim myClassified As New classifieds
            Dim newCat As New Hashtable
            newCat("@catDeep") = 3
            newCat("@catOrder") = 0
            newCat("@catFather") = ListBox2.SelectedValue
            newCat("@catName") = Trim(TextBox3.Text)
            newCat("@catImg") = 0
            Dim catId As Integer = myClassified.cat_add(newCat)
            myClassified.cat_order_set(catId, catId)
            Response.Redirect("classified_cat.aspx")
        End If
    End Sub
End Class