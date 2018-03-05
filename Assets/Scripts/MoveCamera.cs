using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
   

    public float turnSpeed = 4.0f;   
    public float panSpeed = 4.0f;       
    public float zoomSpeed = 4.0f;     

    private Vector3 mouseOrigin;    
    private bool isPanning;     
    private bool isRotating;    
    private bool isZooming;     
    float xAxisValue;
    float zAxisValue;


    void Update()
    {

        //Get the Arrows keys
        xAxisValue = Input.GetAxis("Horizontal");
        zAxisValue = Input.GetAxis("Vertical");

        //Move the Camera on it's X-Z plane 
        if (Camera.current != null)
        {
            Camera.current.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
        }


        // Get the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        // Get the right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }

        // Get the middle mouse button
        if (Input.GetMouseButtonDown(2))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isZooming = true;
        }

        // Disable movements on button release
        if (!Input.GetMouseButton(0)) isRotating = false;
        if (!Input.GetMouseButton(1)) isPanning = false;
        if (!Input.GetMouseButton(2)) isZooming = false;

        // Rotate camera along X and Y axis
        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
        }

        // Move the camera on it's XY plane
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            transform.Translate(move, Space.Self);
        }

        // Move the camera linearly along Z axis
        if (isZooming)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = pos.y * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);
        }
    }
}