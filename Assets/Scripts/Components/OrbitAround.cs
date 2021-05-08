using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    [Header("Orbit settings")]
    [Space(10)]

    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private bool isAuto;

    [SerializeField]
    private bool swapZtoY;

    [Header("Position")]
    [SerializeField]
    [Range(0,1)]
    private float smoothFactor = .1f;

    [SerializeField]
    private Vector3 Offset;

    [SerializeField]
    [Range(0, 360)]
    private float degreesPosition;

    [SerializeField]
    private float speed = 2;

    [SerializeField]
    private float radius;

    [Header("Limit")]
    [SerializeField]
    [Range(0, 100)] private float YLimitMin = 0;
    [SerializeField]
    [Range(0, 100)] private float YLimitMax = 7;

    private Vector3 _newPosition;
    private float _time;
    private float _adjustment;
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
            _adjustment = degreesPosition;
        else
            _adjustment = _time;

        AdjustOrbit(_adjustment);

        transform.position = Vector3.Lerp(transform.position, _newPosition + Offset, smoothFactor);
    }

    private void AdjustOrbit(float adjustment)
    {
        _newPosition.x = _newPosition.x + Mathf.Cos(adjustment * Mathf.Deg2Rad) * radius;
        if (swapZtoY)
            _newPosition.z = _newPosition.z + Mathf.Sin(adjustment * Mathf.Deg2Rad) * radius;
        else
            _newPosition.y = _newPosition.y + Mathf.Sin(adjustment * Mathf.Deg2Rad) * radius;
    }

    public void Orbit(Vector2 direction)
    {
        direction = direction.normalized;
        float valueX = degreesPosition + (direction.x * speed);
        if (valueX > 360)
            valueX = 0;
        if (valueX < 0)
            valueX = 360;
        
        degreesPosition = valueX;


        float valueY = Offset.y + (direction.y/2 * -1);
        if (valueY > YLimitMax)
            valueY = YLimitMax;
        if (valueY < (YLimitMin * -1))
            valueY = YLimitMin * -1;

        Offset.y = valueY;
    }
}
