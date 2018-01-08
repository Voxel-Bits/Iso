using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Class for camera follow behavoir, such as zooming in and out, following the mouse, and changing what object the camera is focused on.
/// </summary>
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

    /// <summary>
    /// Sets the current zoom level to whatever the size of the ortho camera is, and the mouse is not following anything by default. 
    /// </summary>
    void Start()
    {
        currZoom = Camera.main.orthographicSize;
        followMouse = false;
        
    }


    /// <summary>
    /// Update the camera's position . This func is run right after update.
    /// Target's position is updated in 'Updated' func, updating that and camera position will cause jitteriness
    /// </summary>
    void LateUpdate() 
    {
        if (!followMouse)
        {
            transform.position = target.position + offset; 
        }
    }
    /// <summary>
    /// Zoom the camera in, change target, and/or follow the mouse if needed.
    /// </summary>
    void Update()
    {
        
        if (Input.mouseScrollDelta.y != 0) //mouse scroll I want only affects Y
        {
            Zoom();
        }

        ChangeTarget();

        FollowMouse();
    }


    /// <summary>
    /// Function for zooming in the camera. Size of camera determines how zoomed in it is, 
    /// the smaller the size the more zoomed in, the bigger the more zoomed out.
    /// </summary>
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


    /// <summary>
    /// Helper function for zooming in. Mouse scroll delta uses positive Y for scrolling up and negative Y for scrolling down.
    /// </summary>
    void ZoomIn()
    {
        Camera.main.orthographicSize += -zoomScale;
        
    }


    /// <summary>
    /// Helper function for zooming out. Mouse scroll delta uses positive Y for scrolling up and negative Y for scrolling down.
    /// </summary>
    void ZoomOut()
    {
        Camera.main.orthographicSize += zoomScale;
    }

    /// <summary>
    /// Function for changing the target of the camera, the camera will now follow whatever was last clicked.
    /// </summary>
    void ChangeTarget()
    {
        if(Input.GetButtonDown("Fire1")) //check if mouse is pressed
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            GameObject prevTarget = target.gameObject;
            if(Physics.Raycast(ray, out hit)) //if it hits something, the camera should be set to it and follow it, or not if it's not an item
            {
                if (hit.transform.gameObject.tag == "Item") //if I hit an item, select it
                {

                    target = hit.transform;
                    target.gameObject.GetComponent<CharController>().Select();
                    prevTarget.GetComponent<CharController>().Deselect();
                    followMouse = false; //the camera is no longer following the mouse, it's following the char

                    
                    
                }
                else //otherwise, deselect anything that was already selected
                {
                    target.gameObject.GetComponent<CharController>().Deselect();
                }
            }


        }

    }

    /// <summary>
    /// Function for the camera to follow the mouse if it's in any of the corners or edges of the screen, even if something is selected (it will remain selected).
    /// </summary>
    void FollowMouse()
    {

        //check if the mouse is in any of the extremes of the game screen
        //The precompile messages are needed because Unity doesn't change the screen resolution variables while in editor mode
#if UNITY_EDITOR
        if (Input.mousePosition.x == 0 || Input.mousePosition.y == 0 || 
            Input.mousePosition.x >= Handles.GetMainGameViewSize().x - 5 || Input.mousePosition.y >= Handles.GetMainGameViewSize().y - 5)
        {
 //           followMouse = true;

            if(Input.mousePosition.x <= 5)//if mouse is in the left side of the window 
            {
                transform.position -= new Vector3(Time.deltaTime * horizontalSpeed, 0.0f, 0.0f); //I don't need input axis because I don't need a delta for how much the mouse has moved from prev position
            }

            if(Input.mousePosition.x >= Handles.GetMainGameViewSize().x - 5)
            {
                transform.position += new Vector3(Time.deltaTime * horizontalSpeed, 0.0f, 0.0f);
            }
            if(Input.mousePosition.y <= 5) //TODO: this needs to move the camera halfway between z and y axes
            {
                transform.position -= new Vector3(0.0f, Time.deltaTime * verticalSpeed, 0.0f );
                Debug.Log("The mouse is in the bottom of the game screen");
            }
            if(Input.mousePosition.y >= Handles.GetMainGameViewSize().y - 5)
            {
                transform.position += new Vector3(0.0f, Time.deltaTime * verticalSpeed, 0.0f);
            }
//            Debug.Log("Camera position: " + "X = " + transform.position.x + " Y = " + transform.position.y + " Z = " + transform.position.z);
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
