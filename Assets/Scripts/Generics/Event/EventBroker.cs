namespace Com.StudioTBD.CoronaIO.Event
{
    public interface EventBroker
    {
        void Consume(Event @event);
    }
}