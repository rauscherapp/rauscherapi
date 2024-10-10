using Domain.Enum;

namespace Infrastructure.YahooFinance
{
  public abstract class BaseAdapter
  {
    public string GetVendorAdapter() => VendorEnum.Yahoo.Name;
  }
}
