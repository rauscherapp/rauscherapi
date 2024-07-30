using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
  public interface IAboutUsAppService
  {
    Task<AboutUsViewModel> AtualizarAboutUs(AboutUsViewModel aboutUsViewModel);
    Task<AboutUsViewModel> ObterAboutUs();
  }
}
