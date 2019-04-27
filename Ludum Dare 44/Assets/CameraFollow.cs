using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothSpeed;
    public Vector3 offset;

    //public bool freezeY;
    //public float freezeYPosition;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        Vector3 finalPosition = smoothedPosition;
        //if(freezeY)
        //{
        //    finalPosition = new Vector3(smoothedPosition.x, freezeYPosition, smoothedPosition.z);
        //}
        transform.position = finalPosition;
        //GetComponent<SnapToPixelGrid>().Snap();
    }
}
