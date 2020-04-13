using Com.StudioTBD.CoronaIO.Agent.Zombie.States;
using Com.StudioTBD.CoronaIO.FMS;
using Com.StudioTBD.CoronaIO.FMS.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public static class ArriveStateMachineExtension
    {
        static Camera mainCamera = Camera.main;

        static int mouseCLick;
        static float clickTimer = 0.5f;
        static float clickTimerCountdown = clickTimer;
        static bool arrive = false;
        public static void CheckAndTransitionToArrive(this State state, State currentState, ZombieDataHolder dataHolder)
        {
            checkMouseClick();

            if (arrive && HordeHelper.Instance?.LockedHorde)
            {
                GameObject flockCenter = HordeHelper.Instance.LockedHorde.GetComponent<MiniMapIcon>().target.gameObject;

                if (!flockCenter.Equals(currentState.gameObject))
                    return;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    arrive = false;
                    dataHolder.NavMeshAgent.ResetPath();
                    dataHolder.NavMeshAgent.SetDestination(new Vector3(hit.point.x, 0, hit.point.z));
                    currentState.CancelInvoke();
                    currentState.ChangeState(currentState.GetComponent<Zombie_Arrive>());
                }
            }
        }

        static void checkMouseClick()
        {

            if (Input.GetMouseButtonDown(0))
                mouseCLick++;

            if (mouseCLick == 1)
                clickTimerCountdown -= Time.deltaTime;

            if (clickTimerCountdown <= 0 || arrive)
            {
                clickTimerCountdown = clickTimer;
                arrive = false;
                mouseCLick = 0;
            }

            if (mouseCLick == 2)
            {
                arrive = true;
                mouseCLick = 0;
                clickTimerCountdown = clickTimer;
            }

        }
    }
}
