using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControls : MonoBehaviour
{
    public float cameraMoveSpeed = 3;
    public float cameraZoomSpeed = 5;
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
        //Panning
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1, 10);
        if (Input.GetMouseButtonDown(1)) {
            lastPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1)) {
            Vector2 distance = Input.mousePosition - (Vector3)lastPosition;
            transform.Translate(-distance * cameraMoveSpeed * Time.deltaTime);
            lastPosition = Input.mousePosition;
        }
    }

    public void Testing() 
    {
        print("testing function");
    }

}
