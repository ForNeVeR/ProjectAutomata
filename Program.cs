using System;
using System.Linq;
using Microsoft.Office.Interop.MSProject;
using Exception = System.Exception;

namespace ConsoleApplication2
{
	internal class Program
	{
		private class ProjectTask
		{
			public string Name { get; private set; }
			public TimeSpan Duration { get; private set; }
			public TimeSpan Work { get; private set; }
			public ProjectTask[] Subtasks { get; private set; }

			public ProjectTask(Task task)
			{
				Name = task.Name;
				Duration = TimeSpan.FromMinutes(task.Duration);
				Work = TimeSpan.FromMinutes(task.Work);
				Subtasks = task.OutlineChildren.Cast<Task>().Select(t => new ProjectTask(t)).ToArray();
			}
		}
		
		private static void Main(string[] args)
		{
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
			}
		}

		private static void Import(string fileName)
		{
			var importer = new Importer();
			importer.Import(fileName);
		}

		private static void Export(string fileName)
		{
			var msProject = new Application();
			var application = msProject.Application;
			if (!application.FileOpenEx(fileName))
			{
				throw new Exception("Cannot open file " + fileName);
			}

			var projects = application.Projects;
			Project project = null;
			foreach (var p in projects.Cast<Project>())
			{
				if (p.FullName == fileName)
				{
					project = p;
					break;
				}
			}

			if (project == null)
			{
				throw new Exception("Cannot find project");
			}

			var task = new ProjectTask(project.ProjectSummaryTask);
			PrintTaskInfo(task);
		}

		private static void PrintTaskInfo(ProjectTask task, int indent = 0)
		{
			for (var i = 0; i < indent; ++i)
			{
				Console.Write(" ");
			}

			Console.WriteLine("{0} / {1} / {2}", task.Name, task.Duration, task.Work);
			foreach (var subtask in task.Subtasks)
			{
				PrintTaskInfo(subtask, indent + 1);
			}
		}
	}
}
