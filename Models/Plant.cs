using System.ComponentModel;

namespace Explorations.Models
{
    public enum PlantKind { Tree, Plant, Cactus };
    public record Plant
    {
        public int Id { get; init; }

        [DisplayName("Name")]
        public required string Name { get; init; }

        [DisplayName("Location")]
        public string? Location { get; init; }

        [DisplayName("Variety")]
        public required string Variety { get; init; }

        [DisplayName("Purchased")]
        public DateTime PurchasedDate { get; init; }

        [DisplayName("Kind")]
        public PlantKind Kind { get; init; }

        [DisplayName("Is dead")]
        public bool IsDead { get; init; }
    }
}
