using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
string? connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);
WebApplication app = builder.Build();

app.MapGamesEndpoint();
app.MapGenresEndpoints();
await app.MigrateDbAsync();

app.Run();
