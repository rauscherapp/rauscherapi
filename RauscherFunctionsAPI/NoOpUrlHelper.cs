using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

public class NoOpUrlHelper : IUrlHelper
{
  public ActionContext ActionContext => null;

  public string Action(UrlActionContext actionContext) => string.Empty;
  public string Content(string contentPath) => string.Empty;
  public bool IsLocalUrl(string url) => false;
  public string Link(string routeName, object values) => string.Empty;
  public string RouteUrl(UrlRouteContext routeContext) => string.Empty;
}
