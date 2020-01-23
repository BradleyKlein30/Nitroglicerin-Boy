using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [Header("COMPONENTS")]
    public GameObject door;
    public AudioSource audio;
    public Animator doorAnim;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        doorAnim = door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Proyectile")
        {
            doorAnim.SetBool("Open", true);
        }
    }
}
