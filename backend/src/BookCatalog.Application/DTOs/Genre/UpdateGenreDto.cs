﻿namespace BookCatalog.Application.DTOs.Genre;

public record UpdateGenreDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
