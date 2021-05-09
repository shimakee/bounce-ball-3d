using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(OrbitAround))]
public class CameraInputHandler : MonoBehaviour
{
    [SerializeField]
    private bool swapYtoZ;

    private bool _canReadCameraInput;
    private Vector2 _direction;
    private OrbitAround _oa;

    private void Awake()
    {
        _oa = GetComponent<OrbitAround>();

        if (_oa == null)
            throw new NullReferenceException("Orbit around component must not be null");
    }
    // Update is called once per frame
    void Update()
    {
        _oa.Orbit(_direction);
    }

    public void HandleCameraInput(CallbackContext context)
    {
        if (_canReadCameraInput)
        {
            if (context.performed)
                _direction = context.ReadValue<Vector2>().normalized;

            if(context.canceled)
                _direction = context.ReadValue<Vector2>().normalized;
        }
    }

    public void canReadCameraInput(CallbackContext context)
    {
        if (context.started)
        {
            _canReadCameraInput = true;
        }
        if (context.canceled)
        {
            _direction = Vector2.zero;
            _canReadCameraInput = false;
        }

    }
}
