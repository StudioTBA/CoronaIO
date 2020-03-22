using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraClickAndGo : MonoBehaviour, IPointerClickHandler
{
    RectTransform cameraHandlerBlip;
    GameObject cameraHandler;
    MiniMapAndWorldHelper mapHelper;

    private void Start()
    {
        cameraHandler = GameObject.Find("CameraHandler");
        mapHelper = GameObject.Find("MiniMapManager").GetComponent<MiniMapAndWorldHelper>();
        cameraHandlerBlip = GameObject.Find("CameraHandlerBlip").GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        float miniMapSize = mapHelper.MiniMapSize;
        float worldSize = mapHelper.WorldSize;

        Vector2 cameraHandlerBlipPos = new Vector2(cameraHandlerBlip.position.x, cameraHandlerBlip.position.y);
        Vector2 blipToMouse = eventData.position - cameraHandlerBlipPos;
        float distanceBlipToMouse = blipToMouse.magnitude;
        Vector2 directionBlipToMouse = blipToMouse / distanceBlipToMouse;

        float miniMapCanvasScale = mapHelper.MiniMapCanvasScale;

        float blipToMouseWorldX = (directionBlipToMouse * distanceBlipToMouse * (worldSize / miniMapSize)).x;
        float blipToMouseWorldY = (directionBlipToMouse * distanceBlipToMouse * (worldSize / miniMapSize)).y;

        Vector3 goalPos = new Vector3(blipToMouseWorldX, 0, blipToMouseWorldY) / miniMapCanvasScale;

        cameraHandler.GetComponent<CameraMovement>().GoalPos = cameraHandler.transform.position + goalPos;
    }
}
