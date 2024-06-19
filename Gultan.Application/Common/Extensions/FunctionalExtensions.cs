namespace Gultan.Application.Common.Extensions;

public static class FunctionalExtensions
{
    public static IQueryable<TSource> When<TSource>(this IQueryable<TSource> source, Func<bool> when,
        Func<IQueryable<TSource>, IQueryable<TSource>> then)
    {
        return when() ? then(source) : source;
    }

    public static IQueryable<TSource> When<TSource>(this IQueryable<TSource> source, bool when,
        Func<IQueryable<TSource>, IQueryable<TSource>> then)
    {
        return when ? then(source) : source;
    }
}