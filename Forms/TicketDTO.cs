namespace BackendPIA.Forms {
    public class TicketDTO {
        public long Id { get; set; }
        public int Number { get; set; }
        public bool IsWinner { get; set; }
        public long RaffleId{ get; set; }
        public string? UserAccountId { get; set; }
    }
}