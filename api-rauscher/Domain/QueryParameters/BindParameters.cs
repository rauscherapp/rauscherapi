using System;
using System.Collections.Generic;

namespace Domain.QueryParameters
{
  public class BindParameters
  {
    public T BindQueryParameters<T>(IDictionary<string, string> queryParameters) where T : QueryParameters, new()
    {
      var parameters = new T();

      BindQueryParametersBase(parameters, queryParameters);

      if (parameters is SymbolsParameters symbolsParameters)
      {
        BindSymbolsParameters(symbolsParameters, queryParameters);
      }

      return parameters;
    }

    private void BindQueryParametersBase(QueryParameters parameters, IDictionary<string, string> queryParameters)
    {
      if (queryParameters.TryGetValue("pageNumber", out var pageNumberString) && int.TryParse(pageNumberString, out var pageNumber))
      {
        parameters.PageNumber = pageNumber;
      }

      if (queryParameters.TryGetValue("pageSize", out var pageSizeString) && int.TryParse(pageSizeString, out var pageSize))
      {
        parameters.PageSize = pageSize;
      }

      if (queryParameters.TryGetValue("searchQuery", out var searchQuery))
      {
        parameters.SearchQuery = searchQuery;
      }

      if (queryParameters.TryGetValue("orderBy", out var orderBy))
      {
        parameters.OrderBy = orderBy;
      }

      if (queryParameters.TryGetValue("fields", out var fields))
      {
        parameters.Fields = fields;
      }
    }

    private void BindSymbolsParameters(SymbolsParameters parameters, IDictionary<string, string> queryParameters)
    {
      if (queryParameters.TryGetValue("id", out var idString) && Guid.TryParse(idString, out var id))
      {
        parameters.Id = id;
      }

      if (queryParameters.TryGetValue("code", out var code))
      {
        parameters.Code = code;
      }

      if (queryParameters.TryGetValue("name", out var name))
      {
        parameters.Name = name;
      }

      if (queryParameters.TryGetValue("symbolType", out var symbolType))
      {
        parameters.SymbolType = symbolType;
      }

      if (queryParameters.TryGetValue("appvisible", out var appVisibleString) && bool.TryParse(appVisibleString, out var appVisible))
      {
        parameters.Appvisible = appVisible;
      }
    }
  }
}
