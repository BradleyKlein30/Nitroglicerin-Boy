using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class ProyectileController : MonoBehaviour
{    
    public float speed = 0.5f;
    public float bulletLifeTime = 1f;
    public float explosionEffectTime = 3f;

    public GameObject explosionEffect;

    void Start()
    {
        Invoke("Destroy", bulletLifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CanExplode")
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Destroy(explosion, 2);
        }

        if (collision.gameObject.layer == 9)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 2);
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Game Over");
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

    }
}
