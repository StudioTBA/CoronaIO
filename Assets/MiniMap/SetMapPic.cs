using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMapPic : MonoBehaviour
{
    UnityEngine.UI.RawImage mapImg;

    // Start is called before the first frame update
    void Awake()
    {
        mapImg = GetComponent<UnityEngine.UI.RawImage>();
        StartCoroutine("setMapImg");
    }

    IEnumerator setMapImg()
    {
        yield return new WaitForEndOfFrame();

        Texture2D map = new Texture2D(2, 2);
        byte[] bytes = System.IO.File.ReadAllBytes(Application.dataPath + "/MiniMap/MapPic.png");
        map.LoadImage(bytes);

        mapImg.texture = map;
    }
}
