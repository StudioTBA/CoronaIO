using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    public MeshRenderer gameFloor;
    public RectTransform miniMap;
    public Transform target;
    float worldHeight, worldWidth, miniMapHeight, miniMapWidth;

    private void Start()
    {
        worldHeight = gameFloor.bounds.size.z;
        worldWidth = gameFloor.bounds.size.x;
        miniMapHeight = miniMap.sizeDelta.y;
        miniMapWidth = miniMap.sizeDelta.x;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        handleIconPosition();
    }

    private void handleIconPosition()
    {
        Vector2 targetPos = new Vector2(target.position.x, target.position.z);
        Vector2 miniMapSize = new Vector2(miniMapWidth, miniMapHeight);
        Vector2 worldSize = new Vector2(worldWidth, worldHeight);
        Vector2 iconPos = getMiniMapPos(targetPos, miniMapSize, worldSize);

        this.GetComponent<RectTransform>().anchoredPosition = iconPos;
    }

    private Vector2 getMiniMapPos(Vector2 targetPos, Vector2 miniMapSize, Vector2 worldSize)
    {
        return targetPos * (miniMapSize / worldSize);
    }
}
