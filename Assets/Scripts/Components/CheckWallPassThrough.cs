using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CheckWallPassThrough : MonoBehaviour
{
    [SerializeField]
    private LayerMask collisionMask;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (_rb == null)
            throw new NullReferenceException("Rigidbody cannot be null");
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = Vector3.MoveTowards(_rb.position, _rb.position + _rb.velocity, .5f);
        RaycastHit hitInfo;
        bool hasCollision = Physics.Raycast(_rb.position, desiredPosition - _rb.position, out hitInfo, 1, collisionMask);
        if (hasCollision)
            _rb.position = (_rb.position - hitInfo.point) / 2 + hitInfo.point;
    }
}
