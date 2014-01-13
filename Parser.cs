using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectAutomata
{
	class Parser
	{
		private static readonly Regex TaskHeader = new Regex(@"^(\*+)(?: \[(.*?)\])? (.*)$", RegexOptions.Compiled);
		private static readonly Regex Number = new Regex(@"\d+", RegexOptions.Compiled);
		
		public IEnumerable<TaskDescription> Parse(string fileName)
		{
			TaskDescription task = null;
			foreach (var line in File.ReadLines(fileName))
			{
				var match = TaskHeader.Match(line);
				if (match.Success)
				{
					if (task != null)
					{
						yield return task;
					}
					
					task = new TaskDescription
					{
						Level = match.Groups[1].Value.Length,
						Estimation = ParseEstimation(match.Groups[2].Value),
						Name = match.Groups[3].Value,
						Note = string.Empty
					};
				}
				else if (task != null)
				{
					task.Note += " " + line;
				}
			}

			if (task != null)
			{
				yield return task;
			}
		}

		private TimeSpan ParseEstimation(string estimation)
		{
			if (string.IsNullOrEmpty(estimation))
			{
				return TimeSpan.Zero;
			}
			
			var match = Number.Match(estimation);
			return TimeSpan.FromHours(int.Parse(match.Value));
		}
	}
}
