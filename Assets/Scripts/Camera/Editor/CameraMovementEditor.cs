using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraMovement))]
public class CameraMovementEditor : Editor
{
    CameraMovement handler;
    SerializedProperty distance;
    SerializedProperty height;

    private void OnEnable()
    {
        handler = target as CameraMovement;
        distance = serializedObject.FindProperty("distance");
        height = serializedObject.FindProperty("height");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        serializedObject.ApplyModifiedProperties();

        Vector3 camPos = handler.transform.position + (-handler.transform.forward * distance.floatValue) + (handler.transform.up * height.floatValue);
        Handles.color = new Color(0, 1, 0, 0.5f);
        Handles.SphereHandleCap(0, camPos, Quaternion.identity, 5.0f, EventType.Repaint);

        Handles.color = new Color(1, 0, 0, 0.2f);
        Handles.DrawSolidDisc(handler.transform.position, Vector3.up, distance.floatValue);

        Handles.color = new Color(0, 1, 0, 1.0f);
        Handles.DrawWireDisc(handler.transform.position, Vector3.up, distance.floatValue);

        Handles.color = new Color(0, 0, 1, 0.5f);
        distance.floatValue = Handles.ScaleSlider(distance.floatValue, handler.transform.position, -handler.transform.forward, Quaternion.identity, distance.floatValue, 1.0f);
        distance.floatValue = Mathf.Clamp(distance.floatValue, 30.0f, float.MaxValue);

        Handles.color = new Color(1, 0, 0, 0.5f);
        height.floatValue = Handles.ScaleSlider(height.floatValue, handler.transform.position, handler.transform.up, Quaternion.identity, height.floatValue, 1.0f);
        height.floatValue = Mathf.Clamp(height.floatValue, 30.0f, float.MaxValue);


        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 15;
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.UpperCenter;
        Handles.Label(handler.transform.position + (-handler.transform.forward * distance.floatValue), "Distance", labelStyle);

        labelStyle.alignment = TextAnchor.MiddleRight;
        Handles.Label(handler.transform.position + (handler.transform.up * height.floatValue), "Height", labelStyle);

        labelStyle.alignment = TextAnchor.UpperCenter;
        Handles.Label(camPos, "Camera", labelStyle);


    }


}