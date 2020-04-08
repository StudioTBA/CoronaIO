using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DeselectZombieHorde : MonoBehaviour, IPointerClickHandler
{
    public GameObject SelectedHorde { get; set; }

    private void Start()
    {
        SelectedHorde = null;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (SelectedHorde == null)
            return;

        SelectedHorde.GetComponent<ClickAndGoToHorde>().IsSelected = false;
        SelectedHorde.GetComponent<ClickAndGoToHorde>().resetColor();
        SelectedHorde = null;
    }


}
