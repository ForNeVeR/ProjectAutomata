using System.Collections.Generic;
using Microsoft.Office.Interop.MSProject;

namespace ProjectAutomata
{
	class Importer : ProjectWorker
	{
		public void Import(string fileName)
		{
			var tasks = GetTasks(fileName);
			var project = OpenProject();

			CreateTasks(project, tasks);
		}

		private IEnumerable<ProjectTask> GetTasks(string fileName)
		{
			var parser = new Parser();

			var stack = new Stack<ProjectTask>();
			var allTasks = new List<ProjectTask>(parser.Parse(fileName));

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

				stack.Peek().Subtasks.Add(task);
			}

			return allTasks;
		}

		private Project OpenProject()
		{
			var project = Application.Projects.Add();
			return project;
		}

		private void CreateTasks(Project project, IEnumerable<ProjectTask> tasks)
		{
			foreach (var taskDescription in tasks)
			{
				var task = project.Tasks.Add(taskDescription.Name);
				task.OutlineLevel = (short)taskDescription.Level;
				task.Notes = taskDescription.Notes;

				if (taskDescription.Subtasks.Count == 0)
				{
					double work = taskDescription.Work.TotalMinutes;
					task.Work = work;
				}
			}
		}
	}
}
