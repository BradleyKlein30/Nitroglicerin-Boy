using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueShotSpawn : MonoBehaviour
{
    public GameObject proyectile;
    public Transform canon;
    public float timeToSpawn;

    public LookAt lookAt;

    public Transform statue;

    public bool playerVisible;

    void Start()
    {
        Shoot();
    }

    public void Shoot()
    {
        playerVisible = Physics.CheckSphere(statue.position, lookAt.visionRange);

        if (playerVisible == true)
        {
            GameObject stone = Instantiate(proyectile, canon.position, canon.rotation);

            //para acceder a un componente de un objeto, usamos GetComponent
            //Recuerde comprobar siempre si el retorno no es nulo, antes de usar la variable
            ProyectileController shot = stone.GetComponent<ProyectileController>();

            if (shot != null)
            {
                shot.speed = 0.5f;
            }

            Invoke("Shoot", timeToSpawn);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(statue.position, lookAt.visionRange);
    }
}
