using Domain.Core.Events;
using System;

namespace Domain.Events
{
  public class ExcluirEventRegistryEvent : Event
  {
    public ExcluirEventRegistryEvent(
    Guid id,
    string eventname,
    string eventdescription,
    string eventtype,
    DateTime eventdate,
    string eventlocation,
    string eventlink
,
    bool published)
    {
      Id = id;
      Eventname = eventname;
      Eventdescription = eventdescription;
      Eventtype = eventtype;
      Eventdate = eventdate;
      Eventlocation = eventlocation;
      Eventlink = eventlink;
      Published = published;
    }
    public Guid Id { get; set; }
    public string Eventname { get; set; }
    public string Eventdescription { get; set; }
    public string Eventtype { get; set; }
    public DateTime Eventdate { get; set; }
    public string Eventlocation { get; set; }
    public string Eventlink { get; set; }
    public bool Published { get; set; }
  }
}
