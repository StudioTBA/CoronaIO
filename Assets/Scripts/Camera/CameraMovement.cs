using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraMovement : MonoBehaviour
{
    Camera mainCamera = null;
    [SerializeField] float interpolateTime = 5.0f;

    #region Camera Position Vars
    [Header("Camera")]
    [SerializeField] float height = 50;
    [SerializeField] float distance = 50;
    [SerializeField] int FOV = 10;
    [SerializeField] float zoomSpeed = 4.0f;
    #endregion

    #region Parent Movement Vars
    [Header("Handler")]
    [SerializeField] float handlerSpeed = 0.25f;
    Vector3 goalPos = Vector3.zero;
    Vector3 initMouseRot = Vector3.zero;
    Vector3 mousePosDirRot = Vector3.zero;
    [SerializeField] float rotateSpeed = 10.0f;

    Vector3 initMouseDrag = Vector3.zero;
    Vector3 mousePosDirDrag = Vector3.zero;
    [SerializeField] float dragSpeed = 1.5f;

    Quaternion goalRot = Quaternion.identity;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = this.gameObject.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        handleCamPosition();
        handleParentMovement();
    }

    /// <summary>
    /// Takes care of calculating the vector from the handler to where we want the camera to be.
    /// 
    /// Moves camera to that position.
    /// 
    /// Handles Zoom In/Out
    /// </summary>
    void handleCamPosition()
    {
        Vector3 verticalDistance = this.transform.up * height;
        Vector3 horizontalDistance = this.transform.forward * -distance;
        Vector3 handlerPos = this.transform.position;

        Vector3 cameraPos = handlerPos + verticalDistance + horizontalDistance;

        float t = Time.deltaTime * interpolateTime;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPos, t);

        mainCamera.transform.LookAt(handlerPos, Vector3.up);

        mainCamera.fieldOfView = FOV;

        // Zoom
        if (Input.mouseScrollDelta.y != 0)
        {
            float scrollDelta = Input.mouseScrollDelta.y;

            distance += -scrollDelta * zoomSpeed;
            height += -scrollDelta * zoomSpeed;
        }

        // Debug Lines
        //Debug.DrawLine(handlerPos, handlerPos + verticalDistance, Color.red);
        //Debug.DrawLine(handlerPos, handlerPos + horizontalDistance, Color.blue);
        //Debug.DrawLine(handlerPos, cameraPos, Color.green);

    }

    /// <summary>
    /// Handles: WASD/Arrow, Drag, and Rotation Movements
    /// </summary>
    void handleParentMovement()
    {
        // WASD and Arrow movements
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            goalPos += transform.forward * handlerSpeed;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            goalPos += transform.right * -handlerSpeed;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            goalPos += transform.forward * -handlerSpeed;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            goalPos += transform.right * handlerSpeed;
        }

        // Drag Movement
        if (Input.GetMouseButtonDown(2))
        {
            Plane p = new Plane(mainCamera.transform.forward, 0f);
            Ray r = mainCamera.ScreenPointToRay(Input.mousePosition);

            float distanceToPlane;

            if (p.Raycast(r, out distanceToPlane))
            {
                initMouseDrag = r.GetPoint(distanceToPlane);
            }
        }
        if (Input.GetMouseButton(2))
        {
            Plane p = new Plane(mainCamera.transform.forward, 0f);
            Ray r = mainCamera.ScreenPointToRay(Input.mousePosition);

            float distanceToPlane;

            if (p.Raycast(r, out distanceToPlane))
            {
                Vector3 currentPos = r.GetPoint(distanceToPlane);
                mousePosDirDrag = initMouseDrag - currentPos;
                mousePosDirDrag.Normalize();

                goalPos += new Vector3(mousePosDirDrag.x, 0, mousePosDirDrag.z) * dragSpeed;

                initMouseDrag = currentPos;
            }
        }

        // Rotation 
        if (Input.GetMouseButtonDown(1))
        {
            initMouseDrag = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 currentPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
            mousePosDirDrag = currentPos - initMouseDrag;

            Quaternion q = Quaternion.AngleAxis(mousePosDirDrag.x * 180 * rotateSpeed, Vector3.up);

            goalRot = q * transform.rotation;

            initMouseDrag = currentPos;
        }

        // Update Position and Rotation
        float t = Time.deltaTime * interpolateTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, goalRot, t);
        transform.position = Vector3.Lerp(transform.position, goalPos, t);
    }

}
