namespace BackendPIA.Forms {
    public class UserAccountDTO {
        public string Id { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public ICollection<TicketDTO>? Tickets { get; set; }
    }
}