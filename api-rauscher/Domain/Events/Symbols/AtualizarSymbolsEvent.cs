using Domain.Core.Events;
using System;

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

    public AtualizarSymbolsEvent(Guid id, string code, string name, string friendlyName, string symbolType, bool appvisible)
    {
      Id = id;
      Code = code;
      Name = name;
      FriendlyName = friendlyName;
      SymbolType = symbolType;
      Appvisible = appvisible;
    }

    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string FriendlyName { get; set; }

    public string SymbolType { get; set; }
    public Boolean Appvisible { get; set; }
  }
}
