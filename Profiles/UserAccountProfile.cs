using AutoMapper;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Profiles {
    public class UserAccountProfile : Profile {
        public UserAccountProfile() {
            CreateMap<UserAccountForm, UserAccount>().ReverseMap();
            CreateMap<UserAccount, UserAccountDTO>().ForMember(dto => dto.Tickets, o => o.MapFrom(UserTickets));
        }

        private ICollection<TicketDTO> UserTickets(UserAccount user, UserAccountDTO dto) {
            ICollection<TicketDTO> tickets = new List<TicketDTO>();

            if(user.Tickets == null)
                return tickets;

            foreach (var ticket in user.Tickets) {
                tickets.Add(new TicketDTO {
                    Id = ticket.Id,
                    Number = ticket.Number,
                    IsWinner = ticket.IsWinner,
                    RaffleId = ticket.RaffleId,
                    UserAccountId = user.Id
                });
            }

            return tickets;
        }
    }
}
