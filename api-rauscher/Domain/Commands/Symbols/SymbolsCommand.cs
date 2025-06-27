using System;
using Domain.Core.Commands;

namespace Domain.Commands
{
	public abstract class SymbolsCommand : Command
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string FriendlyName { get; set; }
		public string SymbolType { get; set; }
		public string Vendor { get; set; }
		public Boolean Appvisible { get; set; }
	}
}
