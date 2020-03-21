using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewportBox : MonoBehaviour
{
    Camera mainCamera;

    LineRenderer viewPort;

    public MeshRenderer gameFloor;
    public RectTransform miniMap;

    Vector2[] cornerMapPos;

    private void Start()
    {
        mainCamera = Camera.main;
        viewPort = this.GetComponent<LineRenderer>();
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
                cornerWorldPos[i].y = 0;
            }
        }

        Vector3 topRightMapPos, bottomRightMapPos, topLeftMapPos, bottomLeftMapPos;
        topRightMapPos = bottomRightMapPos = topLeftMapPos = bottomLeftMapPos = Vector3.zero;

        cornerMapPos = new Vector2[] { topRightMapPos, bottomRightMapPos, topLeftMapPos, bottomLeftMapPos };

        Vector2 miniMapSize = new Vector2(miniMap.sizeDelta.x, miniMap.sizeDelta.y);
        Vector2 worldSize = new Vector2(gameFloor.bounds.size.x, gameFloor.bounds.size.z);

        for (int i = 0; i < cornerMapPos.Length; i++)
        {
            cornerMapPos[i] = getMiniMapPos(new Vector2(cornerWorldPos[i].x, cornerWorldPos[i].z), miniMapSize, worldSize);
        }


        if (this.name.Equals("TopRight"))
            this.GetComponent<RectTransform>().anchoredPosition = cornerMapPos[0];
        if (this.name.Equals("TopLeft"))
            this.GetComponent<RectTransform>().anchoredPosition = cornerMapPos[1];
        if (this.name.Equals("BottomRight"))
            this.GetComponent<RectTransform>().anchoredPosition = cornerMapPos[2];
        if (this.name.Equals("BottomLeft"))
            this.GetComponent<RectTransform>().anchoredPosition = cornerMapPos[3];

        //drawViewPort(cornerMapPos);
    }

    private void drawViewPort(Vector2[] cornerMapPos)
    {

        if (viewPort == null)
            return;

        viewPort.positionCount = 8;

        // Top Right -> Bottom Right
        viewPort.SetPosition(0, new Vector3(cornerMapPos[0].x, 0, cornerMapPos[0].y));
        viewPort.SetPosition(1, new Vector3(cornerMapPos[1].x, 0, cornerMapPos[1].y));

        // Top Right -> Top Left
        viewPort.SetPosition(2, new Vector3(cornerMapPos[0].x, 0, cornerMapPos[0].y));
        viewPort.SetPosition(3, new Vector3(cornerMapPos[2].x, 0, cornerMapPos[2].y));

        // Top Left -> Bottom Left
        viewPort.SetPosition(4, new Vector3(cornerMapPos[2].x, 0, cornerMapPos[2].y));
        viewPort.SetPosition(5, new Vector3(cornerMapPos[3].x, 0, cornerMapPos[3].y));

        // Bottom Left -> Bottom Right
        viewPort.SetPosition(6, new Vector3(cornerMapPos[3].x, 0, cornerMapPos[3].y));
        viewPort.SetPosition(7, new Vector3(cornerMapPos[1].x, 0, cornerMapPos[1].y));

        viewPort.startColor = Color.white;
        viewPort.endColor = Color.white;
        viewPort.startWidth = 0.5f;
        viewPort.endWidth = 0.5f;
    }

    private Vector2 getMiniMapPos(Vector2 targetPos, Vector2 miniMapSize, Vector2 worldSize)
    {
        return targetPos * (miniMapSize / worldSize);
    }
}
