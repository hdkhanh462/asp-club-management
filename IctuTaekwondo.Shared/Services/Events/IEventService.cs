using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Event;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Schemas.Event;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.Shared.Services.Events
{
    public interface IEventsService
    {
        public Task<Paginator<EventResponse>> GetAllAsync(string token, int page, int size, QueryBuilder? query = null);
        public Task<Dictionary<string, string[]>> CreateAsync(string token, EventCreateSchema schema);
        public Task<EventResponse?> UpdateAsync(string token, int id, EventUpdateSchema schema);
        public Task<EventResponse?> FindByIdAsync(string token, int id);
        public Task<bool> DeleteAsync(string token, int id);
    }
}
