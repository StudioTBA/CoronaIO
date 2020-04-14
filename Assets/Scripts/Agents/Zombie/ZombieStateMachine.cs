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
        static bool didDoubleClick = false;
        public static bool CheckAndTransitionToArrive(this State state, ZombieDataHolder dataHolder)
        {
            if (!HordeHelper.Instance?.LockedHorde)
                return false;

            GameObject flockCenter = HordeHelper.Instance.LockedHorde.GetComponent<MiniMapIcon>().target.gameObject;

            if (!flockCenter.Equals(state.gameObject))
                return false;

            checkMouseClick();

            if (didDoubleClick)
            {
                didDoubleClick = false;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    GameObject.Destroy(dataHolder.myArriveParticleFX);
                    dataHolder.myArriveParticleFX = GameObject.Instantiate(dataHolder.arriveParticleFXPrefab, new Vector3(hit.point.x, 25, hit.point.z), dataHolder.arriveParticleFXPrefab.transform.rotation);

                    dataHolder.NavMeshAgent.ResetPath();
                    dataHolder.NavMeshAgent.SetDestination(new Vector3(hit.point.x, 0, hit.point.z));

                    state.CancelInvoke();
                    state.ChangeState(state.GetComponent<Zombie_Arrive>());
                    return true;
                }
            }

            return false;
        }

        static void checkMouseClick()
        {
            if (Input.GetMouseButtonDown(1))
            {
                mouseCLick++;
            }

            if (mouseCLick == 1)
                clickTimerCountdown -= Time.deltaTime;

            if (clickTimerCountdown <= 0 || didDoubleClick)
            {
                clickTimerCountdown = clickTimer;
                didDoubleClick = false;
                mouseCLick = 0;
            }

            if (mouseCLick == 2)
            {
                didDoubleClick = true;
                mouseCLick = 0;
                clickTimerCountdown = clickTimer;
            }

        }
    }
}
