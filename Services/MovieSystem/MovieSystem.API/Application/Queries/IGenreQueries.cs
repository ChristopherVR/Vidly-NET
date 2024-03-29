﻿namespace MovieSystem.API.Application.Queries;

public interface IGenreQueries
{
    Task<List<GenrePreview>> ListGenresAsync(string? searchTerm);
    Task<string?> GetGenreNameAsync(int id);
}

