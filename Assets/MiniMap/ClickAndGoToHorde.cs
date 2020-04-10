using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickAndGoToHorde : MonoBehaviour, IPointerClickHandler
{
    public static GameObject SelectedHorde { get; set; }
    public static GameObject LockedHorde { get; set; }
    Color originalColor;
    public Image blipOnMiniMap;
    GameObject miniMap;
    GameObject cameraHandler;
    GameObject hordeManager;
    bool followHorde = true;

    private void Start()
    {
        blipOnMiniMap = this.GetComponent<Image>();
        originalColor = blipOnMiniMap.color;
        SelectedHorde = null;
        LockedHorde = null;
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

        if (SelectedHorde == null && LockedHorde != null)
        {
            if (eventData.pointerPress.Equals(LockedHorde))
            {
                followHorde = true;
                return;
            }
        }

        if (SelectedHorde == null)
        {
            SelectedHorde = eventData.pointerPress;
            SelectedHorde.GetComponent<ClickAndGoToHorde>().blipOnMiniMap.color = Color.yellow;
            return;
        }

        if (SelectedHorde != null && LockedHorde == null)
        {
            if (eventData.pointerPress.Equals(SelectedHorde))
            {
                LockedHorde = SelectedHorde;
                LockedHorde.GetComponent<ClickAndGoToHorde>().blipOnMiniMap.color = Color.green;
                followHorde = true;
                SelectedHorde = null;
                return;
            }

            SelectedHorde.GetComponent<ClickAndGoToHorde>().resetColor();
            SelectedHorde = eventData.pointerPress;
            SelectedHorde.GetComponent<ClickAndGoToHorde>().blipOnMiniMap.color = Color.yellow;
            return;
        }

        if (SelectedHorde != null && LockedHorde != null)
        {
            if (eventData.pointerPress.Equals(LockedHorde))
                return;

            if (eventData.pointerPress.Equals(SelectedHorde))
            {
                LockedHorde.GetComponent<ClickAndGoToHorde>().resetColor();
                LockedHorde = SelectedHorde;
                LockedHorde.GetComponent<ClickAndGoToHorde>().blipOnMiniMap.color = Color.green;
                followHorde = true;
                SelectedHorde = null;
                return;
            }
        }

    }

    private void followCenterOfMass()
    {
        if (blipOnMiniMap.color != Color.green)
            return;


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            followHorde = false;

        if (followHorde)
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
            zombie.GetComponent<MeshRenderer>().materials[1].SetFloat("_Outline", 250f);
        }

        return centerOfMass /= zombiesInHorde.Count;
    }

    public void resetColor()
    {
        blipOnMiniMap.color = originalColor;

        List<Flocker> zombiesInHorde = hordeManager.GetComponent<FlockManager>().getZombieList();

        foreach (Flocker zombie in zombiesInHorde)
        {
            zombie.GetComponent<MeshRenderer>().materials[1].SetFloat("_Outline", 0f);
        }
    }

}
