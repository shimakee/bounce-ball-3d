﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody))]
public class MoveInputHandler : MonoBehaviour
{
    [SerializeField]
    private Camera cameraReference;
    [SerializeField]
    private bool isMovementRelativeToCamera = true;
    [SerializeField]
    [Range(0,1)] private float speed = 1;
    [SerializeField]
    [Range(0, 100)] private float Maxspeed = 4;

    private Vector3 _direction { get; set; }
    private Rigidbody _rb { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (_rb == null)
            throw new NullReferenceException("Rigidbody cannot be null.");

        if (cameraReference == null)
            cameraReference = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector3 direction = _rb.velocity + _direction * speed;
        direction.x = Mathf.Clamp(direction.x, Maxspeed * -1, Maxspeed);
        direction.z = Mathf.Clamp(direction.z, Maxspeed * -1, Maxspeed);
        _rb.velocity = direction;
    }


    public void HandleMoveInput(CallbackContext context)
    {
        if (context.performed)
            _direction = context.ReadValue<Vector2>();
        if (context.canceled)
            _direction = context.ReadValue<Vector2>();

        if (isMovementRelativeToCamera)
        {
            Vector3 forwardDirection = cameraReference.transform.forward * _direction.y;
            forwardDirection.y = 0;
            Vector3 rightDirection = cameraReference.transform.right * _direction.x;
            rightDirection.y = 0;

            _direction = forwardDirection + rightDirection;
            _direction = _direction.normalized;
        }
        else
        {
            _direction = new Vector3(_direction.x, _direction.z, _direction.y);
        }
    }
}
