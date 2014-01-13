using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectAutomata
{
	public class TaskDescription
	{
		public string Name { get; set; }
		public TimeSpan Estimation { get; set; }
		public string Note { get; set; }
		public int Level { get; set; }
		public List<TaskDescription> Children { get; private set; }

		public TaskDescription()
		{
			Children = new List<TaskDescription>();
		}
	}
}
