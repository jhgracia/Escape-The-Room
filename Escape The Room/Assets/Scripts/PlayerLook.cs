using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    InputManager inputManager;
    PlayerController playerController;

    [SerializeField] bool useXRotation;
    [SerializeField] bool useYRotation;
    [SerializeField] [Range(1f, 20f)] float rotationXSpeed = 10f;
    [SerializeField] [Range(1f, 20f)] float rotationYSpeed = 10f;
    [SerializeField] [Range(-90f, -1f)] float minXAngle = -60f;
    [SerializeField] [Range(1f, 90f)] float maxXAngle = 60f;

    void Start()
    {
        inputManager = GameObject.Find("Input Manager").GetComponent<InputManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        Look();
    }

    void Look()
    {
        if (playerController.IsInteracting) return;

        if (useXRotation && inputManager.LookValue.y != 0f)
        {
            float step = rotationXSpeed * Mathf.Abs(inputManager.LookValue.y) * Time.deltaTime;
            float newXAngle = inputManager.LookValue.y > 0 ? minXAngle : maxXAngle;
            Quaternion currentRotation = transform.localRotation;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(newXAngle, currentRotation.y, currentRotation.z));

            transform.localRotation = Quaternion.RotateTowards(currentRotation, targetRotation, step);
        }

        if(useYRotation && inputManager.LookValue.x != 0f)
        {
            transform.Rotate(Vector3.up, rotationYSpeed * Time.deltaTime * inputManager.LookValue.x);
        }
    }
}
