Imports DictionaryProject
Imports <xmlns:svg="http://www.w3.org/2000/svg">
Imports <xmlns:xlink="http://www.w3.org/1999/xlink">

Public Class MainApp
    <STAThread()> _
    Public Shared Sub Main(ByVal args() As String)
        Try
            Dim ids As New List(Of String)
            Dim rule As String = "root"
            Dim project As String = Nothing
            For Each arg As String In args.Take(args.Length - 1)
                Dim parts() As String = arg.Split(New Char() {"="c}, 2)

                If parts.Length = 2 Then
                    Select Case parts(0)
                        Case "--id"
                            ids.Add(parts(1).Trim(""""c))
                        Case "--rule"
                            rule = parts(1).Trim(""""c)
                        Case "--project"
                            project = parts(1).Trim(""""c)
                    End Select
                End If
            Next

            If Not String.IsNullOrEmpty(project) Then
                Dim proj As Project = ProjectSerializer.LoadProject(project)

                If proj.Warnings.Count = 0 Then
                    If proj.Rules.GetRuleByName(rule) IsNot Nothing Then
                        Dim svg As XElement
                        Using st As IO.FileStream = IO.File.Open(args.Last, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite Or IO.FileShare.Delete)
                            Using sr As New System.Xml.XmlTextReader(st)
                                sr.MoveToContent()
                                svg = XDocument.ReadFrom(sr)
                            End Using
                        End Using

                        If svg IsNot Nothing AndAlso ids.Count > 0 Then
                            For Each id As String In ids
                                Dim elId As String = id
                                Dim el As XElement = (From e As XElement In svg.Descendants Where e.@id = elId).FirstOrDefault()

                                If el IsNot Nothing Then
                                    Dim fillElement As XElement = Nothing
                                    If el.Name.LocalName = "flowRoot" Then
                                        fillElement = el.<svg:flowPara>.FirstOrDefault()
                                    ElseIf el.Name.LocalName = "text" Then
                                        fillElement = el...<svg:tspan>.FirstOrDefault()
                                    ElseIf el.Name.LocalName = "textPath" Then
                                        fillElement = el...<svg:tspan>.FirstOrDefault()
                                    ElseIf el.Name.LocalName = "path" AndAlso Not (From e As XElement In svg.Descendants Where e.@xlink:href = "#" & elId).Any() Then
                                        Dim pathParts() As String = el.@d.Split(" "c)

                                        If pathParts(0) = "M" Then
                                            Dim coords() As String = pathParts(1).Split(","c)
                                            Dim x As Double = 0
                                            Dim y As Double = 0

                                            Double.TryParse(coords(0), x)
                                            Double.TryParse(coords(1), y)

                                            Dim textEl = <svg:text style="font-size:12pt;font-style:normal;font-weight:normal;fill:#000000;fill-opacity:1;stroke:none;stroke-width:1px;stroke-linecap:butt;stroke-linejoin:miter;stroke-opacity:1;font-family:Bitstream Vera Sans"/>

                                            Dim textPathEl = <svg:textPath/>
                                            textPathEl.@xlink:href = "#" & id
                                            textEl.Add(textPathEl)

                                            fillElement = <svg:tspan/>
                                            fillElement.@x = x.ToString()
                                            fillElement.@y = y.ToString()

                                            textPathEl.Add(fillElement)

                                            el.AddAfterSelf(textEl)
                                        End If
                                    End If

                                    If fillElement IsNot Nothing Then
                                        fillElement.Value = proj.GetWord(rule).ToString()
                                    End If
                                End If
                            Next
                        End If

                        Dim outDoc As New XDocument(New XDeclaration("1.0", "utf-8", Nothing), svg)

                        Using stdout As IO.Stream = Console.OpenStandardOutput()
                            Using sw As New IO.StreamWriter(stdout, Text.Encoding.UTF8)
                                outDoc.Save(sw, SaveOptions.DisableFormatting)
                            End Using
                        End Using
                    End If
                End If
            End If
        Catch ex As Exception
            Console.Error.WriteLine(ex.Message)
            Console.Error.WriteLine(ex.StackTrace)
        End Try
    End Sub
End Class
