Public Class mail
    Public Function sendMail(ByVal mail_to As String, ByVal mail_title As String, ByVal mail_body As String) As Boolean
        Dim client = New System.Net.Mail.SmtpClient()
        Dim myMain = New main
        client.Host = myMain.siteSmtp
        client.UseDefaultCredentials = True
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
        client.Credentials = New System.Net.NetworkCredential(myMain.siteSmtp_user, myMain.siteSmtp_pass)
        Dim Message = New System.Net.Mail.MailMessage()
        Message.From = New System.Net.Mail.MailAddress(myMain.siteSmtp_user)
        'Message.To.Add("78366288@qq.com")
        Message.To.Add(mail_to)
        Message.Subject = mail_title
        Message.Body = mail_body
        Message.SubjectEncoding = System.Text.Encoding.UTF8
        Message.BodyEncoding = System.Text.Encoding.UTF8
        Message.Priority = System.Net.Mail.MailPriority.High
        Message.IsBodyHtml = True
        Try
            client.Send(Message)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function buidMailBody(ByVal strMain As String, ByVal strNew As String, Optional ByVal addType As String = "br", Optional ByVal strHref As String = "") As String
        Select Case addType.ToLower
            Case "br"
                strMain = strMain & "<br/>" & strNew
            Case "h2"
                strMain = strMain & "<br/><h2>" & strNew & "<h2>"
            Case "img"
                strMain = strMain & "<br/><img rel='mailPic' src='" & strNew & "'/>"
            Case "a"
                strMain = strMain & "<a href='" & strHref & "'>" & strNew & "</a>"
            Case Else
                strMain = strMain & strNew
        End Select
        Return strMain
    End Function
End Class
