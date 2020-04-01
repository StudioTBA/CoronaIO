using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickAndGoToHorde : MonoBehaviour, IPointerClickHandler
{
    bool isSelected = false;
    Color originalColor;
    Image blipOnMiniMap;

    private void Start()
    {
        blipOnMiniMap = this.GetComponent<Image>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSelected)
        {
            originalColor = blipOnMiniMap.color;
            blipOnMiniMap.color = Color.yellow;
        }
    }

}
