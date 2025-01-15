using System;
using GameStore.Api.Data;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GenresEndpoint
{
public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
{

var group = app.MapGroup("genres");

group.MapGet("/", async (GameStoreContext dbContext) =>
    await dbContext.Genrs
                    .Select(genre => genre.ToDto())
                    .AsNoTracking()
                    .ToListAsync());
    return group;


}

}

