using System;

namespace Domain.Models
{
	public class Symbols
	{
    public Symbols(
    string code,
    string name,
    Boolean appvisible,
    string friendlyName)
    {
      Id = Guid.NewGuid();
      Code = code;
      Name = name;
      Appvisible = appvisible;
      FriendlyName = friendlyName;
    }
    public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string FriendlyName { get; set; }
		public Boolean Appvisible { get; set; }
	}
}
