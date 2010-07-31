using System;
using Monobjc.Cocoa;

namespace Whee.WordBuilder.Cocoa
{
	public class MainClass
	{
		public MainClass ()
		{
		}
		
		public void Run()
		{
			Monobjc.ObjectiveCRuntime.LoadFramework("Cocoa");
			Monobjc.ObjectiveCRuntime.Initialize();
			
			NSApplication.Bootstrap();
			NSApplication.LoadNib("Main.nib");
			NSApplication.RunApplication();
		}
		
		public static void Main(string[] args)
		{
			MainClass main = new MainClass();
			main.Run();
		}
		
	}
}

