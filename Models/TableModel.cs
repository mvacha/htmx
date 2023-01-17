using Microsoft.AspNetCore.Mvc;

namespace Explorations.Models;

public class TableModel<T>
{
    [BindProperty(SupportsGet = true)]
    public string OrderBy { get; set; } = "Id";

    [BindProperty(SupportsGet = true)]
    public bool OrderDescending { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Skip { get; set; }

    [BindProperty(SupportsGet = true)]
    public int PageSize { get; set; } = 10;

    public bool HasMoreData { get; set; }

    public IList<T> Data { get; set; } = new List<T>();
}
