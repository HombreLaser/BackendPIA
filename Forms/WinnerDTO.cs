namespace BackendPIA.Forms {
    public class WinnerDTO {
        public long Id { get; set; }
        public string? UserAccountId { get; set; }
        public long RaffleId { get; set; }
        public string? Winner { get; set; }
        public string? Prize { get; set; }
        public string? Raffle { get; set; }      
    }
}