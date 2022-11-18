using System.ComponentModel.DataAnnotations;

namespace BackendPIA.Forms {
    public class UserAccountForm {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}