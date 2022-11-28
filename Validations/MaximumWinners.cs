using System.ComponentModel.DataAnnotations;
using BackendPIA.Models;

namespace BackendPIA.Validations {
    public class MaximumWinners : ValidationAttribute {
        public string GetMaximumWinnersMessage(object? value) {
          return $"The given raffle has reached the prize limit.";
        }

        public string GetNullRaffleErrorMessage(object? value) {
            return $"The raffle with id {value} doesn't exist";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            var db_context = (ApplicationDbContext) validationContext.GetService(typeof(ApplicationDbContext));
            var raffle = db_context.Raffles.Find((long) value);

            if(raffle == null)
                return new ValidationResult(GetNullRaffleErrorMessage(value));

            if(db_context.Prizes.Where(p => p.RaffleId == (long) value).Count() >= raffle.Winners) 
                return new ValidationResult(GetMaximumWinnersMessage(value));
            
            return ValidationResult.Success;
        }
    }
}