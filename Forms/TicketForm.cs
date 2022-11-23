using System.ComponentModel.DataAnnotations;

namespace BackendPIA.Forms {
    public class TicketForm {
        [Required]
        [Range(1, 54, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Number { get; set; }
    }
}