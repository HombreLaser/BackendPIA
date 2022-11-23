using AutoMapper;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Profiles {
    public class TicketProfile : Profile {
        public TicketProfile() {
            CreateMap<TicketForm, Ticket>();
            CreateMap<Ticket, TicketDTO>();
        }
    }
}
