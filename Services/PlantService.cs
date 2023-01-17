using Bogus;

namespace Explorations.Services;

public class PlantsService
{
    private readonly List<Plant> _plants;
    private readonly List<string> _varieties;

    public PlantsService()
    {
        _varieties = new()
        {
            "Agave",
            "Alocasia",
            "Calathea",
            "Dracaena",
            "Ficus",
            "Jasminum",
            "Dracaena",
            "Monstera",
            "Nolina",
        };

        Randomizer.Seed = new Random(345678);
        int id = 1;
        var locations = new[] { "V rohu", "Na poličce", "V kuchyni", "Na okně" };

        var PlantsFaker = new Faker<Plant>()
            .RuleFor(f => f.Id, f => id++)
            .RuleFor(f => f.Name, (f, o) => $"Plant #{o.Id}")
            .RuleFor(f => f.Location, f => f.PickRandom(locations))
            .RuleFor(f => f.PurchasedDate, f => f.Date.Past())
            .RuleFor(f => f.Kind, f => f.PickRandom<PlantKind>())
            .RuleFor(f => f.Variety, f => f.PickRandom(_varieties))
            .RuleFor(f => f.IsDead, f => f.Random.Bool());

        _plants = PlantsFaker.Generate(100);
    }

    public IList<Plant> GetPlants() => _plants;

    public Plant GetPlant(int id) => _plants.First(f => f.Id == id);

    public void DeletePlant(int id) => _plants.Remove(GetPlant(id));

    public void AddPlant(Plant newPlant) => _plants.Add(newPlant);

    public void UpdatePlant(Plant plant)
    {
        var index = _plants.IndexOf(GetPlant(plant.Id));
        _plants[index] = plant;
    }

    public IList<string> GetPlantVarieties() => _varieties;

}
