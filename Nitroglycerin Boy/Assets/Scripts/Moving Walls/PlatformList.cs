using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Platform
{
    public Transform targets;
    [HideInInspector]
    public List<Transform> targetList;
    public float speed;

    public float waitTime; // time until platform goes back
    [HideInInspector]
    public float timeStarted;

    [HideInInspector]
    public int targetToReach;
    [HideInInspector]
    public bool backwards;
}

[System.Serializable]
public class PlatformList
{
    public List<Platform> list;
}