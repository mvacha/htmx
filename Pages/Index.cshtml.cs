using Explorations.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace Explorations.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PlantsService _plantsService;

        [BindProperty(SupportsGet = true)]
        public TableModel<Plant> Table { get; set; } = new TableModel<Plant>();

        public IList<SelectListItem> Varieties { get; init; }

        public Plant? PlantsToEdit { get; set; }

        public IndexModel(PlantsService plantsService)
        {
            _plantsService = plantsService;
            Varieties = _plantsService.GetPlantVarieties()
                .Select((v, i) => new SelectListItem() { Text = v, Value = i.ToString() })
                .ToList();
        }

        public void OnGet()
        {
            IEnumerable<Plant> Plants = _plantsService.GetPlants();

            //Sorting
            if (Table.OrderBy == nameof(Plant.Name))
                Plants = Table.OrderDescending ? Plants.OrderByDescending(f => f.Name) : Plants.OrderBy(f => f.Name);
            else if (Table.OrderBy == nameof(Plant.Variety))
                Plants = Table.OrderDescending ? Plants.OrderByDescending(f => f.Variety) : Plants.OrderBy(f => f.Variety);
            else if (Table.OrderBy == nameof(Plant.Kind))
                Plants = Table.OrderDescending ? Plants.OrderByDescending(f => f.Kind) : Plants.OrderBy(f => f.Kind);
            else if (Table.OrderBy == nameof(Plant.PurchasedDate))
                Plants = Table.OrderDescending ? Plants.OrderByDescending(f => f.PurchasedDate) : Plants.OrderBy(f => f.PurchasedDate);
            else
                Plants = Table.OrderDescending ? Plants.OrderByDescending(f => f.Id) : Plants.OrderBy(f => f.Id);

            //Search
            if (!Table.Search.IsNullOrEmpty())
                Plants = Plants.Where(f => f.Name.Contains(Table.Search, StringComparison.InvariantCultureIgnoreCase) || f.Variety.Contains(Table.Search) || f.Kind.ToString().Contains(Table.Search));

            //Pagination
            Table.Data = Plants.Skip(Table.Skip).Take(Table.PageSize).ToList();
            Table.HasMoreData = Plants.Skip(Table.Skip + Table.PageSize).Any();

            if (Request.Query.ContainsKey("Message"))
                ViewData["SuccessMessage"] = Request.Query["Message"].ToString();
        }
    }
}