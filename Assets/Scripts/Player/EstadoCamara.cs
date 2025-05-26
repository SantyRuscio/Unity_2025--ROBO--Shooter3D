using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguimiento
{
    private Transform _playerTr;

    private Transform _cameraAxis;

    private Transform _cameraTrack;

    private Transform _theCamera;

    private bool _enable = true;

    private float _rotY = 0f;

    private float _rotX = 0f;

    private float _camRotSpeed = 100f;

    private float _minAngle = -45f;

    private float _maxAngle = 90f;

    private float _cameraSpeed = 15f;

    private float _rotationSmoothness = 100f; 

    public CamaraSeguimiento(Transform transformPlayer, Transform CameraTrack, Transform CameraAxis)
    {
        _playerTr = transformPlayer;

        _cameraTrack = CameraTrack;

        _cameraAxis = CameraAxis;

        _theCamera = Camera.main.transform;
    }

    public void ToggleFunctionality(bool isEnable)
    {
        _enable = isEnable;
    }

    public void CameraLogic(float MouseX, float MouseY)
    {
        if (_enable == false) return;

        float mouseX = MouseX;

        float mouseY = MouseY;

        float theTime = Time.deltaTime;

        _rotY += mouseY * theTime * _camRotSpeed;

        _rotX += mouseX * theTime * _camRotSpeed;

        _rotY = Mathf.Clamp(_rotY, _minAngle, _maxAngle);

        Quaternion targetPlayerRotation = Quaternion.Euler(0, _rotX, 0);

        _playerTr.rotation = Quaternion.Slerp(_playerTr.rotation, targetPlayerRotation, theTime * _rotationSmoothness);

        Quaternion targetCameraRotation = Quaternion.Euler(-_rotY, 0, 0);

        _cameraAxis.localRotation = Quaternion.Slerp(_cameraAxis.localRotation, targetCameraRotation, theTime * _rotationSmoothness);

        _theCamera.position = Vector3.Lerp(_theCamera.position, _cameraTrack.position, theTime * _cameraSpeed);

        _theCamera.rotation = Quaternion.Slerp(_theCamera.rotation, _cameraTrack.rotation, theTime * _cameraSpeed);
    }
}