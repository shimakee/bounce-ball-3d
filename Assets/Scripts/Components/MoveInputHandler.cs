using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody))]
public class MoveInputHandler : MonoBehaviour
{
    [SerializeField]
    private bool swapYToZ = true;
    [SerializeField]
    [Range(0,100)] private float speed = 10;

    private Vector3 _direction { get; set; }
    private Rigidbody _rb { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (_rb == null)
            throw new NullReferenceException("Rigidbody cannot be null.");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = _direction * speed;
    }


    public void HandleMoveInput(CallbackContext context)
    {

        if (context.performed)
            _direction = context.ReadValue<Vector2>();
        if (context.canceled)
            _direction = context.ReadValue<Vector2>();

        if (swapYToZ)
            _direction = new Vector3(_direction.x, _direction.z, _direction.y);
    }
}
