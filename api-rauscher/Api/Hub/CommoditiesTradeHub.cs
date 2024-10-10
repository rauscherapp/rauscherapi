using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Helpers;
using Application.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace Api
{
  public class CommoditiesTradeHub : Hub
  {
    public async Task SendCommodities(IEnumerable<SymbolsViewModel> message)
    {
      try
      {
        // Logar ou processar a mensagem conforme necessário
        await Clients.All.SendAsync("ReceiveCommodities", message);
      }
      catch (Exception ex)
      {
        // Logar o erro para diagnóstico
        Console.WriteLine($"Error in SendMessage: {ex.Message}");
        throw;
      }
    }

    public async Task SendExchanges(IEnumerable<SymbolsViewModel> message)
    {
      try
      {
        // Logar ou processar a mensagem conforme necessário
        await Clients.All.SendAsync("ReceiveExchanges", message);
      }
      catch (Exception ex)
      {
        // Logar o erro para diagnóstico
        Console.WriteLine($"Error in SendMessage: {ex.Message}");
        throw;
      }
    }
  }
}
