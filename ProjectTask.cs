using System;
using System.Linq;
using Microsoft.Office.Interop.MSProject;

namespace ProjectAutomata
{
	internal class ProjectTask
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
}
