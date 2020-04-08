using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.Agent.Zombie
{
    public class ZombieStateMachine : StateMachine
    {
        private ZombieDataHolder _dataHolder;

        public ZombieDataHolder ZombieDataHolder
        {
            get => _dataHolder;
            set => _dataHolder = value;
        }

        public ZombieStateMachine(ZombieDataHolder dataHolder)
        {
            ZombieDataHolder = dataHolder;
        }
    }
}
