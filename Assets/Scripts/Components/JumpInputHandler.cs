using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody))]
public class JumpInputHandler : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]private float jumpForce = 5;
    [SerializeField]
    [Range(0, 1)] private float groundedCheckerHeight = .3f;
    [SerializeField]
    [Range(0, 1)] private float groundedCheckerSize = .25f;
    [SerializeField]
    private LayerMask groundCollision;
    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    [Range(0, 1)] private float _fallMultiplier = .05f;

    private Rigidbody _rb { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (_rb == null)
            throw new NullReferenceException("Rigidbody cannot be null");
    }

    private void FixedUpdate()
    {
        IsGroundedCheck();
        FallQuicker();
    }

    private void OnDrawGizmos()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y - groundedCheckerHeight, transform.position.z);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, groundedCheckerSize);
    }

    public void HandleJumpInput(CallbackContext context)
    {
        if (context.performed)
        {
            if(context.interaction is HoldInteraction)
            {
                Jump(jumpForce * 1.5f);
            }
            else
            {
                if(_isGrounded)
                    Jump(jumpForce);
            }
        }

    }

    private void Jump(float jumpForce)
    {
        //reseting Y velocity to prevent upward velocity accumulation from bouncy material.
        //this way we can retain bouncyness but still have consistent upward jump
        //otherwise we can get unlimited height on jump if we time jump press right.
        Vector3 velocity = _rb.velocity;
        velocity.y = 0;
        _rb.velocity = velocity;

        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void IsGroundedCheck()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y - groundedCheckerHeight, transform.position.z);
        Collider[] collided = Physics.OverlapSphere(position, groundedCheckerSize, groundCollision);

        if (collided.Length > 0)
            _isGrounded = true;
        else
            _isGrounded = false;
    }

    private void FallQuicker()
    {
        if (_rb.velocity.y < 0)
            _rb.velocity += Vector3.up * Physics.gravity.y * _fallMultiplier;
    }
}
