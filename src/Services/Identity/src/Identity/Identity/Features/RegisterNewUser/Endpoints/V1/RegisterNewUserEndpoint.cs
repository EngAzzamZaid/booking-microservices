using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Web;
using Identity.Identity.Dtos;
using Identity.Identity.Features.RegisterNewUser.Commands.V1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.Identity.Features.RegisterNewUser.Endpoints.V1;

public class RegisterNewUserEndpoint : IMinimalEndpoint
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{EndpointConfig.BaseApiPath}/identity/register-user", RegisterNewUser)
            .WithTags("Identity")
            .WithName("Register User")
            .WithMetadata(new SwaggerOperationAttribute("Register User", "Register User"))
            .WithApiVersionSet(endpoints.NewApiVersionSet("Identity").Build())
            .Produces<RegisterNewUserResponseDto>()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .HasApiVersion(1.0);

        return endpoints;
    }

    private async Task<IResult> RegisterNewUser(RegisterNewUserCommand command, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Results.Ok(result);
    }
}