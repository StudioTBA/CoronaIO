using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPicHandler : MonoBehaviour
{
    Camera miniMapCamera;
    MiniMapAndWorldHelper mapHelper;
    UnityEngine.UI.RawImage mapImg;
    // Start is called before the first frame update
    void Start()
    {
        miniMapCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        mapHelper = GameObject.Find("MiniMapManager").GetComponent<MiniMapAndWorldHelper>();
        mapImg = GameObject.Find("MiniMap").GetComponent<UnityEngine.UI.RawImage>();

        StartCoroutine("takePictureAndSetTexture");
    }

    IEnumerator takePictureAndSetTexture()
    {

        yield return new WaitForEndOfFrame();

        // Set Ortrhographic Size
        miniMapCamera.orthographicSize = mapHelper.WorldSize / 2.03f;

        // Taking care of the scene light
        GameObject sceneLight = GameObject.Find("Directional Light");
        Quaternion originalRot = sceneLight.transform.rotation;
        sceneLight.transform.rotation = Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
        sceneLight.GetComponent<Light>().intensity = 0.7f;

        // Taking a "picture"
        RenderTexture map = RenderTexture.active;
        RenderTexture.active = miniMapCamera.targetTexture;

        miniMapCamera.Render();

        Texture2D image = new Texture2D(miniMapCamera.targetTexture.width, miniMapCamera.targetTexture.height);
        Rect rect = new Rect(0, 0, miniMapCamera.targetTexture.width, miniMapCamera.targetTexture.height);
        image.ReadPixels(rect, 0, 0);
        image.Apply();
        RenderTexture.active = map;

        byte[] bytes = image.EncodeToPNG();
        Destroy(image);

        System.IO.File.WriteAllBytes(Application.dataPath + "/MiniMap/MapPic.png", bytes);

        // Reseting Light
        sceneLight.transform.rotation = originalRot;
        sceneLight.GetComponent<Light>().intensity = 1.0f;

        setMapImg();

        Destroy(miniMapCamera);
    }

    void setMapImg()
    {
        Texture2D map = new Texture2D(2, 2);
        byte[] bytes = System.IO.File.ReadAllBytes(Application.dataPath + "/MiniMap/MapPic.png");
        map.LoadImage(bytes);

        mapImg.texture = map;
    }
}
