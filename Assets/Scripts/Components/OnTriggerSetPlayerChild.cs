using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerSetPlayerChild : MonoBehaviour
{
    private GameObject child;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(this.transform);
            child = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(null);
            child = null;
        }
    }
}
