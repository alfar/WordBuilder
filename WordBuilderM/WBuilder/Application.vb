Imports DictionaryProject

Public Class Application
	Public Shared Sub Main()
		Dim args As String() = System.Environment.GetCommandLineArgs()
		
		If 2 > args.Length Then
			System.Console.WriteLine("wbuilder <filename>[ -v][ -r <rule> <amount>]*")
			Exit Sub
		End If

		Dim extendedOutput As Boolean = args.Contains("-v")
				
		Dim project As Project = ProjectSerializer.LoadProject(args(1))
		
		If project IsNot Nothing Then
			If project.Warnings.Count > 0 Then
				System.Console.WriteLine(String.Join(vbCrLf, project.Warnings.ToArray()))
				Exit Sub
			End If

			Dim rules As New Dictionary(Of String, Integer)()
			Dim rule As String
			Dim ruleCount As Integer			
			Dim mode As Integer = 0
					
			For c As Integer = 2 To args.Length - 1
				Select Case mode
					Case 0
						If "-r" = args(c).ToLower() Then
							mode = 1
						End If
					Case 1
						rule = args(c)
						
						If project.Rules.GetRuleByName(rule) IsNot Nothing Then
							mode = 2
						Else
							System.Console.WriteLine("wbuilder <filename>[ -v][ -r <rule> <amount>]*")
							System.Console.WriteLine("Rule {0} not found", args(c))
							Exit Sub
						End If
					Case 2
						If Integer.TryParse(args(c), ruleCount) Then
							rules.Add(rule, ruleCount)
							mode = 0
						Else
							System.Console.WriteLine("wbuilder <filename>[ -v][ -r <rule> <amount>]*")
							System.Console.WriteLine("Expected amount, got {0}", args(c))
							Exit Sub
						End If
				End Select
			Next

			If rules.Count = 0 Then
				rules = project.StartRules

				If rules.Count = 0 Then
					rules.Add("root", 100)
				End If
			End If
			
			For Each rule As String In rules.Keys
				For c As Integer = 1 To rules(rule)
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
