using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
  public interface IAboutUsRepository : IRepository<AboutUs>
  {
    AboutUs ObterAboutUs();
  }
}
