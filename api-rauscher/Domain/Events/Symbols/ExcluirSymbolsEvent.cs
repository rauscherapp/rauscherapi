using Domain.Core.Events;
using System;

namespace Domain.Events
{
  public class ExcluirSymbolsEvent : Event
  {
    public ExcluirSymbolsEvent(
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
