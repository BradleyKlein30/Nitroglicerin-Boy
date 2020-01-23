using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{


    public void Retry()
    {
        Debug.Log("Retry Level");
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void ExitToHub()
    {
        SceneManager.LoadScene("MainHUB");
    }
}
