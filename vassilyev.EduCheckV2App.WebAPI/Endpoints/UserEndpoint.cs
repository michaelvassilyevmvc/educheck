using System.Net;
using AutoMapper;
using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using vassilyev.EduCheckV2App.WebAPI.Dto;
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
        
        // GET by ID
        app.MapGet("/{id:guid}", GetById)
            .WithName("GetById")
            .Produces<User>(201);
        
        // POST
        app.MapPost("/", CreateUser)
            .WithName("CreateUser")
            .Produces<APIResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status409Conflict);
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
    
    private static async Task<IResult> GetById(IRepository<User> _repo, ILogger<Program> _logger, Guid id)
    {
        APIResponse response = new();
        _logger.LogInformation($"Get user {id}");
        response.Result = await _repo.GetAsync(id);
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
    
    private static async Task<IResult> CreateUser(IRepository<User> _repo, IMapper _mapper,
        IValidator<UserCreateDto> _validation,
        [FromBody] UserCreateDto userCreateDto)
    {
        APIResponse response = new()
        {
            IsSuccess = false,
            StatusCode = HttpStatusCode.BadRequest,
        };


        var validationResult = await _validation.ValidateAsync(userCreateDto);
        if (!validationResult.IsValid)
        {
            response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault()
                .ToString());
            return Results.BadRequest(response);
        }

        if (await (_repo as UserRepository).GetAsync(userCreateDto.Login) is not null)
        {
            response.ErrorMessages.Add("A coupon with the same name already exists");
            return Results.Conflict(response);
        }

        // Создание пользователя
        var user = _mapper.Map<User>(userCreateDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);

        await _repo.CreateAsync(user);
        await _repo.SaveAsync();
        UserDto userDto = _mapper.Map<UserDto>(user);

        response.Result = userDto;
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.Created;
        return Results.Ok(response);
    }
}