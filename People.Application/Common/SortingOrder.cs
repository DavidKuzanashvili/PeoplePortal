namespace People.Application.Common;

public enum SortingOrder
{
    Ascending,
    Descending,
}

public static class SortingOrderHelpers
{
    public static SortingOrder GetSorting(this string sorting) =>
        sorting switch
        {
            "asc" => SortingOrder.Ascending,
            "desc" => SortingOrder.Descending,
            _ => throw new ArgumentException("Unknown sorting parameter", nameof(sorting))
        };
}
