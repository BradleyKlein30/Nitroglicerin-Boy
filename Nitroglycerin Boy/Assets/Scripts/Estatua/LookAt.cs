using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float range = 10;
    public float rotationSpeed = 2f;

    public float fireRate = 1f;
    public float fireCountdown = 0f;

    void Start()
    {

    }

    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= range)
            {
                Vector3 targetDir = target.position - transform.position;
                targetDir.y = 0.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), Time.time * rotationSpeed);

                if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }
            }
            else ResetPosition();
        }
        else ResetPosition();

        

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject fireball = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(fireball, 2f);
    }

    void ResetPosition()
    {
        Quaternion lerpedRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero), 0.1f);
        transform.rotation = lerpedRotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
