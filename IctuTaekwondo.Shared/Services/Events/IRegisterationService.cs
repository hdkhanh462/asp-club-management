using IctuTaekwondo.Shared.Responses.Event;
using IctuTaekwondo.Shared.Responses;

namespace IctuTaekwondo.Shared.Services.Events
{
    public interface IRegisterationService
    {
        public Task<string?> RegisterAsync(string token, int eventId);
        public Task<bool> UnregisterAsync(string token,int eventId);
        public Task<bool> ManagerRegisterAsync(string token,int eventId, string userId);
        public Task<bool> ManagerUnregisterAsync(string token,int eventId, string userId);
        public Task<Paginator<ResgiteredUsersResponse>> GetAllAsync(string token,int id, int page, int size);
    }
}
