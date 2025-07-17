using System.Net;
using Carter;
using vassilyev.EduCheckV2App.WebAPI.Entities;
using vassilyev.EduCheckV2App.WebAPI.Helpers;
using vassilyev.EduCheckV2App.WebAPI.Repository;

namespace vassilyev.EduCheckV2App.WebAPI.Endpoints;

public class UserEndpoint : CarterModule
{
    public UserEndpoint() : base("api/users")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        // GET
        app.MapGet("/", GetAll)
            .WithName("GetUsers")
            .Produces<User>(201);
    }

    private static async Task<IResult> GetAll(IRepository<User> _repo, ILogger<Program> _logger)
    {
        APIResponse response = new();
        _logger?.LogInformation("Get all users");
        response.IsSuccess = true;
        response.Result = await _repo.GetAllAsync();
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
}