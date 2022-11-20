using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;


namespace BackendPIA.Forms {
    public class UserAccountForm {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [Ignore]
        public string? Password { get; set; }
    }
}