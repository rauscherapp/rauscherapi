using System;
using Domain.Core.Events;

namespace Domain.Events
{
	    public class AtualizarFolderEvent : Event
	{
		public AtualizarFolderEvent(
		Guid iD,
		string tITLE,
		string sLUG,
		string iCON
		)
		{
			ID = iD;
			TITLE = tITLE;
			SLUG = sLUG;
			ICON = iCON;
		}
		public Guid ID { get; set; }
		public string TITLE { get; set; }
		public string SLUG { get; set; }
		public string ICON { get; set; }
	}
}
