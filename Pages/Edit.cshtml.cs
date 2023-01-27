using Explorations.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorations.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Plant? Item { get; set; }

        public bool IsEdit { get; private set; }

        public IList<SelectListItem> Varieties { get; set; }

        private readonly PlantsService _plantsService;

        public EditModel(PlantsService plantsService)
        {
            _plantsService = plantsService;

            Varieties = _plantsService.GetPlantVarieties()
                .Select((v, i) => new SelectListItem() { Text = v, Value = v })
                .ToList();
        }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Item = _plantsService.GetPlant(id.Value);
                Varieties.First(v => v.Text.Equals(Item.Variety)).Selected = true;
                IsEdit = true;
            }

            return Partial("_EditModal", this);
        }

        public IActionResult OnPost(int? id)
        {
            if (id is null)
            {
                //New plant
                var newPlant = Item! with { Id = _plantsService.GetPlants().Last().Id + 1 };
                _plantsService.AddPlant(newPlant);

                return RedirectToPage(nameof(Index), new { Message = $"Added plant {newPlant.Name}." });
            }
            else
            {
                _plantsService.UpdatePlant(Item with { Id = id.Value });

                return RedirectToPage(nameof(Index), new { Message = $"Plant {Item!.Name} updated!" });
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            var plant = _plantsService.GetPlant(id);

            _plantsService.DeletePlant(id);

            return RedirectToPage(nameof(Index), new { Message = $"Deleted plant {plant.Name}." });
        }

        public IActionResult OnGetDelete(int id) => Partial("_DeleteModal", _plantsService.GetPlant(id));
    }
}
