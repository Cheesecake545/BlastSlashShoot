using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    //targer for camera to follow

    public float smoothing = 5.0f;
    //smooth out the camera

    Vector3 offset;
    //offset distance from camera to player

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate() // player moves with physics  so our camera does the same if not it will move in different time
    {
        Vector3 targetCamPos = target.position + offset;
        //set camera position

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        //Lerp move position, currentposition, end position, movement speed * time

    }
}

