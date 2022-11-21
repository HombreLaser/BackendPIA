using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BackendPIA.Models {
    public class Raffle {
        public long Id { get; set; }
        [Required]
        [StringLength(128)]
        public string? Name { get; set; }
        [Required]
        public int Winners { get; set; }
        public bool IsClosed { get; set; }
    }
}