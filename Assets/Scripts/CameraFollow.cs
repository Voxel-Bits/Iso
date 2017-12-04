using System;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;  

    public float smoothSpeed = 0.125f;
    public float horizontalSpeed = 10f;
    public float verticalSpeed = 10f;
    public Vector3 offset;

    public float zoomScale;

    float maxZoom = 5f;
    float minZoom = 15f;
    float currZoom;

    [HideInInspector]
    public bool followMouse { get; set; }

    void Start()
    {
        currZoom = Camera.main.orthographicSize;
        followMouse = false;
        
    }

    //Target's position is updated in 'Updated' func, updating that and camera position will cause jitteriness

    void LateUpdate() //run right after update
    {
        if (!followMouse)
        {
            transform.position = target.position + offset; //update the camera's position 
        }
    }

    void Update()
    {
        
        if (Input.mouseScrollDelta.y != 0) //mouse scroll I want only affects Y
        {
            Zoom();
        }

        ChangeTarget();

        FollowMouse();
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

    //When you click on an item/char it will be deselcted, and select what you just clicked on. The camera will also now follow what you just clicked on
    void ChangeTarget()
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
                    target.gameObject.GetComponent<CharController>().Select();
                    prevTarget.GetComponent<CharController>().Deselect();
                    
                    
                }
                else
                { //what do I set the target to if it doesn't have a transform??
                    transform.position = Input.mousePosition;
                }
            }


        }

    }

    //the camera will follow the mouse if it's in any of the corners/edges of the game screen, even if something is selected (it will remain selected)
    void FollowMouse()
    {

        //check if the mouse is in any of the extremes of the game screen
        //The precompile messages are needed because Unity doesn't change the screen resolution variables while in editor mode
#if UNITY_EDITOR
        if (Input.mousePosition.x == 0 || Input.mousePosition.y == 0 || 
            Input.mousePosition.x >= Handles.GetMainGameViewSize().x - 5 || Input.mousePosition.y >= Handles.GetMainGameViewSize().y - 5)
        {
            followMouse = true;

            if(Input.mousePosition.x <= 5)//if mouse is in the left side of the window 
            {
                transform.position -= new Vector3(Time.deltaTime * horizontalSpeed, 0.0f, 0.0f); //I don't need input axis because I don't need a delta for how much the mouse has moved from prev position
            }

            if(Input.mousePosition.x >= Handles.GetMainGameViewSize().x - 5)
            {
                transform.position += new Vector3(Time.deltaTime * horizontalSpeed, 0.0f, 0.0f);
            }
            if(Input.mousePosition.y <= 5)
            {
                transform.position -= new Vector3(0.0f, Time.deltaTime * verticalSpeed, 0.0f );
                Debug.Log("The mouse is in the bottom of the game screen");
            }
            if(Input.mousePosition.y >= Handles.GetMainGameViewSize().y - 5)
            {
                transform.position += new Vector3(0.0f, Time.deltaTime * verticalSpeed, 0.0f);
            }
            Debug.Log("Camera position: " + "X = " + transform.position.x + " Y = " + transform.position.y + " Z = " + transform.position.z);
        }
#else
        if(Input.mousePosition.x == 0 || Input.mousePosition.y == 0 || Input.mousePosition.x >= Screen.width - 5 || Input.mousePosition.y >= Screen.height - 5)
        {
            followMouse = true;
            
            if(Input.mousePosition.x <= 5)//if mouse is in the left side of the window 
            {
                transform.position -= new Vector3(Time.deltaTime * horizontalSpeed, 0.0f, 0.0f); //I don't need input axis because I don't need a delta for how much the mouse has moved from prev position
            }

            if(Input.mousePosition.x >= Screen.width - 5)
            {
                transform.position += new Vector3(Time.deltaTime * horizontalSpeed, 0.0f, 0.0f);
            }
            if(Input.mousePosition.y <= 5)
            {
                transform.position -= new Vector3(0.0f, Time.deltaTime * verticalSpeed, 0.0f );
                Debug.Log("The mouse is in the bottom of the game screen");
            }
            if(Input.mousePosition.y >= Screen.height - 5)
            {
                transform.position += new Vector3(0.0f, Time.deltaTime * verticalSpeed, 0.0f);
            }

        }
#endif
    }
}
