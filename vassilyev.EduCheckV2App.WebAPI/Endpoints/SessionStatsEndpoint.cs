using System.Net;
using Carter;
using vassilyev.EduCheckV2App.WebAPI.Entities;
using vassilyev.EduCheckV2App.WebAPI.Helpers;
using vassilyev.EduCheckV2App.WebAPI.Repository;

namespace vassilyev.EduCheckV2App.WebAPI.Endpoints;

public class SessionStatsEndpoint : CarterModule
{
    public SessionStatsEndpoint() : base("api/sessionStats")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        // GET
        app.MapGet("/", GetAll)
            .WithName("GetSessionStats")
            .Produces<SessionStats>(201);
    }

    private static async Task<IResult> GetAll(IRepository<SessionStats> _repo, ILogger<Program> _logger)
    {
        APIResponse response = new();
        _logger?.LogInformation("Get all sessionStats");
        response.IsSuccess = true;
        response.Result = await _repo.GetAllAsync();
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
}