using System.ComponentModel.DataAnnotations;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Validations {
    public class UniqueTier : ValidationAttribute {
        public string GetErrorMessage(object? value) {
          return $"The given raffle already has a {value}-Tier.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            var prize = (PrizeForm) validationContext.ObjectInstance;
            var db_context = (ApplicationDbContext) validationContext.GetService(typeof(ApplicationDbContext));

            if(db_context.Prizes.Where(p => p.RaffleId == prize.RaffleId).Where(p => p.Tier == (int) value).Any()) 
                return new ValidationResult(GetErrorMessage(value));
            
            return ValidationResult.Success;
        }
    }
}