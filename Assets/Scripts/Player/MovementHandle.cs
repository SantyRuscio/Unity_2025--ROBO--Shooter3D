using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

//Codigo por: Berola, Ruscio, Beghin
public class MovementHandle
{
    private float _horizontalInput, _verticalInput, _movementSpeed, _jumpForce;
    private Vector3 _direction;
    private Transform _transform;
    private Rigidbody _rigidBody;

    public MovementHandle(float speed, float jumpForce, Transform transform, Rigidbody rigidBody, MovAndStamina movAndStamina)
    {
        _movementSpeed = speed;
        _transform = transform;
        _rigidBody = rigidBody;
        _jumpForce = jumpForce;

        movAndStamina.OnMove += Move;
        movAndStamina.OnJump += Jump;
    }

    ~MovementHandle() { }

 // public void MoveCall(float horizontal, float vertical)
 // {
 //     Move(horizontal, vertical);
 // }
 // public void JumpCall()
 // {
 //     Jump();
 // }

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

        Vector3 direction = (_transform.forward * vertical + _transform.right * horizontal).normalized;

        _transform.localEulerAngles = directionAngle;

        Vector3 velocity = direction * _movementSpeed;

        _rigidBody.velocity = new Vector3(velocity.x, _rigidBody.velocity.y, velocity.z);

        _direction = direction;
    }
 
    private void Jump()
    {
        _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}