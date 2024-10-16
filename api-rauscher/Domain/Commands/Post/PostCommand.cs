using System;
using Domain.Core.Commands;

namespace Domain.Commands
{
	public abstract class PostCommand : Command
	{
		public Guid ID { get; set; }
		public string TITLE { get; set; }
		public DateTime CREATEDDATE { get; set; }
		public string CONTENT { get; set; }
		public string AUTHOR { get; set; }
		public Boolean VISIBLE { get; set; }
		public DateTime? PUBLISHEDAT { get; set; }
		public Guid Folderid { get; set; }
		public string FolderName { get; set; }
		public string Language { get; set; }
	}
}
