using Microsoft.Office.Interop.MSProject;

namespace ProjectAutomata
{
	class ProjectWorker
	{
		protected Application Application { get; set; }
		
		protected ProjectWorker()
		{
			Application = new Application { Visible = true };
		}
	}
}
