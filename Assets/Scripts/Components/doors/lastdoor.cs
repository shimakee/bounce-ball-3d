using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastdoor : MonoBehaviour
{
    [SerializeField]
    private int _id;
    [SerializeField]
    private GameManager gm;


    private void Start()
    {
        GameManager.DoorTriggerEnter += OpenDoor;
    }

    private void OpenDoor(int id)
    {
        if (id == _id && gm.HasKey)
        {
            this.gameObject.SetActive(false);
        }
    }
}
