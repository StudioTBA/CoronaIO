using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    MiniMapAndWorldHelper mapHelper;
    public Transform target;
    private void Start()
    {
        mapHelper = GameObject.Find("MiniMapManager").GetComponent<MiniMapAndWorldHelper>();
    }


    // Update is called once per frame
    void LateUpdate()
    {
        handleIconPosition();
    }

    private void handleIconPosition()
    {
        Vector2 targetWorldPos = new Vector2(target.position.x, target.position.z);
        float miniMapSize = mapHelper.MiniMapSize;
        float worldSize = mapHelper.WorldSize;
        Vector2 iconMapPos = mapHelper.getMiniMapPos(targetWorldPos, miniMapSize, worldSize);

        this.GetComponent<RectTransform>().anchoredPosition = iconMapPos;
    }


}
