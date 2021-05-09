using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody))]
public class DashInputHandler : MonoBehaviour
{
    [SerializeField]
    private LayerMask collisionMask;

    [SerializeField]
    [Range(0, 100)]
    private float distance = 5;

    [SerializeField]
    [Range(0, 100)]
    private float distanceDelta = .5f;

    [SerializeField]
    [Range(0, 30)]
    private float cooldown = 5f;

    private bool _isDashing { get; set; }
    private bool _canDash { get; set; } = true;
    private Vector3 _direction { get; set; }
    private Rigidbody _rb { get; set; }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (_rb == null)
            throw new NullReferenceException("Rigidbody cannot be null");
    }

    private void FixedUpdate()
    {
        if(_isDashing)
            Dash();
    }

    public void HandleDashInput(CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 direction = _rb.velocity;
            direction.y = 0;
            _direction = direction;

            if(_canDash == true)
                StartCoroutine(DashCaller(cooldown));
        }
    }

    private IEnumerator DashCaller(float cooldown)
    {
        _isDashing = true;
        _canDash = false;
        Vector3 startPosition = _rb.position;
        yield return new WaitUntil(()=> (startPosition - _rb.position).magnitude >= distance);
        _isDashing = false;
        _direction = Vector3.zero;
        yield return new WaitForSeconds(cooldown);
        _canDash = true;
    }

    private void Dash()
    {
        Vector3 desiredPosition = Vector3.MoveTowards(_rb.position, _rb.position + _direction.normalized * distance, distanceDelta);
        RaycastHit hitInfo;
        bool hasCollision = Physics.Raycast(_rb.position, desiredPosition - _rb.position, out hitInfo, distance, collisionMask);
        if (!hasCollision)
            _rb.position = desiredPosition;
        else
        {
            _rb.position = (_rb.position - hitInfo.point) / 2 + hitInfo.point;
        }
    }
}
