﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParenting : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        col.transform.parent = gameObject.transform;
    }

    void OnTriggerExit(Collider col)
    {
        col.transform.parent = null;
    }
}