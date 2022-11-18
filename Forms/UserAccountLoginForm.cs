using System.ComponentModel.DataAnnotations;

namespace BackendPIA.Forms {
    public class UserAccountLoginForm {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}