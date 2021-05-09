using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject InitialSpawnpoint;
    [SerializeField]
    private GameObject player;

    public Vector3 respawnPoint { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = InitialSpawnpoint.transform.position;
    }

    public void RespawnPlayer()
    {
        player.transform.position = respawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
