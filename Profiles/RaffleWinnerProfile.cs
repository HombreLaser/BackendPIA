using AutoMapper;
using BackendPIA.Forms;
using BackendPIA.Models;

namespace BackendPIA.Profiles {
    public class RaffleWinnerProfile : Profile {
        public RaffleWinnerProfile() {
            CreateMap<RaffleWinner, WinnerDTO>().ForMember(dto => dto.Winner, o => o.MapFrom(MapWinner)).ForMember(dto => dto.Prize, o => o.MapFrom(MapPrize))
                                                .ForMember(dto => dto.Raffle, o => o.MapFrom(MapRaffle));
        }

        private string MapWinner(RaffleWinner model, WinnerDTO dto) {
            return model.UserAccount.UserName;
        } 

        private string MapPrize(RaffleWinner model, WinnerDTO dto) {
            return model.Prize.Name;
        }

        private string MapRaffle(RaffleWinner model, WinnerDTO dto) {
            return model.Raffle.Name;
        }
    }
}