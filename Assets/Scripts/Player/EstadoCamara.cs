using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    Transform playerTr;
    public Transform cameraAxis;

    public Transform cameraTrack;

    private Transform theCamera;

    private float rotY = 0f;

    private float rotX = 0f;

    public float camRotSpeed = 200f;

    public float minAngle = -45f;

    public float maxAngle = 45f;

    public float cameraSpeed = 5f; 

    public float rotationSmoothness = 10f; 

    void Start()
    {
        playerTr = this.transform;

        theCamera = Camera.main.transform;
    }

    void LateUpdate()
    {
        CameraLogic();
    }

    public void CameraLogic()
    {
        float mouseX = Input.GetAxis("Mouse X");

        float mouseY = Input.GetAxis("Mouse Y");

        float theTime = Time.deltaTime;

        rotY += mouseY * theTime * camRotSpeed;

        rotX += mouseX * theTime * camRotSpeed;

        rotY = Mathf.Clamp(rotY, minAngle, maxAngle);

        Quaternion targetPlayerRotation = Quaternion.Euler(0, rotX, 0);

        playerTr.rotation = Quaternion.Slerp(playerTr.rotation, targetPlayerRotation, theTime * rotationSmoothness);

        Quaternion targetCameraRotation = Quaternion.Euler(-rotY, 0, 0);

        cameraAxis.localRotation = Quaternion.Slerp(cameraAxis.localRotation, targetCameraRotation, theTime * rotationSmoothness);

        theCamera.position = Vector3.Lerp(theCamera.position, cameraTrack.position, theTime * cameraSpeed);

        theCamera.rotation = Quaternion.Slerp(theCamera.rotation, cameraTrack.rotation, theTime * cameraSpeed);
    }
}


