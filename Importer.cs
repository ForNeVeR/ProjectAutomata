using System.Collections.Generic;
using Microsoft.Office.Interop.MSProject;

namespace ConsoleApplication2
{
	class Importer
	{
		public void Import(string fileName)
		{
			var tasks = GetTasks(fileName);
			var project = OpenProject();

			CreateTasks(project, tasks);
		}

		private IEnumerable<TaskDescription> GetTasks(string fileName)
		{
			var parser = new Parser();

			var stack = new Stack<TaskDescription>();
			var allTasks = new List<TaskDescription>(parser.Parse(fileName));

			foreach (var task in allTasks)
			{
				if (stack.Count == 0 || task.Level == 1)
				{
					stack.Push(task);
					continue;
				}

				while (task.Level <= stack.Peek().Level)
				{
					stack.Pop();
				}

				stack.Peek().Children.Add(task);
			}

			return allTasks;
		}

		private Project OpenProject()
		{
			var msProject = new Application();
			var application = msProject.Application;
			var project = application.Projects.Add();
			return project;
		}

		private void CreateTasks(Project project, IEnumerable<TaskDescription> tasks)
		{
			foreach (var taskDescription in tasks)
			{
				var task = project.Tasks.Add(taskDescription.Name);
				task.OutlineLevel = (short)taskDescription.Level;
				task.Notes = taskDescription.Note;

				if (taskDescription.Children.Count == 0)
				{
					double work = taskDescription.Estimation.TotalMinutes;
					task.Work = work;
				}
			}
		}
	}
}
