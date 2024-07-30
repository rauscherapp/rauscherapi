using System;
using Domain.Core.Commands;

namespace Domain.Commands
{
	public abstract class AboutUsCommand : Command
	{
		public Guid Id { get; set; }
		public string Description { get; set; }
	}
}
