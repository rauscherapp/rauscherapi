using System;

namespace Domain.QueryParameters
{
	public class PostParameters : QueryParameters
	{
		public Guid ID { get; set; }
		public string TITLE { get; set; }
		public DateTime CREATEDATE { get; set; }
		public string CONTENT { get; set; }
		public string folder { get; set; }
		public string AUTHOR { get; set; }
		public Boolean? VISIBLE { get; set; }
		public DateTime? PUBLISHEDAT { get; set; }
		public string FolderName { get; set; }
	}
}
