using System.ComponentModel.DataAnnotations;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Validations {
    public class IsNotClosed : ValidationAttribute {
        public string GetIsClosedErrorMessage(object? value) {
          return $"The given raffle is already closed.";
        }

        public string GetNullRaffleErrorMessage(object? value) {
            return $"The raffle with id {value} doesn't exist";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            var db_context = (ApplicationDbContext) validationContext.GetService(typeof(ApplicationDbContext));
            var raffle = db_context.Raffles.Find((long) value);

            if(raffle == null)
                return new ValidationResult(GetNullRaffleErrorMessage(value));

            if(raffle.IsClosed) 
                return new ValidationResult(GetIsClosedErrorMessage(value));
            
            return ValidationResult.Success;
        }
    }
}