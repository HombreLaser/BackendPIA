namespace BackendPIA.Models {
    public class Prize {
        public long Id { get; set; }
        public long RaffleId { get; set; }
        public Raffle? Raffle { get; set; }
        public string Name { get; set; }
        public int Tier { get; set; }
        public string Category { get; set; }
    }
}