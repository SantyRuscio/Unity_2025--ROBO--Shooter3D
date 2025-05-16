using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandle
{
    private float _horizontalInput, _verticalInput, _movementSpeed, _jumpForce;

    private Vector3 _direction;

    private Transform _transform;

    private Rigidbody _rigidBody;

    public MovementHandle(float speed,float JumpForce, Transform transform, Rigidbody RigidBody)
    {
        _movementSpeed = speed;
        _transform = transform;
        _rigidBody = RigidBody;
        _jumpForce = JumpForce;
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

    public void ChangeSpeed(float NewSpeed)
    {
        _movementSpeed = NewSpeed;
    }

    public void Move(float horizonal, float vertical)
    {
        _horizontalInput = horizonal;
        _verticalInput = vertical;

        Vector3 directionAngle = _transform.localEulerAngles;
        _transform.localEulerAngles = new Vector3(0, _transform.localEulerAngles.y, 0);

        _direction = (_transform.forward * vertical + _transform.right * horizonal).normalized;

        _transform.localEulerAngles = directionAngle;

        _transform.position += _direction * _movementSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}