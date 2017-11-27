using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;  

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float zoomScale;

    float maxZoom = 5f;
    float minZoom = 15f;
    float currZoom;

    void Start()
    {
        currZoom = Camera.main.orthographicSize;
        
    }

    //Target's position is updated in 'Updated' func, updating that and camera position will cause jitteriness

    void LateUpdate() //run right after update
    {
        
        transform.position = target.position + offset; //update the camera's position 
    }

    void Update()
    {
        
        if (Input.mouseScrollDelta.y != 0) //mouse scroll I want only affects Y
        {
            Zoom();
        }

        Select();
    }

    //Maybe I don't need a camera follow really?? RAther I don't need an offset!

    //Mouse scroll delta uses positive Y for scrolling up and negative Y for scrolling down
    //Size of camera determines how zoomed in it is, the smaller the size the more zoomed in it is
    //Now to get it to zoom in and out
    void Zoom()
    {
        if(Input.mouseScrollDelta.y > 0 ) //check if already at max zoom-in while mouse scroll up
        {
            if (currZoom >= maxZoom)
            {
                ZoomIn();
            }

        }else if(Input.mouseScrollDelta.y < 0 ) //check if already at min zoom-in while mouse scroll down
        {
            if (currZoom <= minZoom)
            {
                ZoomOut();
            }
        }
        currZoom = Camera.main.orthographicSize;
    }


    void ZoomIn()
    {
        Camera.main.orthographicSize += -zoomScale;
        
    }

    void ZoomOut()
    {
        Camera.main.orthographicSize += zoomScale;
    }

    void Select()
    {
        if(Input.GetButtonDown("Fire1")) //check if mouse is pressed
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            GameObject prevTarget = target.gameObject;
            if(Physics.Raycast(ray, out hit)) //if it hits something, the camera should be set to it and follow it, or not if it's not an item
            {
                if (hit.transform.gameObject.tag == "Item")
                {

                    target = hit.transform;
                    prevTarget.tag = hit.transform.gameObject.tag; //set it to item except I'm not hardcoding that
                    hit.transform.gameObject.tag = "Player";
                    
                }
                else
                { //what do I set the target to if it doesn't have a transform??
                    transform.position = Input.mousePosition;
                }
            }


        }

    }

    void Deselect()
    {

    }
    
}
