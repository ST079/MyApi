using MyApi.GraphQL;
using MyApi.infrastructure;
using Microsoft.EntityFrameworkCore;
using MyApi.Repository;
using MyApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseUrl")));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();
// app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();
