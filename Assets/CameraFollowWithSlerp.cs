using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.ScrollRect;

public class CameraFollowWithSlerp : MonoBehaviour
{
    public Transform target;
    public float movementTime = 1;
    public float rotationSpeed = 0.8f;
    public Vector3 currentSpeed;
    public Vector3 CameraPosOffset = new Vector3(0, 5, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            return;
        //Interpolate Position

        //Interpolate Rotation
        transform.SetPositionAndRotation(
            Vector3.SmoothDamp(transform.position, target.position + CameraPosOffset, ref currentSpeed, movementTime), 
            Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime)
        );
    }
}

