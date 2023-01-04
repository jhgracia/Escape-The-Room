using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SecondaryCamCaller : MonoBehaviour
{
    //Every object that needs to be focused by the secondary camera must inherit from this class to call and reset the camera as needed

    [Header("Secondary cam fields")]

    [Tooltip("The secondary camera script")]
    [SerializeField] protected SecondaryCam secondaryCam;

    [Tooltip("Added to this object's position to calculate the camera's target position when is called")]
    [SerializeField] protected Vector3 camPositionOffset;

    [Tooltip("Added to this object's position to calculate the point to which the camera will be looking at when is called")]
    [SerializeField] protected Vector3 camLookAtOffset;

    protected virtual void CallCamera()
    {
        //Moves and rotates the secondary camera to face the object calling it

        /* 
         * The camera's target position is calculated as: transform.position + camPositionOffset
         * The point to which the camera will look at is calculated as: transform.position + camLookAtOffset
         * 
         * transform.position is the position of the game object that is calling the camera
         * 
         * Override the function if these values need to be calculated in a different way
         */

        Vector3 position = transform.position;
        secondaryCam.MoveAndRotate(position + camPositionOffset, position + camLookAtOffset);
    }

    protected virtual void ResetCamera()
    {
        //Moves and rotates the secondary camera to the same position and rotation of the main camera
        secondaryCam.SetDefaultPosition();
    }
}
