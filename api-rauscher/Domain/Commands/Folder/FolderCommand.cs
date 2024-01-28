using System;
using Domain.Core.Commands;

namespace Domain.Commands
{
	public abstract class FolderCommand : Command
	{
		public Guid ID { get; set; }
		public string TITLE { get; set; }
		public string SLUG { get; set; }
		public string ICON { get; set; }
	}
}
