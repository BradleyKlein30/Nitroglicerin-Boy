using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileSpawner : MonoBehaviour
{
    public GameObject bulletEmitter;
    public GameObject bullet;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            GameObject Temporary_Bullet_Handler = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation); //as GameObject;
        }
    }
}
