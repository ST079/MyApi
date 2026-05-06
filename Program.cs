using MyApi.GraphQL;
using MyApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MyApi.Infrastructure.Repository;
using MyApi.Services;
using MyApi.Application.UseCases.CreateUser;
using FluentValidation.AspNetCore;
using FluentValidation;
using MyApi.Application.UseCases.UpdateUser;
using MyApi.Application.UseCase.Login;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<UpdateUserUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseUrl")));

//Hot chocolate configuration to show real errors in the GraphQL response
builder.Services
    .AddGraphQLServer()
    .ModifyRequestOptions(opt =>
    {
        opt.IncludeExceptionDetails = true;
    });

// FluentValidation 
builder.Services.AddFluentValidationAutoValidation();
// Scan all validators automatically
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Register GraphQL services and types
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();



var app = builder.Build();

app.MapGraphQL();

app.Run();
