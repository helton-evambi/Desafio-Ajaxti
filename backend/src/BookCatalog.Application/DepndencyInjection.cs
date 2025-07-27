using BookCatalog.Application.Interfaces;
using BookCatalog.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookCatalog.Application;

public static class DepndencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services)
    {
        // validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //services
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IGenreSerivce, GenreService>();

        return services;
    }
}