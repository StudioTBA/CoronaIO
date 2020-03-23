using UnityEngine;

namespace Com.StudioTBD.CoronaIO.Event
{
    /// <summary>
    /// Base class for any sort of event.
    /// </summary>
    public abstract class Event
    {
        public readonly GameObject Producer;

        public Event(GameObject producer)
        {
            Producer = producer;
        }
    }
}