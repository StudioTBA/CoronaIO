using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;
public class DeselectZombieHorde : MonoBehaviour, IPointerClickHandler
{


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!eventData.pointerPress.name.Contains("ZombieHordeBlip"))
        {

            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (HordeHelper.Instance.SelectedHorde != null)
            {
                HordeHelper.Instance.SelectedHorde.GetComponent<ClickAndGoToHorde>().resetColor();
                HordeHelper.Instance.SelectedHorde = null;
            }

            if (HordeHelper.Instance.LockedHorde != null)
            {
                HordeHelper.Instance.LockedHorde.GetComponent<ClickAndGoToHorde>().resetColor();
                HordeHelper.Instance.LockedHorde = null;
            }

            return;
        }
    }
}
