using System;

namespace Domain.Models
{
	public class Folder
	{
		public Folder(
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
