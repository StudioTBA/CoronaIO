using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class CameraViewportBox : MonoBehaviour
{
    Camera mainCamera;

    MiniMapAndWorldHelper mapHelper;
    private void Start()
    {
        mainCamera = Camera.main;
        mapHelper = GameObject.Find("MiniMapManager").GetComponent<MiniMapAndWorldHelper>();
    }
    // Update is called once per frame
    void Update()
    {
        handleCameraViewPortInMiniMap();
    }


    /// <summary>
    /// Takes care of getting the position of the corners of the viewport in the World, 
    /// translating it to MiniMap "Space" and connecting the points with lines.
    /// </summary>
    private void handleCameraViewPortInMiniMap()
    {
        // Initializing Rays whose origin represent the corners of the cameras viewport
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

        // Initializing structures that will hold the position of where the rays hit in World Space
        Vector3 topRightWorldPos, bottomRightWorldPos, topLeftWorldPos, bottomLeftWorldPos;
        topRightWorldPos = bottomRightWorldPos = topLeftWorldPos = bottomLeftWorldPos = Vector3.zero;

        Vector3[] cornerWorldPos = { topRightWorldPos, bottomRightWorldPos, topLeftWorldPos, bottomLeftWorldPos };


        // Where in the world do the rays hit
        for (int i = 0; i < cornerRays.Length; i++)
        {
            if (Physics.Raycast(cornerRays[i], out hits[i], Mathf.Infinity))
            {
                cornerWorldPos[i] = hits[i].point;
            }
        }


        // Initializing structures that will hold the position of the where the rays hit in Map Space
        Vector3 topRightMapPos, bottomRightMapPos, topLeftMapPos, bottomLeftMapPos;
        topRightMapPos = bottomRightMapPos = topLeftMapPos = bottomLeftMapPos = Vector3.zero;

        Vector2[] cornerMapPos = new Vector2[] { topRightMapPos, bottomRightMapPos, topLeftMapPos, bottomLeftMapPos };

        float miniMapSize = mapHelper.MiniMapSize;
        float worldSize = mapHelper.WorldSize;

        // Translating the World Ray Position to Map Position
        for (int i = 0; i < cornerMapPos.Length; i++)
        {
            Vector2 worldPos = new Vector2(cornerWorldPos[i].x, cornerWorldPos[i].z);
            cornerMapPos[i] = mapHelper.getMiniMapPos(worldPos, miniMapSize, worldSize);
        }

        /* DEBUG PURPOSES
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

    /// <summary>
    /// Takes care of drawing the viewport in the MiniMap
    /// </summary>
    /// <param name="cornerMapPos"></param>
    private void drawViewPort(Vector2[] cornerMapPos)
    {
        UILineRenderer lineRenderer = this.GetComponentInChildren<UILineRenderer>();

        lineRenderer.Points = new Vector2[cornerMapPos.Length * 2];

        // Top Right -> Top Left
        lineRenderer.Points[0] = cornerMapPos[0];
        lineRenderer.Points[1] = cornerMapPos[2];

        // Top Left -> Bottom Left
        lineRenderer.Points[2] = cornerMapPos[2];
        lineRenderer.Points[3] = cornerMapPos[3];

        // Bottom Left -> Bottom Right
        lineRenderer.Points[4] = cornerMapPos[3];
        lineRenderer.Points[5] = cornerMapPos[1];

        // Bottom Right -> Top Right
        lineRenderer.Points[6] = cornerMapPos[1];
        lineRenderer.Points[7] = cornerMapPos[0];
    }
}
