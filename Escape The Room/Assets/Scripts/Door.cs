using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : SecondaryCamCaller
{
    public bool IsOpen { get; private set; }
    [Space]
    [Header("Animation")]
    [SerializeField] Animator doorAnimator;
    [SerializeField] AudioClip doorOpenClip;

    public void Open()
    {
        if (IsOpen) return;

        //Call the secondary camera so it sees the door
        CallCamera();

        //Open the door
        StartCoroutine(RunOpenAnimation());
    }

    public void PlayOpenClip()
    {
        //Called by an event in the door animation
        MasterManager.Instance.audioManager.playPlayerSound(doorOpenClip);
    }

    IEnumerator RunOpenAnimation()
    {
        //Give a moment for the camera to start moving to its target position and rotation
        yield return new WaitForSeconds(0.1f);

        //Wait until the camera is in position
        yield return new WaitUntil(() => !secondaryCam.IsRotating && !secondaryCam.IsMoving);

        //Trigger the openning animation
        doorAnimator.SetTrigger("Open");
        IsOpen = true;

        //Give a moment for the animation to start
        yield return new WaitForSeconds(0.3f);

        //Wait until the animation ends
        yield return new WaitUntil(() => doorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        //Make a pause before reseting the camera
        yield return new WaitForSeconds(0.2f);

        //Reset the camera
        ResetCamera();
    }
}
