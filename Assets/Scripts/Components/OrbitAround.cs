using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    [Header("Orbit settings")]
    [Tooltip("Adjust orbit around a target object")]
    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private Vector3 Offset;
    [Range(0, 360)] [SerializeField]
    private float degreesPosition;
    [SerializeField]
    private bool isAuto;
    [SerializeField]
    private float speed = 2;

    [SerializeField]
    private bool swapZtoY;
    [SerializeField] float radius;

    Vector3 _newPosition;
    float _time;
    // Start is called before the first frame update
    void Start()
    {
        
        _newPosition.x = targetObject.transform.position.x;
        _newPosition.y = targetObject.transform.position.y;
        _newPosition.z = targetObject.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime * speed;

        _newPosition = targetObject.transform.position;
    }

    private void LateUpdate()
    {
        if (!isAuto)
        {
            _newPosition.x = _newPosition.x + Mathf.Cos(degreesPosition * Mathf.Deg2Rad) * radius;
            if (swapZtoY)
                _newPosition.z = _newPosition.z + Mathf.Sin(degreesPosition * Mathf.Deg2Rad) * radius;
            else
                _newPosition.y = _newPosition.y + Mathf.Sin(degreesPosition * Mathf.Deg2Rad) * radius;
        }
        else
        {
            _newPosition.x = _newPosition.x + Mathf.Cos(_time * Mathf.Deg2Rad) * radius;
            if (swapZtoY)
                _newPosition.z = _newPosition.z + Mathf.Sin(_time * Mathf.Deg2Rad) * radius;
            else
                _newPosition.y = _newPosition.y + Mathf.Sin(_time * Mathf.Deg2Rad) * radius;
        }

        transform.position = _newPosition + Offset;
    }
}
