using YourNamespace;

public class EventPresenter
{
    private readonly IEventView _view;
    private readonly List<EventModel> _events;

    public EventPresenter(IEventView view)
    {
        _view = view;
        _events = new List<EventModel>();

        _view.LoadEvents += OnLoadEvents;
        _view.AddEvent += OnAddEvent;
        _view.UpdateEvent += OnUpdateEvent;
        _view.DeleteEvent += OnDeleteEvent;
    }

    private void OnLoadEvents(object sender, EventArgs e)
    {
        _view.DisplayEvents(_events);
    }

    private void OnAddEvent(object sender, EventArgs e)
    {
        var eventModel = _view.GetEventDetails();
        _events.Add(eventModel);
        _view.DisplayEvents(_events);
    }

    private void OnUpdateEvent(object sender, EventArgs e)
    {
        var eventModel = _view.GetEventDetails();
        var existingEvent = _events.FirstOrDefault(e => e.Id == eventModel.Id);
        if (existingEvent != null)
        {
            existingEvent.Title = eventModel.Title;
            existingEvent.Description = eventModel.Description;
            existingEvent.Date = eventModel.Date;
        }
        _view.DisplayEvents(_events);
    }

    private void OnDeleteEvent(object sender, EventArgs e)
    {
        var eventModel = _view.GetEventDetails();
        _events.RemoveAll(e => e.Id == eventModel.Id);
        _view.DisplayEvents(_events);
    }

    internal void SetView(EventView eventView)
    {
        throw new NotImplementedException();
    }
}
