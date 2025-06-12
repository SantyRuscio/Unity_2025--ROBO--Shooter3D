using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

        Vector3 forward = _transform.forward;
        Vector3 right = _transform.right;
        _direction = (forward * vertical + right * horizontal).normalized;

        Vector3 currentVelocity = _rigidBody.velocity;
        Vector3 targetVelocity = _direction * _movementSpeed;
        targetVelocity.y = currentVelocity.y;

        _rigidBody.velocity = targetVelocity;
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}