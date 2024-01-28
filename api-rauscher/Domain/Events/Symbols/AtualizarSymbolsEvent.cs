using System;
using Domain.Core.Events;

namespace Domain.Events
{
	    public class AtualizarSymbolsEvent : Event
	{
		public AtualizarSymbolsEvent(
		Guid id,
		string code,
		string name,
		Boolean appvisible
		)
		{
			Id = id;
			Code = code;
			Name = name;
			Appvisible = appvisible;
		}
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Boolean Appvisible { get; set; }
	}
}
