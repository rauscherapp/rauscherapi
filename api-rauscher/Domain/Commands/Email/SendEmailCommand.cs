using Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands
{
  public class SendEmailCommand : EmailCommand
  {
    public override bool IsValid()
    {
      return true;
    }
  }
}
