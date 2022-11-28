using AutoMapper; 
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Profiles {
    public class PrizeProfile : Profile {
        public PrizeProfile() {
            CreateMap<PrizeForm, Prize>();
            CreateMap<JsonPatchDocument<PrizeForm>, JsonPatchDocument<Prize>>();
            CreateMap<Operation<PrizeForm>, Operation<Prize>>();
            CreateMap<Prize, PrizeDTO>();
        }
    }
}