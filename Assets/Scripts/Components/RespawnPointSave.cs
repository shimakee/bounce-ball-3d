using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointSave : MonoBehaviour
{
    
    [SerializeField]
    private GameObject respawnPoint;
    [SerializeField]
    private GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            gm.respawnPoint = respawnPoint.transform.position;
    }
}
