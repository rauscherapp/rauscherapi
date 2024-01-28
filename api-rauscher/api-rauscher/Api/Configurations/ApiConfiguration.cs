using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static class ApiConfiguration
    {
        public static void AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers(setupAction =>
            {
                setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                setupAction.Filters.Add(new ProducesDefaultResponseTypeAttribute());
                setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
                setupAction.Filters.Add(new AuthorizeFilter());

                setupAction.ReturnHttpNotAcceptable = true;
            })
                .AddXmlDataContractSerializerFormatters()
                 .ConfigureApiBehaviorOptions(setupAction =>
                 {
                     setupAction.InvalidModelStateResponseFactory = context =>
                     {
                         var problemDetailsFactory = context.HttpContext.RequestServices
                             .GetRequiredService<ProblemDetailsFactory>();
                         var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                                 context.HttpContext,
                                 context.ModelState);

                         problemDetails.Detail = "See the errors field for details.";
                         problemDetails.Instance = context.HttpContext.Request.Path;

                         var actionExecutingContext = context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                         if ((context.ModelState.ErrorCount > 0) &&
                             (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                         {
                             problemDetails.Type = "https://sistockler/modelvalidationproblem";
                             problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                             problemDetails.Title = "One or more validation errors occurred.";

                             return new UnprocessableEntityObjectResult(problemDetails)
                             {
                                 ContentTypes = { "application/problem+json" }
                             };
                         }

                         problemDetails.Status = StatusCodes.Status400BadRequest;
                         problemDetails.Title = "One or more errors on input occurred.";
                         return new BadRequestObjectResult(problemDetails)
                         {
                             ContentTypes = { "application/problem+json" }
                         };
                     };
                 });
        }
    }
}
