using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
    [SerializeField]
    private int Myid;
    [SerializeField]
    private GameObject ObjectToShoot;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    [Range(0,100)]
    private float shootSpeed = 10;
    [SerializeField]
    [Range(0, 100)]
    private float ShootInterval = 2f;

    private bool isEnabled = false;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.ShootTriggerEnter += ShootObject;
        GameManager.ShootTriggerLeave += ShootObjectStop;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShootObject(int id)
    {
        if (id == Myid && !isEnabled)
        {

            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        isEnabled = true;
        while (true)
        {
            GameObject objectToshoot = GameObject.Instantiate(ObjectToShoot);
            Objectkiller killer = objectToshoot.GetComponent<Objectkiller>();
            killer.gm = gm;
            Rigidbody objectRb = objectToshoot.GetComponent<Rigidbody>();
            if(objectRb != null)
            {
                objectRb.position = this.transform.position;
                objectRb.velocity = this.transform.forward * shootSpeed;
            }
            yield return new WaitForSeconds(ShootInterval);
        }
    }

    private void ShootObjectStop(int id)
    {
        if (id == Myid && isEnabled)
        {
            StopCoroutine(Shoot());
        }
    }
}
