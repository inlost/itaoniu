Imports Microsoft.VisualBasic
Imports System.Drawing
Public Class CreateImage

    Public Shared Sub DrawImage()

        Dim img As New CreateImage()

        HttpContext.Current.Session("CheckCode") = img.RndNum(6)

        img.CreateImages(HttpContext.Current.Session("CheckCode").ToString())

    End Sub 'DrawImage

    '/ <summary>

    '/ 生成验证图片

    '/ </summary>

    '/ <param name="checkCode">验证字符</param>

    Private Sub CreateImages(ByVal checkCode As String)

        Dim iwidth As Integer = CInt(checkCode.Length * 15)

        Dim image As New System.Drawing.Bitmap(iwidth, 40)

        Dim g As Graphics = Graphics.FromImage(image)

        g.Clear(Color.White)

        '定义颜色

        Dim c As Color() = {Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple}

        '定义字体

        Dim font As String() = {"Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体"}

        Dim rand As New Random()

        '随机输出噪点

        Dim i As Integer

        For i = 0 To 49

            Dim x As Integer = rand.Next(image.Width)

            Dim y As Integer = rand.Next(image.Height)

            g.DrawRectangle(New Pen(Color.LightGray, 0), x, y, 1, 1)

        Next i

        '输出不同字体和颜色的验证码字符

        For i = 0 To checkCode.Length - 1

            Dim cindex As Integer = rand.Next(7)

            Dim findex As Integer = rand.Next(5)

            Dim f = New System.Drawing.Font(font(findex), 16, System.Drawing.FontStyle.Bold)

            Dim b = New System.Drawing.SolidBrush(c(cindex))

            Dim ii As Integer = 4

            If (i + 1) Mod 2 = 0 Then

                ii = 2

            End If

            g.DrawString(checkCode.Substring(i, 1), f, b, 3 + i * 12, ii)

        Next i

        '画一个边框

        g.DrawRectangle(New Pen(Color.White, 0), 0, 0, image.Width - 1, image.Height - 1)

        '输出到浏览器

        Dim ms As New System.IO.MemoryStream()

        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)

        HttpContext.Current.Response.ClearContent()

        'Response.ClearContent();

        HttpContext.Current.Response.ContentType = "image/Jpeg"

        HttpContext.Current.Response.BinaryWrite(ms.ToArray())

        g.Dispose()

        image.Dispose()

    End Sub 'CreateImages

    '/ <summary>

    '/ 生成随机的字母

    '/ </summary>

    '/ <param name="vcodeNum">生成字母的个数</param>

    '/ <returns>string</returns>

    Private Function RndNum(ByVal VcodeNum As Integer) As String

        Dim allChar As String = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z"

        Dim allCharArray() As String = allChar.Split(",")

        Dim randomCode As String = ""

        Dim temp As Integer = -1

        '记录上次随机数值，尽量避免生产几个一样的随机数

        Dim rand As Random = New Random

        Dim i As Integer = 0

        Do While (i < VcodeNum)

            If (temp <> -1) Then

                Dim key As Integer = CType(DateTime.Now.Ticks Mod System.Int32.MaxValue, Integer)

                '用系统时间产生随机种子

                rand = New Random(key)

            End If

            Dim t As Integer = rand.Next(allCharArray.Length) + 1

            If t > allCharArray.Length - 1 Then

                t = allCharArray.Length - 1

            End If

            If temp = t Then

                i -= 1

                randomCode = Microsoft.VisualBasic.Left(randomCode, i)

            End If

            temp = t

            randomCode = randomCode + allCharArray(t)

            i += 1

        Loop

        Return randomCode

    End Function 'RndNum

End Class 'CreateImage
