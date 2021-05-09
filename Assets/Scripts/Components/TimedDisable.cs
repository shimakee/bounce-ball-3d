using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDisable : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private float timerDurationDisappear = 3f;

    [SerializeField]
    [Range(0, 100)]
    private float timerDurationAppear = 2f;

    [SerializeField]
    private Material material;

    private MeshCollider collider;
    private MeshRenderer renderer;
    private bool hasStarted;

    private void Awake()
    {
        collider = GetComponent<MeshCollider>();
        renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        //material.color = Color.green;
        renderer.material.color = Color.green;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Player" && !hasStarted)
            StartCoroutine(DisablePlatform());
    }

    private IEnumerator DisablePlatform()
    {
        hasStarted = true;
        renderer.material.color = Color.yellow;
        yield return new WaitForSeconds(timerDurationDisappear/2);
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(timerDurationDisappear / 2);
        collider.enabled = false;
        renderer.enabled = false;
        //this.gameObject.SetActive(false);
        yield return new WaitForSeconds(timerDurationAppear);
        //this.gameObject.SetActive(true);
        renderer.material.color = Color.green;
        collider.enabled = true;
        renderer.enabled = true;
        hasStarted = false;
    }
}
