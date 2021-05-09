using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Objectkiller : MonoBehaviour
{
    public GameManager gm;
    [SerializeField]
    [Range(0, 100)]
    private float deathTimer = 10f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(DeathCountdown());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.RespawnPlayer();
            Destroy(this.gameObject);
        }

    }

    private IEnumerator DeathCountdown()
    {
        yield return new WaitForSeconds(deathTimer);
        Destroy(this.gameObject);
    }
}
