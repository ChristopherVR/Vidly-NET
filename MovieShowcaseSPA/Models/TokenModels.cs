namespace MovieShowcaseSPA.Models;

public record User(int Id, string Username, string Name, string Surname, string? Role = default);
