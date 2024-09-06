using Domain.Enum;

namespace Infrastructure.BancoCentral
{
    public abstract class BaseAdapter
    {
        public string GetVendorAdapter() => VendorEnum.Bacen.Name;
    }
}
