using AutoMapper;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Profiles {
    public class UserAccountProfile : Profile {
        public UserAccountProfile() {
            CreateMap<UserAccountForm, UserAccount>().ReverseMap();
        }
    }
}
