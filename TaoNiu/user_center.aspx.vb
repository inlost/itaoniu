Public Class user_center1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("action") = "modify" And Request.Params("msg") <> Nothing Then
            Select Case Request.Params("msg")
                Case "r_name"
                    Response.Write("<script>alert('真实姓名为空或输入不合规范')</script>")
                Case "address"
                    Response.Write("<script>alert('联系地址为空或输入不合规范')</script>")
                Case "mobile"
                    Response.Write("<script>alert('手机号码为空或输入不合规范')</script>")
                Case "id"
                    Response.Write("<script>alert('身份证号码为空或输入不合规范')</script>")
                Case "qq"
                    Response.Write("<script>alert('QQ号码为空或输入不合规范')</script>")
                Case "passNotEq"
                    Response.Write("<script>alert('两次输入的新密码不一致')</script>")
                Case "passIsNull"
                    Response.Write("<script>alert('新密码为空或输入不合规范')</script>")
                Case "upFall"
                    Response.Write("<script>alert('图片上传失败" & Request.Params("wd") & "')</script>")
                Case "updateVdSuccess"
                    Response.Write("<script>alert('认证请求提交成功')</script>")
                Case "needInfo"
                    Response.Write("<script>alert('您需要完善个人信息后才能开店')</script>")
                Case "needId"
                    Response.Write("<script>alert('需要提交实名认证信息才能开店哦')</script>")
                Case Else
                    Response.Write("<script>alert('无法与服务器交换信息')</script>")
            End Select
        End If
    End Sub
    Public Sub getUserInfo()
        Dim strWite As String = ""
        strWite = setInfo("用户编号：", Session("id"), strWite)
        strWite = setInfo("E-mail：", Session("email"), strWite)
        strWite = setInfo("昵称：", Session("niceName"), strWite)
        strWite = setInfo("账户状态：", IIf(Session("status") = 1, "正常", "锁定"), strWite)
        Dim myUser As New user, userInfo As New Hashtable
        userInfo = myUser.getUserInfo(Session("id"))
        If userInfo Is Nothing Then
            strWite = setInfo("联系地址：", "未设置", strWite, False)
            strWite = setInfo("真实姓名：", "未设置", strWite)
            strWite = setInfo("身份认证：", "未认证", strWite)
            strWite = setInfo("手机：", "未设置", strWite)
            strWite = setInfo("QQ：", "未设置", strWite)
            strWite = setInfo("买家信用：", "没有记录", strWite)
            strWite = setInfo("卖家信用：", "没有记录", strWite)
            strWite = setInfo("Tip1：", "您的详细信息尚未填写，您可以点击<a href='?action=modify' style='color:red'>这儿</a>或右上角的'修改资料'来完善它们。", strWite, False)
            strWite = setInfo("Tip2：", "在这儿完善详细信息后，您便不用在购物过程中再重复输入这些信息了。", strWite, False)
        Else
            strWite = setInfo("联系地址：", userInfo("address"), strWite, False)
            strWite = setInfo("真实姓名：", userInfo("real_name"), strWite)
            Dim strTp As String = userInfo("id_status")
            If strTp = 0 Then
                strTp = "尚未认证"
            ElseIf strTp = 1 Then
                strTp = "认证审核中"
            Else
                strTp = "已认证"
            End If
            strWite = setInfo("身份认证：", strTp, strWite)
            strWite = setInfo("手机：", IIf(Len(userInfo("mobile")) > 3, userInfo("mobile"), "未设置"), strWite)
            strWite = setInfo("QQ：", IIf(Len(userInfo("qq")) > 3, userInfo("qq"), "未设置"), strWite)
            Dim xy As Integer
            Try
                xy = userInfo("buy_pj_good") - userInfo("buy_pj_bad")
            Catch ex As Exception
                xy = 0
            End Try
            strWite = setInfo("买家信用：", xy, strWite)
            Try
                xy = userInfo("sale_pj_good") - userInfo("sale_pj_bad")
            Catch ex As Exception
                xy = 0
            End Try
            strWite = setInfo("卖家信用：", xy, strWite)
        End If
        Response.Write(strWite)
        Response.Write("<div class='clear'></div>")
    End Sub
    Public Sub getModifyForm()
        Dim myUser As New user, userInfo As New Hashtable
        userInfo = myUser.getUserInfo(Session("id"))
        Dim strWite As String = ""
        Try
            strWite = setInfo("联系地址：", "<input name='address' value='" & IIf(Len(userInfo("address")) > 3, userInfo("address"), "广西壮族自治区柳州市") & "' type='text'/>", strWite, False)
            strWite = setInfo("手机：", "<input name='mobile' value='" & userInfo("mobile") & "' type='text'/>", strWite)
            strWite = setInfo("QQ：", "<input name='qq' value='" & userInfo("qq") & "' type='text'/>", strWite)
            strWite = setInfo("真实姓名：", "<input name='real_name' value='" & userInfo("real_name") & "' type='text'/>", strWite)
            strWite = setInfo("身份证号：", "<input name='id_code' value='" & userInfo("id_code") & "' type='text'/>", strWite)
        Catch ex As Exception
            strWite = setInfo("联系地址：", "<input name='address' value='广西壮族自治区柳州市' type='text'/>", strWite, False)
            strWite = setInfo("手机：", "<input name='mobile' type='text'/>", strWite)
            strWite = setInfo("QQ：", "<input name='qq' type='text'/>", strWite)
            strWite = setInfo("真实姓名：", "<input name='real_name' type='text'/>", strWite)
            strWite = setInfo("身份证号：", "<input name='id_code' type='text'/>", strWite)
        End Try
        Response.Write(strWite)
        Response.Write("<div class='clear'></div>")
    End Sub
    Public Sub getCheckRealForm()
        Dim myUser As New user, userInfo As New Hashtable
        userInfo = myUser.getUserInfo(Session("id"))
        Dim strWite As String = ""
        Try
            If userInfo("id_status") = 0 Then
                strWite = setInfo("Tip：", "清晰的身份证复印件或扫描件能让实名认证更快通过喔。", strWite, False)
                strWite = setInfo("身份证号：", "<input name='id_code' value='" & userInfo("id_code") & "' type='text'/>", strWite)
                strWite = setInfo("身份证：", "<input type='file' id='imgFile' name='imgFile' />", strWite)
                strWite += "<input class='subBt' type='submit' name='infoSub' value ='提交更改' />"
            Else
                strWite = setInfo("身份认证：", IIf(userInfo("id_status") = 1, "身份认证审核中", "已认证账户"), strWite)
            End If
        Catch ex As Exception
            strWite = setInfo("Tip：", "个人信息填写完整后才能进行身份认证哦！。", strWite, False)
        End Try
        Response.Write(strWite)
        Response.Write("<div class='clear'></div>")
    End Sub
    Public Sub getModifyPassForm()
        Dim strWite As String = ""
        strWite = setInfo("原密码：", "<input name='old_pass' type='password'/>", strWite, False)
        strWite = setInfo("新密码：", "<input name='new_pass' type='password'/>", strWite)
        strWite = setInfo("重复新密码：", "<input name='renew_pass' type='password'/>", strWite)
        Response.Write(strWite)
        Response.Write("<div class='clear'></div>")
    End Sub
    Private Function setInfo(ByVal key As String, ByVal value As String, ByVal strIn As String, Optional ByVal halfWidth As Boolean = True) As String
        Dim strPClass As String = IIf(halfWidth, "class='halfWidth'", "class='fullWidth'")
        strIn += "<p " & strPClass & "><span class='infoTitle'>" & key & "</span><span class='info'>" & value & "</span></p>"
        Return strIn
    End Function
    Public Sub getAd()
        Dim strWite As String, myAd As New Ad
        strWite = myAd.ad_get_uCenter
        Response.Write(strWite)
    End Sub
End Class