using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField]
    private GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gm.RespawnPlayer();
        }
            
    }
}
