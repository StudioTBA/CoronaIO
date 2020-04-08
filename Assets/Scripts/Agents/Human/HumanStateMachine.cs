using Com.StudioTBD.CoronaIO.FMS;
using UnityEngine.AI;

namespace Com.StudioTBD.CoronaIO.Agent.Human
{
    public class HumanStateMachine : StateMachine
    {
        private DataHolder _dataHolder;

        public DataHolder DataHolder
        {
            get => _dataHolder;
            set => _dataHolder = value;
        }

        public HumanStateMachine(DataHolder dataHolder)
        {
            DataHolder = dataHolder;
        }
    }
}