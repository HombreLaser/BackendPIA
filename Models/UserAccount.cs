using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BackendPIA.Models {
    public class UserAccount : IdentityUser {
        [StringLength(64)]
        public string? SessionToken { get; set; }
    }
}