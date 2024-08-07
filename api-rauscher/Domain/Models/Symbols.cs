using System;
using System.Collections.Generic;

namespace Domain.Models
{
	public class Symbols
	{
    public Symbols(
    string code,
    string name,
    Boolean appvisible,
    string friendlyName,
    string symbolType)
    {
      Id = Guid.NewGuid();
      Code = code;
      Name = name;
      Appvisible = appvisible;
      FriendlyName = friendlyName;
      SymbolType = symbolType;
    }
    public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string FriendlyName { get; set; }
		
    public string SymbolType { get; set; }
		public Boolean Appvisible { get; set; }

    // Navigation property
    public virtual ICollection<CommoditiesRate> CommoditiesRates { get; set; }
  }
}
