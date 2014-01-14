using System;

namespace ProjectAutomata
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length < 2)
			{
				PrintUsage();
				return;
			}

			var command = args[0];
			var fileName = args[1];

			switch (command)
			{
				case "import":
					Import(fileName);
					break;
				case "export":
					Export(fileName);
					break;
				default:
					PrintUsage();
					break;
			}
		}

		private static void PrintUsage()
		{
			Console.WriteLine(@"Usage:
    ProjectAutomata import filename.org
    ProjectAutomata export filename.prj");
		}

		private static void Import(string fileName)
		{
			var importer = new Importer();
			importer.Import(fileName);
		}

		private static void Export(string fileName)
		{
			var exporter = new Exporter();
			exporter.Export(fileName);
		}
	}
}
