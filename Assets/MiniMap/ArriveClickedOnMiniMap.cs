using System.Collections;
using System.Collections.Generic;
using Com.StudioTBD.CoronaIO.Agent.Zombie;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArriveClickedOnMiniMap : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ArriveStateMachineExtension.clickedOnMiniMap = true;
    }
}
