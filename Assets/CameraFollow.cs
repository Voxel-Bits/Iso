using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;  

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    //Target's position will be updated in 'Updated' func, updating that and camera position will cause jitteriness

    void LateUpdate() //run right after update
    {

        transform.position = target.position + offset;
    }

    //Maybe I don't need a camera follow really?? RAther I don't need an offset!

    //Now to 
}
