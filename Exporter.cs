using System;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.MSProject;
using Exception = System.Exception;

namespace ProjectAutomata
{
	class Exporter
	{
		public void Export(string fileName)
		{
			fileName = Path.GetFullPath(fileName);

			var msProject = new Application { Visible = true };
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
