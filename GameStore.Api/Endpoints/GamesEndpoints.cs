using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entites;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGameBy";

    public static RouteGroupBuilder MapGamesEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        //GET all games//
        group.MapGet("/", async (GameStoreContext dbContext) =>
        
          await dbContext.Games.Include(game => game.Genre).Select(game => game.ToGameSummaryDto()).AsNoTracking().ToListAsync());

        // GET game by id//
        group.MapGet("/{id}",async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToDetailsDto());


        }).WithName(GetGameEndpointName);

        //Resources games/{} proper way//
        //POST//
    group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();         

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

          
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToDetailsDto());


        });

        //POSTgames/id//
        group.MapPut("/{id}", async (int id, UpdateGameDto updateGame, GameStoreContext dbContext) =>

        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
           dbContext.Entry(existingGame).CurrentValues.SetValues(updateGame.ToEntity(id));
           await dbContext.SaveChangesAsync();

            return Results.NoContent();
          
           
        });

        //DELETE//
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        group.MapGet("/search/{search}", async (string search, GameStoreContext dbContext) =>{
            var games = await dbContext.Games //DbContext connects to game table
            .Include(game => game.Genre) //Include the genre
            .Where(game => game.Name.Contains(search))
            .AsNoTracking()
            .ToListAsync();

            //Map the filtered games to GameDto
            //Game? game = await dbContext.Games.FindAsync(id);

           if (!games.Any())
    {
        return Results.NotFound("No games found matching the search criteria.");
    }

    // Map the games to GameDto using the constructor
            var gameDtos = games.Select(game => new GameDto(game)).ToList();

    // Return the DTOs
            return Results.Ok(gameDtos);
           


        });


        return group;

    }
}
