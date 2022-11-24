using AutoMapper;

using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Profiles {
    public class PrizeProfile : Profile {
        public PrizeProfile() {
            CreateMap<PrizeForm, Prize>();
        }
    }
}