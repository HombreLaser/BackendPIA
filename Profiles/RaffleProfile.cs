using AutoMapper;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Profiles {
    public class RaffleProfile : Profile {
        public RaffleProfile() {
            CreateMap<RaffleForm, Raffle>().ReverseMap();
            CreateMap<Raffle, RaffleDTO>();
        }
    }
}
