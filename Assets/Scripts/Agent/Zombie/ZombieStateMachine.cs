using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine.AI;
using Com.StudioTBD.CoronaIO.Agent.Human;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie
{
    public class ZombieStateMachine : StateMachine
    {
        private DataHolder _dataHolder;

        public DataHolder DataHolder
        {
            get => _dataHolder;
            set => _dataHolder = value;
        }

        public ZombieStateMachine(DataHolder dataHolder)
        {
            DataHolder = dataHolder;
        }
    }
}
