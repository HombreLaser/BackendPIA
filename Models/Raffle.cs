using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace BackendPIA.Models {
    public class Raffle {
        public long Id { get; set; }
        [Required]
        [StringLength(128)]
        public string? Name { get; set; }
        [Required]
        [Range(1, 54, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Winners { get; set; }
        public bool IsClosed { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Ticket>? Tickets { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Prize>? Prizes { get; set; }
    }
}