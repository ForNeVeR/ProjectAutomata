using System;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.MSProject;
using Exception = System.Exception;

namespace ProjectAutomata
{
	class Exporter : ProjectWorker
	{
		public void Export(string fileName)
		{
			fileName = Path.GetFullPath(fileName);

			if (!Application.FileOpenEx(fileName, true))
			{
				throw new Exception("Cannot open file " + fileName);
			}

			var project = Application.Projects.Cast<Project>().FirstOrDefault(p => p.FullName == fileName);
			if (project == null)
			{
				throw new Exception("Cannot find project");
			}

			var task = new ProjectTask(project.ProjectSummaryTask);
			foreach (var subtask in task.Subtasks)
			{
				PrintTaskInfo(subtask);
			}
		}

		private static void PrintTaskInfo(ProjectTask task, int indent = 1)
		{
			for (var i = 0; i < indent; ++i)
			{
				Console.Write("*");
			}

			Console.WriteLine(" [{0} h] {1}", task.Work.TotalHours, task.Name);
			if (!string.IsNullOrEmpty(task.Notes))
			{
				Console.WriteLine(task.Notes);
			}

			foreach (var subtask in task.Subtasks)
			{
				PrintTaskInfo(subtask, indent + 1);
			}
		}
	}
}
