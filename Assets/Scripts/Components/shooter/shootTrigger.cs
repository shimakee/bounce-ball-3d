using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootTrigger : MonoBehaviour
{
    [SerializeField]
    private int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.OnShootTriggerEnter(id);
        Debug.Log("enter");
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.OnShootTriggerLeave(id);
    }
}
