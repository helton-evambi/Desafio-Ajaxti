using BookCatalog.Application.ViewModel;
using BookCatalog.Domain.Abstractions;
using BookCatalog.Domain.Entities;
using Mapster;

namespace BookCatalog.Application.Mappings;

public static class MapsterConfig
{
    public static void Configure()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        // Configurações globais
        config.Default
            .IgnoreNullValues(true)
            .PreserveReference(true)
            .NameMatchingStrategy(NameMatchingStrategy.Flexible)
            .MaxDepth(2);

        // Book → BookViewModel
        config.NewConfig<Book, BookViewModel>()
            .Map(dest => dest.AuthorName,
                src => src.Author != null ? $"{src.Author.FirstName} {src.Author.LastName}" : string.Empty)
            .Map(dest => dest.GenreName,
                src => src.Genre != null ? src.Genre.Name : string.Empty)
            .IgnoreIf((src, dest) => src.Genre == null, dest => dest.GenreName);

        // Author → AuthorViewModel
        config.NewConfig<Author, AuthorViewModel>()
            .Map(dest => dest.BookTitles, src => src.Books.Adapt<List<BookViewModel>>());

        // Genre → GenreViewModel
        config.NewConfig<Genre, GenreViewModel>();

        // Configuração genérica para PagedResult
        ConfigurePagedResultMappings(config);
    }

    private static void ConfigurePagedResultMappings(TypeAdapterConfig config)
    {
        // Método auxiliar para registrar mapeamentos genéricos
        var method = typeof(MapsterConfig).GetMethod(nameof(CreatePagedResultMapping),
                   System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        if (method != null)
        {
            method.MakeGenericMethod(typeof(Author), typeof(AuthorViewModel)).Invoke(null, new object[] { config });
            method.MakeGenericMethod(typeof(Book), typeof(BookViewModel)).Invoke(null, new object[] { config });
            method.MakeGenericMethod(typeof(Genre), typeof(GenreViewModel)).Invoke(null, new object[] { config });
        }
    }

    private static void CreatePagedResultMapping<TSource, TDestination>(TypeAdapterConfig config)
    {
        config.NewConfig<PagedResult<TSource>, PagedResult<TDestination>>()
            .ConstructUsing(src => new PagedResult<TDestination>
            {
                Items = src.Items.Adapt<List<TDestination>>() ?? new List<TDestination>(),
                TotalCount = src.TotalCount,
                Page = src.Page,
                PageSize = src.PageSize
            });
    }
}