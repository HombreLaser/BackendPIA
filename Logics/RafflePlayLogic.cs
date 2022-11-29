using BackendPIA.Models;
using BackendPIA.Services;

namespace BackendPIA.Logics {
    public class RafflePlayLogic {
        private readonly IGameService _game_service;
        private readonly IRaffleService _raffle_service;
        private readonly long _raffle_id;
        public string? ErrorMessage { get; set; }
        public IEnumerable<RaffleWinner>? Winners { get; set;}

        public RafflePlayLogic(IGameService game_service, IRaffleService raffle_service, long raffle_id) {
            _game_service = game_service;
            _raffle_service = raffle_service;
            _raffle_id = raffle_id;
        }

        public async Task<bool> Call() {
            var raffle = await _raffle_service.GetRaffle(_raffle_id);

            // Checks.
            if(raffle.IsClosed) {
                ErrorMessage = $"The raffle is already closed.";

                return false;
            }

            if(raffle == null) {
                ErrorMessage = $"The raffle with id {_raffle_id} couldn't be found.";

                return false;
            }

            if(raffle.Winners >= _raffle_service.GetRaffleTickets(_raffle_id).Count()) {
                ErrorMessage = $"Can't play: not enough players.";

                return false;
            }

            if(raffle.Prizes.Count() < raffle.Winners) {
                ErrorMessage = $"Can't play: not enough prizes.";

                return false;
            }

            Winners = await _game_service.GetWinners(_raffle_id);

            return true;
        }
    }
}