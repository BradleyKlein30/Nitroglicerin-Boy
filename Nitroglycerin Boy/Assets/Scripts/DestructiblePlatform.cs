using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePlatform : MonoBehaviour
{
    Rigidbody rb;
    public float gravityTime;
    public float destroyTime;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject, destroyTime);
            StartCoroutine(Gravity(gravityTime));
        }
    }

    private IEnumerator Gravity(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        rb.useGravity = true;
    }
}
