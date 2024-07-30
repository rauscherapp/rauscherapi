//using Domain.Models;
//using Domain.Queries;
//using Domain.QueryParameters;
//using Domain.Repositories;
//using MediatR;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Domain.QueryHandlers
//{
//  public class GetPtaxRateQueryHandler : IRequestHandler<GetPtaxRateQuery, PagedList<CommoditiesRate>>
//  {
//    private readonly ILogger<GetPtaxRateQueryHandler> _logger;
//    private readonly IBancoCentralRepository _bancoCentralRepository;
//    public GetPtaxRateQueryHandler(ILogger<GetPtaxRateQueryHandler> logger, IBancoCentralRepository bancoCentralRepository)
//    {
//      _logger = logger;
//      _bancoCentralRepository = bancoCentralRepository;
//    }
//    public async Task<PagedList<CommoditiesRate>> Handle(GetPtaxRateQuery request, CancellationToken cancellationToken)
//    {
//      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request);

//      return await _bancoCentralRepository.GetExchangeRateAsync(DateTime.Now.ToString("dd-MM-yyyy"));

//    }
//  }
//}
