using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.JsonPatch;
using BackendPIA.Models;

namespace BackendPIA.Services {
    public interface IPrizeService {
        public Task<Prize> CreatePrize(Prize to_create);
        public Task<IEnumerable<Prize>> GetPrizes();
        public Task<Prize> GetPrize(long id);
        public Task<bool> DeletePrize(long id);
    }
}