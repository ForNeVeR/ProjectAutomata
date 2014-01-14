using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.MSProject;

namespace ProjectAutomata
{
	internal class ProjectTask
	{
		public string Name { get; private set; }
		public TimeSpan Duration { get; private set; }
		public TimeSpan Work { get; private set; }
		public int Level { get; private set; }
		public List<ProjectTask> Subtasks { get; private set; }
		public string Notes { get; private set; }

		private ProjectTask(int level)
		{
			Subtasks = new List<ProjectTask>();

			Level = level;
		}

		public ProjectTask(Task task, int level = 0)
			: this(level)
		{
			Name = task.Name;
			Duration = TimeSpan.FromMinutes(task.Duration);
			Work = TimeSpan.FromMinutes(task.Work);
			Notes = task.Notes;

			Subtasks.AddRange(task.OutlineChildren.Cast<Task>().Select(t => new ProjectTask(t, level + 1)));
		}

		public ProjectTask(string name, TimeSpan estimation, int level)
			: this(level)
		{
			Name = name;
			Work = estimation;
		}

		public void AddNoteLine(string line)
		{
			if (Notes == null)
			{
				Notes = line;
			}
			else
			{
				Notes += "\n" + line;
			}
		}
	}
}
