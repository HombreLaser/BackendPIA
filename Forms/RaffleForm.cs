using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BackendPIA.Forms {
    public class RaffleForm {
        [Required]
        [StringLength(128)]
        public string? Name { get; set; }
        [Required]
        [Range(1, 54, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Winners { get; set; }
    }
}