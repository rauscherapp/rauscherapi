using System;
using Domain.Core.Events;

namespace Domain.Events
{
	    public class AtualizarPostEvent : Event
	{
		public AtualizarPostEvent(
		Guid iD,
		string tITLE,
		DateTime cREATEDATE,
		string cONTENT,
		string aUTHOR,
		Boolean? vISIBLE,
		DateTime? pUBLISHEDAT,
		Guid folderid,
		string language
		)
		{
			ID = iD;
			TITLE = tITLE;
			CREATEDATE = cREATEDATE;
			CONTENT = cONTENT;
			AUTHOR = aUTHOR;
			VISIBLE = vISIBLE;
			PUBLISHEDAT = pUBLISHEDAT;
			Folderid = folderid;
			language = language;
		}
		public Guid ID { get; set; }
		public string TITLE { get; set; }
		public DateTime CREATEDATE { get; set; }
		public string CONTENT { get; set; }
		public string AUTHOR { get; set; }
		public Boolean? VISIBLE { get; set; }
		public DateTime? PUBLISHEDAT { get; set; }
		public Guid Folderid { get; set; }
	}
}
