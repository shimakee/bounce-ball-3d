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

    public bool HasKey;

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

    public static event Action<int> ShootTriggerEnter;
    public static event Action<int> ShootTriggerLeave;


    public static void OnShootTriggerEnter(int id)
    {
        ShootTriggerEnter?.Invoke(id);
    }
    public static void OnShootTriggerLeave(int id)
    {
        ShootTriggerLeave?.Invoke(id);
    }

    public static event Action<int> DoorTriggerEnter;
    public static event Action<int> DoorTriggerLeave;

    public static void OnDoorTriggerEnter(int id)
    {
        DoorTriggerEnter?.Invoke(id);
    }
    public static void OnDoorTriggerLeave(int id)
    {
        DoorTriggerLeave?.Invoke(id);
    }

    public void OnKeyGet()
    {
        HasKey = true;
    }
}
