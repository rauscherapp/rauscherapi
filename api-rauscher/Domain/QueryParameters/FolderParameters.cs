using System;

namespace Domain.QueryParameters
{
	public class FolderParameters : QueryParameters
	{
		public Guid ID { get; set; }
		public string TITLE { get; set; }
		public string SLUG { get; set; }
		public string ICON { get; set; }
	}
}
