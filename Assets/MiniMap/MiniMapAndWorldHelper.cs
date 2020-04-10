using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapAndWorldHelper : MonoBehaviour
{
    public float MiniMapSize
    {
        get
        {
            return miniMap.sizeDelta.x;
        }
    }
    public float WorldSize
    {
        get
        {
            return worldPlane.bounds.size.x;
        }
    }
    public float MiniMapCanvasScale
    {
        get
        {
            return miniMapCanvas.localScale.x;
        }
    }

    RectTransform miniMap;
    RectTransform miniMapCanvas;
    public MeshRenderer worldPlane;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MiniMapCanvas").GetComponent<Canvas>().enabled = true;
        miniMap = GameObject.Find("MiniMap").GetComponent<RectTransform>();
        miniMapCanvas = GameObject.Find("MiniMapCanvas").GetComponent<RectTransform>();
    }


    /// <summary>
    /// Takes care of translating from World Position to MiniMap Position
    /// </summary>
    /// <param name="targetWorldPos"></param>
    /// <param name="miniMapSize"></param>
    /// <param name="worldSize"></param>
    /// <returns></returns>
    public Vector2 getMiniMapPos(Vector2 targetWorldPos, float miniMapSize, float worldSize)
    {
        return targetWorldPos * (miniMapSize / worldSize);
    }
}
