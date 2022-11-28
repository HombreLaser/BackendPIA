using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendPIA.Validations;

namespace BackendPIA.Forms {
    public class PrizeForm {
        [Required]
        [ForeignKey("RaffleId")]
        [IsNotClosed]
        [MaximumWinners]
        public long RaffleId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [UniqueTier]
        [Range(0, 53)]
        public int Tier { get; set; }
        [Required]
        public string? Category { get; set; }
    }
}