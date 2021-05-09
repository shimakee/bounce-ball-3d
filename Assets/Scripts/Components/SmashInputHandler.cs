using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody), typeof(JumpInputHandler))]
public class SmashInputHandler : MonoBehaviour
{
    [SerializeField]
    private LayerMask collisionMask;
    [SerializeField]
    [Range(0, 1)]
    private float distanceDelta = .01f;
    [SerializeField]
    [Range(0, 10)]
    private float threshhold = 2f;
    [SerializeField]
    [Range(0, 30)]
    private float cooldown = 0f;
    [SerializeField]
    [Range(0, 10)]
    private float contactRadius = 2f;

    private bool _isSmashing { get; set; }
    private bool _canSmash { get; set; } = true;
    private Vector3 _desiredPosition { get; set; }
    private Rigidbody _rb { get; set; }
    private JumpInputHandler _jumpHandler { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _jumpHandler = GetComponent<JumpInputHandler>();

        if (_rb == null)
            throw new NullReferenceException("Rigidbody cannot be null");
        if (_jumpHandler == null)
            throw new NullReferenceException("Jump handler cannot be null");
    }

    private void FixedUpdate()
    {
        if (_isSmashing)
            Smash();
    }

    public void HandleSmashInput(CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(_rb.velocity.y);
            float yVelocity = 0 - _rb.velocity.y;
            yVelocity = (yVelocity * yVelocity) / 2;
            if (_canSmash == true && !_jumpHandler.IsGrounded && yVelocity <= threshhold)
            {
                Debug.Log("Smashing");
                StartCoroutine(SmashCaller(cooldown));
            }
        }
    }

    private IEnumerator SmashCaller(float cooldown)
    {
        _isSmashing = true;
        _canSmash = false;
        RaycastHit hitInfo;
        Physics.Raycast(_rb.position, Vector3.down, out hitInfo, collisionMask);
        _desiredPosition = hitInfo.point;
        yield return new WaitUntil(() => (_jumpHandler.IsGrounded));
        _jumpHandler.IsGrounded = false;
        _isSmashing = false;
        yield return new WaitForSeconds(cooldown);
        _canSmash = true;
    }

    private void Smash()
    {
        Vector3 desiredPosition = Vector3.MoveTowards(_rb.position, _desiredPosition, distanceDelta);
        RaycastHit hitInfo;
        bool hasCollision = Physics.Raycast(_rb.position, desiredPosition - _rb.position, out hitInfo, int.MaxValue, collisionMask);
        if (!hasCollision)
            _rb.position = desiredPosition;
        else
        {
            _rb.position = (_rb.position - hitInfo.point) / 2 + hitInfo.point;
            DestroyObjects();
        }
    }

    private void DestroyObjects()
    {
        Collider[] collided = Physics.OverlapSphere(_rb.position, contactRadius, collisionMask);

        if(collided.Length > 0)
        {
            foreach (var collision in collided)
            {
                if (collision.tag == "Destructables")
                    Destroy(collision.gameObject);
            }
        }
    }
}
