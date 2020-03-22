using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapAndWorldHelper : MonoBehaviour
{
    public float MiniMapSize { get; private set; }
    public float WorldSize { get; private set; }
    public float MiniMapCanvasScale { get; private set; }

    RectTransform miniMap;
    RectTransform miniMapCanvas;
    public MeshRenderer worldPlane;

    // Start is called before the first frame update
    void Start()
    {
        miniMap = GameObject.Find("MiniMap").GetComponent<RectTransform>();
        miniMapCanvas = GameObject.Find("MiniMapCanvas").GetComponent<RectTransform>();
        setSizes();
    }

    /// <summary>
    /// Takes care of setting the minimap canvas size and the size of the world
    /// as well as the MiniMap Canvas Scale
    /// 
    /// NOTE: This assumes they are square (nxn dimensions)
    /// </summary>
    private void setSizes()
    {
        MiniMapSize = miniMap.sizeDelta.x;
        WorldSize = worldPlane.bounds.size.x;
        MiniMapCanvasScale = miniMapCanvas.localScale.x;
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
