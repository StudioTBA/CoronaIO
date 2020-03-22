using UnityEngine;

namespace Com.StudioTBD.CoronaIO.FMS
{
    /// <summary>
    /// Basic human event. This is an example and working implementation fo an event.
    /// </summary>
    public class HumanEvent : Event.Event
    {
        public enum HumanEventType
        {
            PoliceAlert,
            SpottedZombie
        }

        public readonly HumanEventType EventType;

        public HumanEvent(GameObject producer, HumanEventType type) : base(producer)
        {
            EventType = type;
        }
    }
}