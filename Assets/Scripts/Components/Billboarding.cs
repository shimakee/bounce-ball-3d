using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private bool clampX;
    [SerializeField]
    private bool clampY;
    [SerializeField]
    private bool clampZ;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetObject.transform.position);

        var x = transform.rotation.eulerAngles.x;
        var y = transform.rotation.eulerAngles.y;
        var z = transform.rotation.eulerAngles.z;

        if (clampX)
            x = 0;
        if (clampY)
            y = 0;
        if (clampZ)
            z = 0;

        transform.rotation = Quaternion.Euler(x, y, z);
    }
}
