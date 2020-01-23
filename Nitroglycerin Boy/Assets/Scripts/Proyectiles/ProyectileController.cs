using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProyectileController : MonoBehaviour
{    
    public float speed = 0.5f;
    public float bulletLifeTime = 1f;
    public float explosionEffectTime = 3f;

    public GameObject explosionEffect;

    [Header("AUDIOCLIPS")]
    public AudioClip explosionSound;

    [Header("COMPONENTS")]
    public GameObject door;
    public AudioSource audio;

    void Start()
    {
        Invoke("Destroy", bulletLifeTime);
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    private void PlayExplosionSound()
    {
        audio.clip = explosionSound;
        audio.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CanExplode")
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            PlayExplosionSound();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 9)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            PlayExplosionSound();
            Destroy(gameObject);
        }

    }
}
