using MyApi.GraphQL;
using MyApi.infrastructure;
using Microsoft.EntityFrameworkCore;
using MyApi.Repository;
using MyApi.Services;
using MyApi.Application.UseCases.CreateUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseUrl")));

//Hot chocolate configuration to show real errors in the GraphQL response
builder.Services
    .AddGraphQLServer()
    .ModifyRequestOptions(opt =>
    {
        opt.IncludeExceptionDetails = true;
    });

// Register GraphQL services and types
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();



var app = builder.Build();

app.MapGraphQL();

app.Run();
