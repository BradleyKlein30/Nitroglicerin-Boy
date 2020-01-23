using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour
{
    public string levelName;

    public void OnTriggerEnter(Collider other)
    {
        ChangeScene(levelName);
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
