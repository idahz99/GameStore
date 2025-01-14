using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGameBy";
    private static readonly List<GameDto> games = [
        new GameDto(1, "Super Mario Bros.", "Platformer", 29.99m, new DateOnly(1985, 9, 13)),
    new GameDto(2, "The Legend of Zelda", "Action-adventure", 49.99m, new DateOnly(1986, 2, 21)),
    new GameDto(3, "Minecraft", "Sandbox", 19.99m, new DateOnly(2011, 11, 18))
    ];

    public static RouteGroupBuilder MapGamesEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("games");
        //GET all games//
        group.MapGet("/", () => games);

        // GET game by id//
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);


        }).WithName(GetGameEndpointName);

        //Resources games/{} proper way//
        //POST//
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
                );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);


        });

        //PUT/games/id//
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>

        {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }
            else
            {
                games[index] = new GameDto(
            id,
            updateGame.Name,
            updateGame.Genre,
            updateGame.Price,
            updateGame.ReleaseDate
            );
                return Results.NoContent();
            }
        });

        //DELETE//
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });


        return group;

    }
}
