using System;
namespace Domain.QueryParameters
{
	public class SymbolsParameters : QueryParameters
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Boolean Appvisible { get; set; }
	}
}
