namespace StripeApi.Models
{
  public class Customer
  {
    public string Id { get; set; }
    public string Object { get; set; } = "customer";
    public string Email { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Description { get; set; }
    public long Created { get; set; }
    public bool Delinquent { get; set; }
    public int Balance { get; set; }
    public string Currency { get; set; }
    public string DefaultSource { get; set; }
    public string InvoicePrefix { get; set; }
    public InvoiceSettings InvoiceSettings { get; set; }
    public bool Livemode { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
    public int NextInvoiceSequence { get; set; }
    public List<string> PreferredLocales { get; set; }
    public Shipping Shipping { get; set; }
    public string TaxExempt { get; set; }
    public string TestClock { get; set; }
    public Discount Discount { get; set; }
  }

  public class InvoiceSettings
  {
    public List<CustomField> CustomFields { get; set; }
    public string DefaultPaymentMethod { get; set; }
    public string Footer { get; set; }
    public RenderingOptions RenderingOptions { get; set; }
  }

  public class CustomField
  {
    public string Name { get; set; }
    public string Value { get; set; }
  }

  public class RenderingOptions
  {
    // Add relevant properties as needed
  }

  public class Shipping
  {
    public Address Address { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
  }

  public class Address
  {
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
  }

  public class Discount
  {
    public string Coupon { get; set; }
    public int PercentOff { get; set; }
    public DateTime? End { get; set; }
  }
}