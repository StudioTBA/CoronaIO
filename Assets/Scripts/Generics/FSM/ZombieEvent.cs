using UnityEngine;

namespace Com.StudioTBD.CoronaIO.FMS
{
    public class ZombieEvent : Event.Event
    {
        public enum ZombieEventType
        {
            SpottedHuman,
            Overwhelmed,
            LackingGoal,
            WaitingForOrders
        }

        public readonly ZombieEventType EventType;

        public ZombieEvent(GameObject producer, ZombieEventType type) : base(producer)
        {
            EventType = type;
        }
    }
}
