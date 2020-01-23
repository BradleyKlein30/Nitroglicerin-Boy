using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public float visionRange = 10;

    public StatueShotSpawn statueShotSpawn;

    void Start()
    {

    }

    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= visionRange)
            {
                transform.LookAt(target);
            }
            else ResetPosition();
        }
        else ResetPosition();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player visible");
            statueShotSpawn.playerVisible = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player not visible");
            statueShotSpawn.playerVisible = false;
        }
    }

    void ResetPosition()
    {
        Quaternion lerpedRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero), 0.2f);
        transform.rotation = lerpedRotation;
    }
}
