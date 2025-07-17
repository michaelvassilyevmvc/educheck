using System.Net;
using Carter;
using vassilyev.EduCheckV2App.WebAPI.Entities;
using vassilyev.EduCheckV2App.WebAPI.Helpers;
using vassilyev.EduCheckV2App.WebAPI.Repository;

namespace vassilyev.EduCheckV2App.WebAPI.Endpoints;

public class AnswerOptionEndpoint : CarterModule
{
    public AnswerOptionEndpoint() : base("api/answeroptions")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        // GET
        app.MapGet("/", GetAll)
            .WithName("GetAnswerOptions")
            .Produces<AnswerOption>(201);
    }

    private static async Task<IResult> GetAll(IRepository<AnswerOption> _repo, ILogger<Program> _logger)
    {
        APIResponse response = new();
        _logger?.LogInformation("Get all answerOptions");
        response.IsSuccess = true;
        response.Result = await _repo.GetAllAsync();
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
}