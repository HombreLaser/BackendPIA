namespace BackendPIA.Forms {
    public class RaffleDTO {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int Winners { get; set; }
        public bool IsClosed { get; set; }
    }
}