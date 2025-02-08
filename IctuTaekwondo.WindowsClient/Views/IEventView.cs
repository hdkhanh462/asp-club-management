public interface IEventView
{
    event EventHandler LoadEvents;
    event EventHandler AddEvent;
    event EventHandler UpdateEvent;
    event EventHandler DeleteEvent;

    void DisplayEvents(List<EventModel> events);
    EventModel GetEventDetails();
}
