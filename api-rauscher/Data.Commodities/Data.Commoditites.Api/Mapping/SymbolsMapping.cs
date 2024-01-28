using Domain.Models;

namespace Data.Commodities.Api.Mapping
{
  public static class SymbolsMapper
  {
    public static IEnumerable<Symbols> AsDomainModel(this Dictionary<string, string> models)
    {
      return models.Select(model => model.AsDomainModel());
    }

    public static Symbols AsDomainModel(this KeyValuePair<string, string> model)
    {
      return new Symbols(model.Key, model.Value, false, string.Empty);
    }
  }
}
