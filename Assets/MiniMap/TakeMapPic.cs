using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeMapPic : MonoBehaviour
{
    GameObject sceneLight;
    // Start is called before the first frame update
    void Start()
    {
        sceneLight = GameObject.Find("Directional Light");
        getMiniMapTexture();
    }

    private void getMiniMapTexture()
    {
        // Taking care of the scene light
        Quaternion originalRot = sceneLight.transform.rotation;
        sceneLight.transform.rotation = Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
        sceneLight.GetComponent<Light>().intensity = 0.7f;

        // Taking a "picture"
        Camera miniMapCam = this.GetComponent<Camera>();
        RenderTexture map = RenderTexture.active;
        RenderTexture.active = miniMapCam.targetTexture;

        miniMapCam.Render();

        Texture2D image = new Texture2D(miniMapCam.targetTexture.width, miniMapCam.targetTexture.height);
        Rect rect = new Rect(0, 0, miniMapCam.targetTexture.width, miniMapCam.targetTexture.height);
        image.ReadPixels(rect, 0, 0);
        image.Apply();
        RenderTexture.active = map;

        byte[] bytes = image.EncodeToPNG();
        Destroy(image);

        System.IO.File.WriteAllBytes(Application.dataPath + "/MiniMap/MapPic.png", bytes);

        // Reseting Light
        sceneLight.transform.rotation = originalRot;
        sceneLight.GetComponent<Light>().intensity = 1.0f;

        Destroy(gameObject);
    }
}
