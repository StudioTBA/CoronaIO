using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraClickAndGo : MonoBehaviour, IPointerClickHandler
{

    public MeshRenderer gameFloor;
    public RectTransform miniMap;
    public RectTransform cameraHandlerBlip;

    CameraMovement cameraHandlerData;

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 miniMapSize = new Vector2(miniMap.sizeDelta.x, miniMap.sizeDelta.y);
        Vector2 worldSize = new Vector2(gameFloor.bounds.size.x, gameFloor.bounds.size.z);
        cameraHandlerData = GameObject.Find("CameraHandler").GetComponent<CameraMovement>();
        Vector2 distance = eventData.position - new Vector2(cameraHandlerBlip.position.x, cameraHandlerBlip.position.y);
        float scale = GameObject.Find("MiniMapCanvas").GetComponent<RectTransform>().localScale.x;
        Debug.Log(cameraHandlerBlip.position);
        Debug.Log(eventData.position);
        Debug.Log(scale);
        //Debug.Log(distance.magnitude);
        //Debug.Log(distance.normalized);
        //Debug.Log(distance.magnitude * (worldSize / miniMapSize));

        cameraHandlerData.GoalPos = (cameraHandlerData.transform.position + (new Vector3((distance.normalized * distance.magnitude * (worldSize / miniMapSize)).x, 0, (distance.normalized * distance.magnitude * (worldSize / miniMapSize)).y)) / scale);

        Debug.Log(GameObject.Find("CameraHandler").transform.position);
    }
}
