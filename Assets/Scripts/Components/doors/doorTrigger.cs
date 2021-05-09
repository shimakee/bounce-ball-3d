using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTrigger : MonoBehaviour
{
    [SerializeField]
    private int _id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameManager.OnDoorTriggerEnter(_id);
    }
}
