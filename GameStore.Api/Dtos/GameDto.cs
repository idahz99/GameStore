using GameStore.Api.Entites;

namespace GameStore.Api.Dtos;

public record class GameDto
{
public int Id { get; set; }

public string Name { get; set; }

public int GenreId { get; set; }

public string Genre { get; set; } = null!;   

public decimal Price { get; set; }

public string PriceString { get; set; } = null!;

public decimal Tax { get; set; }

public GameDto(int id, string name, int genreId, string genre, decimal price)
{
    Id = id;
    Name = name;
    GenreId = genreId;
    Genre = genre;
    Price = price;
    Tax = price * 0.8m;
    PriceString = string.Format("{0:C}", Price);
}
  //This is the mapping usually done in the mapping class
public GameDto(Game game)
{
    Id = game.Id;
    Name = game.Name;
    GenreId = game.GenreId;
    Genre = game.Genre.Name;
    Price = game.Price;
    Tax = game.Price * 0.8m;
    PriceString = string.Format("{0:C}", Price);
}

}