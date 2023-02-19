using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryCam : MonoBehaviour
{
    public Transform mainCam;
    public Transform player;

    public bool IsMoving { get; private set; }
    public bool IsRotating { get; private set; }

    [SerializeField] float camTransitionSpeed;
    [SerializeField] float camRotationSpeed;

    public void SetDefaultPosition()
    {
        //Return the secondary camera to the same position the main camera is and switch to the main camera

        transform.SetParent(player, true);
        StartTransition(mainCam.localPosition, mainCam.localRotation);
        StartCoroutine(SwitchToMainCam());
    }

    public void MoveAndRotate(Vector3 targetPosition, Vector3 lookAtPosition)
    {
        //Switch to the secondary camera and move it to face the object calling it

        Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition - targetPosition);

        MasterManager.Instance.gameManager.SwitchToSecondaryCam();
        transform.SetParent(null, true);
        StartTransition(targetPosition, targetRotation);
    }

    void StartTransition(Vector3 targetPosition, Quaternion targetRotation)
    {
        //Initiate the camera movement and rotation

        IsMoving = true;
        IsRotating = true;
        StartCoroutine(Move(targetPosition));
        StartCoroutine(Rotate(targetRotation));
    }

    float GetStepValue(float speed)
    {
        return speed * Time.deltaTime;
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        //Move the camera to its target position

        //If the camera is set as an object's child, local position is used, else, world position is used

        bool useLocalPosition = transform.parent != null; 
        Vector3 currentPosition = useLocalPosition ? transform.localPosition : transform.position;
        float step = GetStepValue(camTransitionSpeed);

        while (Vector3.Distance(currentPosition, targetPosition) > 0.1f)
        {
            currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, step);

            if (useLocalPosition) transform.localPosition = currentPosition;

            if (!useLocalPosition) transform.position = currentPosition;
            
            yield return null;
        }

        IsMoving = false;
    }

    IEnumerator Rotate(Quaternion targetRotation)
    {
        //Rotate the camera to its target rotation

        //If the camera is set as an object's child, local rotation is used, else, world rotation is used

        bool useLocalRotation = transform.parent != null;
        Quaternion currentRotation = useLocalRotation ? transform.localRotation : transform.rotation;
        float step = GetStepValue(camRotationSpeed);

        while (Quaternion.Angle(currentRotation, targetRotation) > 0.1f)
        {
            currentRotation = Quaternion.RotateTowards(currentRotation, targetRotation, step);

            if (useLocalRotation) transform.localRotation = currentRotation;

            if (!useLocalRotation) transform.rotation = currentRotation;

            yield return null;
        }

        IsRotating = false;
    }

    IEnumerator SwitchToMainCam()
    {
        //Give a moment for the camera to start moving and rotating
        yield return new WaitForSeconds(0.1f);

        //Wait until the camera is in position
        yield return new WaitUntil(() => !IsRotating && !IsMoving);

        //Switch to the main camera
        MasterManager.Instance.gameManager.SwitchToMainCam();
    }
}
