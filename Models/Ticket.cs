using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPIA.Models {
    public class Ticket {
        public long Id { get; set; }
        [Required]
        [Range(1, 54, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Number { get; set; }
        public bool IsWinner { get; set; }
        [Required]
        [ForeignKey("UserAccountId")]
        public string? UserAccountId { get; set; }
        [Required]
        [ForeignKey("RaffleId")]
        public long RaffleId { get; set; }
        public UserAccount? Owner { get; set; }
        public Raffle? Raffle { get; set; }
    }
}