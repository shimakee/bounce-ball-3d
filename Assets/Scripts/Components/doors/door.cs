using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField]
    private int _id;


    private void Start()
    {
        GameManager.DoorTriggerEnter += OpenDoor;
    }

    private void OpenDoor(int id)
    {
        if(id == _id)
        {
            this.gameObject.SetActive(false);
        }
    }
}
