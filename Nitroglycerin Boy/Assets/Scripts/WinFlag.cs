using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinFlag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Canvas/Timer Text").SendMessage("Finish");
        GameObject.Find("Canvas/Timer Text").SendMessage("Win");
    }
}
