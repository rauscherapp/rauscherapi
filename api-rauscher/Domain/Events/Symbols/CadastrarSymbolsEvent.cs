using Domain.Core.Events;
using System;

namespace Domain.Events
{
  public class CadastrarSymbolsEvent : Event
  {
    public CadastrarSymbolsEvent(
    Guid id,
    string code,
    string name,
    Boolean appvisible,
    string friendlyName)
    {
      Id = id;
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
