using System;

namespace GameStore.Api.Entites;

public class Game
{
public int Id { get; set; }

public required string Name { get; set; } //required is a new feature in C# 10

public int GenreId { get; set; }

public Genre? Genre { get; set; }    

public decimal Price { get; set; }

public DateOnly ReleaseDate { get; set; }
}
