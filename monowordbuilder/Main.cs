using System;
using System.Collections.Generic;

namespace monotest
{
	public class Application
	{
		public static void Main()
		{
			string[] args = System.Environment.GetCommandLineArgs();
	
			if (2 > args.Length) {
				System.Console.WriteLine("wbuilder <filename>[ -v][ -r <rule> <amount>]*");
				return;
			}
	
			bool extendedOutput = new List<string>(args).Contains("-v");
	
			Project project = ProjectSerializer.LoadProject(args[1]);
	
			if (project != null) {
				if (project.Warnings.Count > 0) {
					System.Console.WriteLine(string.Join(System.Environment.NewLine, project.Warnings.ToArray()));
					return;
				}
	
				Dictionary<string, int> rules = new Dictionary<string, int>();
				string rule = "";
				int ruleCount;
				int mode = 0;
	
				for (int c = 2; c <= args.Length - 1; c++) {
					switch (mode) {
						case 0:
							if ("-r" == args[c].ToLower()) {
								mode = 1;
							}
							break;
						case 1:
							rule = args[c];
	
							if (project.Rules.GetRuleByName(rule) != null) {
								mode = 2;
							}
							else {
								System.Console.WriteLine("wbuilder <filename>[ -v][ -r <rule> <amount>]*");
								System.Console.WriteLine("Rule {0} not found", args[c]);
								return;
							}
							break;
						case 2:
							if (int.TryParse(args[c], out ruleCount)) {
								rules.Add(rule, ruleCount);
								mode = 0;
							}
							else {
								System.Console.WriteLine("wbuilder <filename>[ -v][ -r <rule> <amount>]*");
								System.Console.WriteLine("Expected amount, got {0}", args[c]);
								return;
							}
							break;
					}
				}
	
				if (rules.Count == 0) {
					rules = project.StartRules;
	
					if (rules.Count == 0) {
						rules.Add("root", 100);
					}
				}
	
				foreach (string ruleiter in rules.Keys) {
					for (int c = 1; c <= rules[ruleiter]; c++) {
						if (extendedOutput) {
							System.Console.WriteLine(project.GetWord(ruleiter).Description(""));
						}
						else {
							System.Console.WriteLine(project.GetWord(ruleiter).ToString());
						}
					}
				}
			}
		}
	}
}
