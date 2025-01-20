using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtension
{
public static async Task MigrateDbAsync(this WebApplication app){
using IServiceScope scope = app.Services.CreateScope();
GameStoreContext dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
await dbContext.Database.MigrateAsync();
}
}
