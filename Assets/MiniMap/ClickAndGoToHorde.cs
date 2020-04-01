using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickAndGoToHorde : MonoBehaviour, IPointerClickHandler
{
    public bool IsSelected { get; set; }
    Color originalColor;
    Image blipOnMiniMap;
    GameObject miniMap;
    GameObject cameraHandler;
    GameObject hordeManager;

    private void Start()
    {
        blipOnMiniMap = this.GetComponent<Image>();
        originalColor = blipOnMiniMap.color;
        IsSelected = false;
        miniMap = GameObject.Find("MiniMap");
        cameraHandler = GameObject.Find("CameraHandler");
        hordeManager = this.GetComponent<MiniMapIcon>().target.gameObject;
    }

    private void Update()
    {
        followCenterOfMass();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsSelected)
        {
            IsSelected = true;
            miniMap.GetComponent<DeselectZombieHorde>().SelectedHorde = this.gameObject;
            blipOnMiniMap.color = Color.yellow;
            return;
        }

        blipOnMiniMap.color = Color.green;
    }

    private void followCenterOfMass()
    {
        if (blipOnMiniMap.color != Color.green)
            return;

        cameraHandler.GetComponent<CameraMovement>().GoalPos = calculateCenterOfMass();
    }

    Vector3 calculateCenterOfMass()
    {

        Vector3 centerOfMass = Vector3.zero;
        List<Flocker> zombiesInHorde = hordeManager.GetComponent<FlockManager>().getZombieList();

        if (zombiesInHorde.Count == 0)
            return cameraHandler.transform.position;

        foreach (Flocker zombie in zombiesInHorde)
        {
            centerOfMass += zombie.transform.position;
        }

        return centerOfMass /= zombiesInHorde.Count;
    }

    public void resetColor()
    {
        blipOnMiniMap.color = originalColor;
    }

}
