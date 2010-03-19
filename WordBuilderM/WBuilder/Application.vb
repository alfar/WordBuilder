Imports DictionaryProject

Public Class Application
	Public Shared Sub Main()
		Dim args As String() = System.Environment.GetCommandLineArgs()
		
		If 2 > args.Length Then
			System.Console.WriteLine("wbuilder <filename>")
			Exit Sub
		End If

		Dim extendedOutput As Boolean = args.Contains("-v")
				
		Dim project As Project = ProjectSerializer.LoadProject(args(1))
		
		If project IsNot Nothing Then
			If project.Warnings.Count > 0 Then
				System.Console.WriteLine(String.Join(vbCrLf, project.Warnings.ToArray()))
				Exit Sub
			End If
		
			If project.StartRules.Count = 0 Then
				project.StartRules.Add("root", 100)
			End If
			
			For Each rule As String In project.StartRules.Keys
				For c As Integer = 1 To project.StartRules(rule)
					If extendedOutput Then
						System.Console.WriteLine(project.GetWord(rule).Description())
					Else
						System.Console.WriteLine(project.GetWord(rule).ToString())
					End If
				Next
			Next
		End If
	End Sub
End Class
