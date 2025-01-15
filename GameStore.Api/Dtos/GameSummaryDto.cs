namespace GameStore.Api.Dtos;

public record class GameSummaryDto(
  int Id, 
  string Name,
  String Genre,
  decimal Price, 
  DateOnly ReleaseDate)
{

}
