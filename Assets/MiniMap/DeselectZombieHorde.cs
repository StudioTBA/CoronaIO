using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DeselectZombieHorde : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!eventData.pointerPress.name.Contains("ZombieHordeBlip"))
        {
            if (ClickAndGoToHorde.SelectedHorde != null)
            {
                ClickAndGoToHorde.SelectedHorde.GetComponent<ClickAndGoToHorde>().resetColor();
                ClickAndGoToHorde.SelectedHorde = null;
            }

            if (ClickAndGoToHorde.LockedHorde != null)
            {
                ClickAndGoToHorde.LockedHorde.GetComponent<ClickAndGoToHorde>().resetColor();
                ClickAndGoToHorde.LockedHorde = null;
            }

            return;
        }
    }
}
