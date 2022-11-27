using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPIA.Models {
    public class RaffleWinner {
        public long Id { get; set; }
        [ForeignKey("UserAccountId")]
        public string UserAccountId { get; set; }
        [ForeignKey("PrizeId")]
        public long PrizeId { get; set; }
        [ForeignKey("RaffleId")]
        public long RaffleId { get; set; }
        public UserAccount? UserAccount { get; set; }
        public Prize? Prize { get; set; }
        public Raffle? Raffle { get; set; }
    }
}