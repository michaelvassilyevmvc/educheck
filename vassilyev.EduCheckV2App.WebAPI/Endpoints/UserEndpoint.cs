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
            .Produces<APIResponse>(201);

        // GET by ID
        app.MapGet("/{id:guid}", GetById)
            .WithName("GetById")
            .Produces<APIResponse>(201);

        // POST
        app.MapPost("/", CreateUser)
            .WithName("CreateUser")
            .Produces<APIResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status409Conflict);
        
        // PUT
        app.MapPut("/{id:guid}", UpdateUser)
            .WithName("UpdateUser")
            .Accepts<UserUpdateDto>("application/json")
            .ProducesValidationProblem()
            .Produces<APIResponse>(StatusCodes.Status400BadRequest)
            .Produces<APIResponse>(StatusCodes.Status404NotFound)
            .Produces<APIResponse>(StatusCodes.Status409Conflict);

        // DELETE
        app.MapDelete("/{id:guid}", DeleteUser)
            .WithName("DeleteUser")
            .ProducesValidationProblem()
            .Produces<APIResponse>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized);
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

    private static async Task<IResult> DeleteUser(IRepository<User> _repo, ILogger<Program> _logger,
        IValidator<UserDeleteDto> _validation, [FromBody] UserDeleteDto userDeleteDto, Guid Id)
    {
        APIResponse response = new()
        {
            IsSuccess = false,
            StatusCode = HttpStatusCode.BadRequest,
        };
        
        var validationResult = await _validation.ValidateAsync(userDeleteDto);
        if (!validationResult.IsValid)
        {
            response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault()
                .ToString());
            return Results.BadRequest(response);
        }
        
        var user = await _repo.GetAsync(Id);
        if (user is null)
        {
            return Results.NotFound(new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessages = { $"User with ID {Id} not found" }
            });
        }
        
        if (!BCrypt.Net.BCrypt.Verify(userDeleteDto.VerificationPassword, user.PasswordHash))
        {
            return Results.Unauthorized();
        }

        _repo.Remove(user);
        await _repo.SaveAsync();

        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
    
    private static async Task<IResult> UpdateUser(IRepository<User> _repo, IMapper _mapper,
        IValidator<UserUpdateDto> _validation,
        [FromBody] UserUpdateDto userUpdateDto, Guid Id)
    {
        APIResponse response = new()
        {
            IsSuccess = false,
            StatusCode = HttpStatusCode.BadRequest,
        };
        
        var validationResult = await _validation.ValidateAsync(userUpdateDto);
        if (!validationResult.IsValid)
        {
            response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault()
                .ToString());
            return Results.BadRequest(response);
        }
        // Проверка существования пользователя
        var existingUser = await _repo.GetAsync(Id);
        if (existingUser == null)
        {
            return Results.NotFound(new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessages = { $"User with ID {Id} not found" }
            });
        }
        // Проверка уникальности нового логина (если изменяется)
        if (!string.IsNullOrEmpty(userUpdateDto.NewLogin) && 
            userUpdateDto.NewLogin != existingUser.Login)
        {
            var loginExists = await (_repo as UserRepository).GetAsync(userUpdateDto.NewLogin) != null;
            if (loginExists)
            {
                return Results.Conflict(new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.Conflict,
                    ErrorMessages = { $"Login '{userUpdateDto.NewLogin}' already taken" }
                });
            }
        }
        
        // Применение изменений
        if (!string.IsNullOrEmpty(userUpdateDto.NewLogin))
            existingUser.Login = userUpdateDto.NewLogin;

        if (!string.IsNullOrEmpty(userUpdateDto.NewPassword))
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.NewPassword);

        // Сохранение изменений
        _repo.Update(existingUser);
        await _repo.SaveAsync();
        
        // Возврат результата
        return Results.Ok(new APIResponse
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Result = _mapper.Map<UserDto>(existingUser)
        });
    }
}