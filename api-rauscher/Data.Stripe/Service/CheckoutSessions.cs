using System;
using System.Collections.Generic;

namespace StripeApi.Model
{
  /// <summary>
  /// Represents a Stripe Checkout Session.
  /// </summary>
  public class CheckoutSession
  {
    /// <summary>
    /// Unique identifier for the session.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The object type (checkout.session).
    /// </summary>
    public string Object { get; set; }

    /// <summary>
    /// A unique string to reference the Checkout Session. This can be a customer ID, a cart ID, or similar, and can be used to reconcile the session with your internal systems.
    /// </summary>
    public string ClientReferenceId { get; set; }

    /// <summary>
    /// ID of an existing Customer, if one exists.
    /// </summary>
    public string Customer { get; set; }

    /// <summary>
    /// The customer’s email address. It’s displayed alongside the customer in your dashboard and can be useful for searching and tracking.
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// A list of items the customer is purchasing.
    /// </summary>
    public List<LineItem> LineItems { get; set; }

    /// <summary>
    /// Set of key-value pairs that you can attach to an object. This can be useful for storing additional information about the object in a structured format.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; }

    /// <summary>
    /// The mode of the Checkout Session. Can be 'payment', 'setup', or 'subscription'.
    /// </summary>
    public string Mode { get; set; }

    /// <summary>
    /// The URL the customer will be directed to after the payment or setup flow is complete.
    /// </summary>
    public string SuccessUrl { get; set; }

    /// <summary>
    /// The URL the customer will be directed to if they decide to cancel payment and return to your website.
    /// </summary>
    public string CancelUrl { get; set; }

    /// <summary>
    /// Time at which the object was created. Measured in seconds since the Unix epoch.
    /// </summary>
    public long Created { get; set; }

    /// <summary>
    /// Three-letter ISO currency code.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Indicates whether the object exists in live mode or test mode.
    /// </summary>
    public bool Livemode { get; set; }

    /// <summary>
    /// The payment status of the Checkout Session.
    /// </summary>
    public string PaymentStatus { get; set; }

    /// <summary>
    /// The URL to the Checkout Session.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Total amount of the Checkout Session, including discounts, shipping, and taxes.
    /// </summary>
    public TotalDetails TotalDetails { get; set; }

    /// <summary>
    /// Amount subtotal before discounts and taxes.
    /// </summary>
    public int AmountSubtotal { get; set; }

    /// <summary>
    /// Total amount due, including discounts, shipping, and taxes.
    /// </summary>
    public int AmountTotal { get; set; }

    /// <summary>
    /// Automatic tax details.
    /// </summary>
    public AutomaticTax AutomaticTax { get; set; }

    /// <summary>
    /// The status of the Checkout Session.
    /// </summary>
    public string Status { get; set; }
  }

  /// <summary>
  /// Represents a line item in a Checkout Session.
  /// </summary>
  public class LineItem
  {
    public string Id { get; set; }
    public string Object { get; set; }
    public int AmountSubtotal { get; set; }
    public int AmountTax { get; set; }
    public int AmountTotal { get; set; }
    public string Currency { get; set; }
    public string Description { get; set; }
    public Price Price { get; set; }
    public int Quantity { get; set; }
  }

  /// <summary>
  /// Represents the price details of a line item.
  /// </summary>
  public class Price
  {
    public string Id { get; set; }
    public string Object { get; set; }
    public bool Active { get; set; }
    public string BillingScheme { get; set; }
    public long Created { get; set; }
    public string Currency { get; set; }
    public long UnitAmount { get; set; }
    public string UnitAmountDecimal { get; set; }
  }

  /// <summary>
  /// Represents the total details for discounts, shipping, and taxes.
  /// </summary>
  public class TotalDetails
  {
    public int AmountDiscount { get; set; }
    public int AmountShipping { get; set; }
    public int AmountTax { get; set; }
  }

  /// <summary>
  /// Represents the automatic tax details.
  /// </summary>
  public class AutomaticTax
  {
    public bool Enabled { get; set; }
    public object Liability { get; set; }
    public object Status { get; set; }
  }

  /// <summary>
  /// Represents the mode of a Stripe Checkout Session.
  /// </summary>
  public enum CheckoutSessionMode
  {
    /// <summary>
    /// Accept one-time payments for cards, iDEAL, and more.
    /// </summary>
    Payment,

    /// <summary>
    /// Save payment details to charge your customers later.
    /// </summary>
    Setup,

    /// <summary>
    /// Use Stripe Billing to set up fixed-price subscriptions.
    /// </summary>
    Subscription
  }
}
