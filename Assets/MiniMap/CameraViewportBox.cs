using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class CameraViewportBox : MonoBehaviour
{
    Camera mainCamera;
    public MeshRenderer gameFloor;
    public RectTransform miniMap;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        handleCameraViewPortInMiniMap();
    }

    private void handleCameraViewPortInMiniMap()
    {
        Vector3 topRightRayOrigin = new Vector3(Screen.width, Screen.height, 0);
        Ray topRightRay = mainCamera.ScreenPointToRay(topRightRayOrigin);

        Vector3 bottomRightRayOrigin = new Vector3(Screen.width, 0, 0);
        Ray bottomRightRay = mainCamera.ScreenPointToRay(bottomRightRayOrigin);

        Vector3 topLeftRayOrigin = new Vector3(0, Screen.height, 0);
        Ray topLeftRay = mainCamera.ScreenPointToRay(topLeftRayOrigin);

        Vector3 bottomLeftRayOrigin = new Vector3(0, 0, 0);
        Ray bottomLeftRay = mainCamera.ScreenPointToRay(bottomLeftRayOrigin);

        Ray[] cornerRays = { topRightRay, bottomRightRay, topLeftRay, bottomLeftRay };
        RaycastHit[] hits = new RaycastHit[4];

        Vector3 topRightWorldPos, bottomRightWorldPos, topLeftWorldPos, bottomLeftWorldPos;
        topRightWorldPos = bottomRightWorldPos = topLeftWorldPos = bottomLeftWorldPos = Vector3.zero;

        Vector3[] cornerWorldPos = { topRightWorldPos, bottomRightWorldPos, topLeftWorldPos, bottomLeftWorldPos };

        for (int i = 0; i < cornerRays.Length; i++)
        {
            if (Physics.Raycast(cornerRays[i], out hits[i], Mathf.Infinity))
            {
                cornerWorldPos[i] = hits[i].point;
            }
        }

        Vector3 topRightMapPos, bottomRightMapPos, topLeftMapPos, bottomLeftMapPos;
        topRightMapPos = bottomRightMapPos = topLeftMapPos = bottomLeftMapPos = Vector3.zero;

        Vector2[] cornerMapPos = new Vector2[] { topRightMapPos, bottomRightMapPos, topLeftMapPos, bottomLeftMapPos };

        Vector2 miniMapSize = new Vector2(miniMap.sizeDelta.x, miniMap.sizeDelta.y);
        Vector2 worldSize = new Vector2(gameFloor.bounds.size.x, gameFloor.bounds.size.z);

        for (int i = 0; i < cornerMapPos.Length; i++)
        {
            cornerMapPos[i] = getMiniMapPos(new Vector2(cornerWorldPos[i].x, cornerWorldPos[i].z), miniMapSize, worldSize);
        }

        /*
        GameObject.Find("TopRight").SetActive(true);
        GameObject.Find("TopRight").GetComponent<RectTransform>().anchoredPosition = cornerMapPos[0];

        GameObject.Find("BottomRight").SetActive(true);
        GameObject.Find("BottomRight").GetComponent<RectTransform>().anchoredPosition = cornerMapPos[1];

        GameObject.Find("TopLeft").SetActive(true);
        GameObject.Find("TopLeft").GetComponent<RectTransform>().anchoredPosition = cornerMapPos[2];

        GameObject.Find("BottomLeft").SetActive(true);
        GameObject.Find("BottomLeft").GetComponent<RectTransform>().anchoredPosition = cornerMapPos[3];
        */

        drawViewPort(cornerMapPos);
    }

    private void drawViewPort(Vector2[] cornerMapPos)
    {
        UILineRenderer lineRenderer = this.GetComponentInChildren<UILineRenderer>();

        lineRenderer.Points = new Vector2[cornerMapPos.Length * 2];

        lineRenderer.Points[0] = cornerMapPos[0];
        lineRenderer.Points[1] = cornerMapPos[2];
        lineRenderer.Points[2] = cornerMapPos[2];
        lineRenderer.Points[3] = cornerMapPos[3];
        lineRenderer.Points[4] = cornerMapPos[3];
        lineRenderer.Points[5] = cornerMapPos[1];
        lineRenderer.Points[6] = cornerMapPos[1];
        lineRenderer.Points[7] = cornerMapPos[0];
    }

    private Vector2 getMiniMapPos(Vector2 targetPos, Vector2 miniMapSize, Vector2 worldSize)
    {
        return targetPos * (miniMapSize / worldSize);
    }
}
