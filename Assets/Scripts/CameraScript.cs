using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    
    public Vector3 MaxPos;
    public Vector3 MinPos;

    float CameraX;
    float CameraZ;

    void Start() 
    {
        // MaxPos = new Vector3(280, 0, 90);
        // MinPos = new Vector3(-200, 0, -300);    
    }
 
    void Update()
    {
        // dont move if there is a popup
        // if (GameObject.FindGameObjectWithTag("Popup"))return;
        
        // dont move if shift is down
        if (Input.GetKey(KeyCode.LeftShift)) return;



        // mouse event
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
 
        if (!Input.GetMouseButton(0)) return;

        
        // clamp
        // ClampCamera();


        // create camera vectors
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
        

        // move the camera
        transform.Translate(move, Space.World);
    



 
    }




    void ClampCamera()
    {       
        if (Camera.main.transform.position.x >= MaxPos.x)
        {
            Camera.main.transform.position = new Vector3(MaxPos.x , Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        else if (Camera.main.transform.position.x <= MinPos.x)
        {
            Camera.main.transform.position = new Vector3(MinPos.x , Camera.main.transform.position.y, Camera.main.transform.position.z);
        }        
        
        if (Camera.main.transform.position.z >= MaxPos.z)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x , Camera.main.transform.position.y, MaxPos.z);
        }
        else if (Camera.main.transform.position.z <= MinPos.z)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x , Camera.main.transform.position.y, MinPos.z);
        }
    }

}
