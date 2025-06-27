using System;
using System.Collections.Generic;

namespace Domain.Models
{
	public class Symbols
	{
    public Symbols(
      Guid id,
    string code,
    string name,
    Boolean appvisible,
    string friendlyName,
    string symbolType,
    string vendor)
    {
      Id = id;
      Code = code;
      Name = name;
      Appvisible = appvisible;
      FriendlyName = friendlyName;
      SymbolType = symbolType;
      Vendor = vendor;
    }
    public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string FriendlyName { get; set; }
		
    public string SymbolType { get; set; }
    public string Vendor { get; set; }
		public Boolean Appvisible { get; set; }

    // Navigation property
    public virtual ICollection<CommoditiesRate> CommoditiesRates { get; set; }
  }
}
