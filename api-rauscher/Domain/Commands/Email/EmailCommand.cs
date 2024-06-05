using System;
using System.Collections.Generic;
using Domain.Core.Commands;

namespace Domain.Commands
{
  public abstract class EmailCommand : Command
  {
    public string CustomerEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
  }
}
