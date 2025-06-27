using Domain.Enum;

namespace Infrastructure.Commodities.Adapters
{
    public abstract class BaseAdapter
    {
        public string GetVendorAdapter() => VendorEnum.Commodity.Name;
    }
}
