using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementHandle
{
    private float _horizontalInput, _verticalInput, _movementSpeed, _jumpForce;
    private Vector3 _direction;
    private Transform _transform;
    private Rigidbody _rigidBody;

    public Action<float,float> OnMove;
    public Action OnJump;

    public MovementHandle(float speed, float jumpForce, Transform transform, Rigidbody rigidBody)
    {
        _movementSpeed = speed;
        _transform = transform;
        _rigidBody = rigidBody;
        _jumpForce = jumpForce;

        OnMove += Move;
        OnJump += Jump;
    }

    ~MovementHandle() { }

    public bool IsMoving()
    {
        return _horizontalInput != 0 || _verticalInput != 0;
    }

    public Vector3 GetDirection()
    {
        return _direction;
    }

    public void ChangeSpeed(float newSpeed)
    {
        _movementSpeed = newSpeed;
    }

    private void Move(float horizontal, float vertical)
    {
        _horizontalInput = horizontal;
        _verticalInput = vertical;

        Vector3 directionAngle = _transform.localEulerAngles;
        _transform.localEulerAngles = new Vector3(0, directionAngle.y, 0);

        _direction = (_transform.forward * vertical + _transform.right * horizontal).normalized;

        _transform.localEulerAngles = directionAngle;

        _transform.position += _direction * _movementSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}