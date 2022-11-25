namespace BackendPIA.Models {
    public class RaffleWinner {
        public long Id { get; set; }
        public string UserAccountId { get; set; }
        public long PrizeId { get; set; }
        public long RaffleId { get; set; }
        public UserAccount? UserAccount { get; set; }
        public Prize? Prize { get; set; }
        public Raffle? Raffle { get; set; }
    }
}