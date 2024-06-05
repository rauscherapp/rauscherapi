using System.Collections.Generic;

namespace Domain.QueryParameters
{
  public class AppEmailParameters
  {
    public string CustomerEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<string> Attachments { get; set; }
  }
}
