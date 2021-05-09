using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keygetter : MonoBehaviour
{
    [SerializeField]
    private GameManager gm;


    private void OnTriggerEnter(Collider other)
    {
        gm.OnKeyGet();
        Destroy(this.gameObject);
    }
}
