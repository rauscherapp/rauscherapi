using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
  public class AboutUs
  {
    public AboutUs(Guid id, string description)
    {
      Id = id;
      Description = description;      
    }

    public Guid Id { get; set; }
    public string Description { get; set; }
  }
}
