namespace Com.StudioTBD.CoronaIO.Event
{
    public interface EventHandler
    {
        void Consume(Event @event);
    }
}