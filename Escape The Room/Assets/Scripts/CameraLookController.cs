using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookController : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float rotationSpeed = 5.0f;
    [SerializeField] [Range(-90f, -1f)] float minAngle = -60.0f;
    [SerializeField] [Range(1f, 90f)] float maxAngle = 60.0f;

    private void FixedUpdate()
    {
        if (MasterManager.main.gameManager.UseMoveAndLook) Look();
    }

    void Look()
    {
        if (MasterManager.main.inputManager.LookValue.y != 0f)
        {
            float step = rotationSpeed * Mathf.Abs(MasterManager.main.inputManager.LookValue.y) * Time.deltaTime;
            float newAngle = MasterManager.main.inputManager.LookValue.y > 0 ? minAngle : maxAngle;
            Quaternion currentRotation = transform.localRotation;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(newAngle, currentRotation.y, currentRotation.z));

            transform.localRotation = Quaternion.RotateTowards(currentRotation, targetRotation, step);
        }
    }
}
