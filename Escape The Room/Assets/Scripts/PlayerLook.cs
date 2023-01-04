using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] bool useXRotation;
    [SerializeField] bool useYRotation;
    [SerializeField] [Range(1f, 20f)] float rotationXSpeed = 10f;
    [SerializeField] [Range(1f, 20f)] float rotationYSpeed = 10f;
    [SerializeField] [Range(-90f, -1f)] float minXAngle = -60f;
    [SerializeField] [Range(1f, 90f)] float maxXAngle = 60f;

    private void FixedUpdate()
    {
        if (MasterManager.main.gameManager.UseMoveAndLook) Look();
    }

    void Look()
    {
        if (useXRotation && MasterManager.main.inputManager.LookValue.y != 0f)
        {
            float step = rotationXSpeed * Mathf.Abs(MasterManager.main.inputManager.LookValue.y) * Time.deltaTime;
            float newXAngle = MasterManager.main.inputManager.LookValue.y > 0 ? minXAngle : maxXAngle;
            Quaternion currentRotation = transform.localRotation;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(newXAngle, currentRotation.y, currentRotation.z));

            transform.localRotation = Quaternion.RotateTowards(currentRotation, targetRotation, step);
        }

        if(useYRotation && MasterManager.main.inputManager.LookValue.x != 0f)
        {
            transform.Rotate(Vector3.up, rotationYSpeed * Time.deltaTime * MasterManager.main.inputManager.LookValue.x);
        }
    }
}
