using System;
using GameStore.Api.Dtos;
using GameStore.Api.Entites;

namespace GameStore.Api.Mapping;

public static class GenreMapping
{
public static GenreDto ToDto(this Genre genre){
    return new GenreDto(genre.Id, genre.Name);
}
}
