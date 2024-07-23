using Data.Context;
using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
  public class AboutUsRepository : Repository<AboutUs>, IAboutUsRepository
  {
    public AboutUsRepository(RauscherDbContext context) : base(context)
    {
    }
    public AboutUs ObterAboutUs()
    {
      var Apicredentials = Db.AboutUs;

      return Apicredentials.FirstOrDefault();
    }
  }
}
