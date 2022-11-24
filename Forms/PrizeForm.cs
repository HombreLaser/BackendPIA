using System.ComponentModel.DataAnnotations;
using BackendPIA.Validations;

namespace BackendPIA.Forms {
    public class PrizeForm {
        [Required]
        public long RaffleId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [UniqueTier]
        public int Tier { get; set; }
        [Required]
        public string? Category { get; set; }
    }
}