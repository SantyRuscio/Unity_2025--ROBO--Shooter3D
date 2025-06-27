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

    private float _cameraCollisionRadius = 0.3f; 
    private LayerMask _collisionMask = ~0; 

    public CamaraSeguimiento(Transform transformPlayer, Transform cameraTrack, Transform cameraAxis)
    {
        _playerTr = transformPlayer;
        _cameraTrack = cameraTrack;
        _cameraAxis = cameraAxis;
        _theCamera = Camera.main.transform;
    }

    public void ToggleFunctionality(bool isEnable)
    {
        _enable = isEnable;
    }

        float deltaTime = Time.deltaTime; Vector3 finalCameraPos;
    public void CameraLogic(float mouseX, float mouseY)
    {
        if (!_enable) return;


        _rotY += mouseY * deltaTime * _camRotSpeed;
        _rotX += mouseX * deltaTime * _camRotSpeed;
        _rotY = Mathf.Clamp(_rotY, _minAngle, _maxAngle);

        Quaternion targetPlayerRotation = Quaternion.Euler(0, _rotX, 0);
        _playerTr.rotation = Quaternion.Slerp(_playerTr.rotation, targetPlayerRotation, deltaTime * _rotationSmoothness);

        Quaternion targetCameraRotation = Quaternion.Euler(-_rotY, 0, 0);
        _cameraAxis.localRotation = Quaternion.Slerp(_cameraAxis.localRotation, targetCameraRotation, deltaTime * _rotationSmoothness);

        Vector3 desiredCameraPos = _cameraTrack.position;
        Vector3 direction = desiredCameraPos - _cameraAxis.position;
        float distance = direction.magnitude;
        direction.Normalize();

         finalCameraPos = desiredCameraPos;

        if (Physics.SphereCast(_cameraAxis.position, _cameraCollisionRadius, direction, out RaycastHit hit, distance, _collisionMask))
        {
            finalCameraPos = hit.point + hit.normal * 0.05f; // Alejamos ligeramente la cámara del muro
        }

    }

    public void CameraMoving()
    {
        _theCamera.position = Vector3.Lerp(_theCamera.position, finalCameraPos, _cameraSpeed);
        _theCamera.rotation = Quaternion.Slerp(_theCamera.rotation, _cameraTrack.rotation, _cameraSpeed);

    }
}