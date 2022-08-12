using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControls : MonoBehaviour
{
    public float minZoom;
    public float maxZoom;
    public float cameraMoveSpeed = 3;
    public float cameraZoomSpeed = 5;
    private float lastDistance = 0;
    private float distance;
    private Vector2 lastPosition;

    // Update is called once per frame
    void Update()
    {
        //Return if not over game object
        if (EventSystem.current.IsPointerOverGameObject(-1)) return;

        //Moving Camera
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(input * cameraMoveSpeed * Time.deltaTime);
        //Zooming 
        float scrollWheel = Input.mouseScrollDelta.y;
        if (scrollWheel > 0) {
            Camera.main.orthographicSize -= cameraZoomSpeed * Time.deltaTime;
        }
        if (scrollWheel < 0)
        {
            Camera.main.orthographicSize += cameraZoomSpeed * Time.deltaTime;
        }

        if (Input.touchCount == 2)
        {
            Vector2 touch0;
            Vector2 touch1;
            touch0 = Input.GetTouch(0).position;
            touch1 = Input.GetTouch(1).position;
            lastDistance = distance;
            distance = Vector2.Distance(touch0, touch1);
            float distanceDelta = distance - lastDistance;
            if (distanceDelta > 0)
            {
                Camera.main.orthographicSize -= cameraZoomSpeed * Time.deltaTime;
            }
            if (distanceDelta < 0)
            {
                Camera.main.orthographicSize += cameraZoomSpeed * Time.deltaTime;
            }
        }

        else if (Input.touchCount > 2)
        {
            //Panning
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1, 10);
            if (Input.GetMouseButtonDown(1))
            {
                lastPosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(1))
            {
                Vector2 distance = Input.mousePosition - (Vector3)lastPosition;
                transform.Translate(-distance * cameraMoveSpeed * Time.deltaTime);
                lastPosition = Input.mousePosition;
            }
        }

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
    }

    public void Testing() 
    {
        print("testing function");
    }

}
