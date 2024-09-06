namespace Domain.Enum
{
  public class VendorEnum : Enumeration
  {
    public static readonly VendorEnum Commodity = new VendorEnum(0, "Commodity");
    public static readonly VendorEnum Bacen = new VendorEnum(1, "Bacen");
    public static readonly VendorEnum Yahoo = new VendorEnum(1, "YahooFinance");

    private VendorEnum(int value, string name) : base(value, name)
    {
    }

    public VendorEnum()
    {
    }
  }
}
