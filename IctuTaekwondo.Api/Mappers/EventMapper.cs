using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Shared.Responses.Event;

namespace IctuTaekwondo.Api.Mappers
{
    public static class EventMapper
    {
        public static EventResponse ToEventResponse(this Event @event)
        {
            return new EventResponse
            {
                Id = @event.Id,
                Name = @event.Name,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate,
                Location = @event.Location,
                Fee = @event.Fee,
                MaxParticipants = @event.MaxParticipants,
                Description = @event.Description,
                CreatedAt = @event.CreatedAt
            };
        }

        public static ResgiteredUsersResponse ToUserResgiteredEventResponse(this User user)
        {
            return new ResgiteredUsersResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
            };
        }
    }
}
