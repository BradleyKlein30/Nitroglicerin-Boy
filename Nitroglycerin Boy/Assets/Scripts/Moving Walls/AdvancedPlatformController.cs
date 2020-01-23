using UnityEngine;

public class AdvancedPlatformController : MonoBehaviour
{
    [Header("Components")]
    public PlatformList platforms = new PlatformList();

    void Start()
    {
        // Initialize other targets for each child
        for (int i = 0; i < platforms.list.Count; i++)
        {
            // Initial position is start position of the game object
            GameObject initialTransformGO = new GameObject();
            initialTransformGO.transform.position = transform.GetChild(i).position;
            initialTransformGO.name = "Position 1";
            initialTransformGO.transform.parent = platforms.list[i].targets;
            initialTransformGO.transform.SetSiblingIndex(0);

            platforms.list[i].targetList.Add(initialTransformGO.transform);

            // Put all childs from target group to platform
            Transform targets = platforms.list[i].targets;
            foreach (Transform child in targets)
            {
                platforms.list[i].targetList.Add(child);
            }

            // Also set the initial target to face and time
            platforms.list[i].targetToReach = 1;
            platforms.list[i].timeStarted = Time.time;
        }
    }

    void FixedUpdate()
    {
        // Move each child of Platform manager
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            float speed = platforms.list[i].speed;
            int targetToReach = platforms.list[i].targetToReach;
            Transform targetTransform = platforms.list[i].targetList[targetToReach];

            // If time has not passed and waiting, don't move
            float timeElapsed = Time.time - platforms.list[i].timeStarted;
            if (timeElapsed < platforms.list[i].waitTime)
            {
                continue;
            }

            // If not at the position of the target
            if (child.position != targetTransform.position)
            {
                // Move to target
                child.position = Vector3.MoveTowards(child.position, targetTransform.position, speed * Time.deltaTime);
            }
            else
            {
                #region backwards
                if (platforms.list[i].backwards)
                {
                    // Backward case
                    // If we reached the last target
                    if (targetToReach == 0)
                    {
                        // Go forwards
                        platforms.list[i].backwards = false;
                        platforms.list[i].timeStarted = Time.time;
                    }
                    else
                    {
                        // Go to next target
                        platforms.list[i].targetToReach--;
                    }
                }
                #endregion
                #region Forwards
                else
                {
                    // Forward case
                    // If we reached the last target
                    if (targetToReach == platforms.list[i].targetList.Count-1)
                    {
                        // Go backwards
                        platforms.list[i].backwards = true;
                        platforms.list[i].timeStarted = Time.time;
                    }
                    else
                    {
                        // Go to next target
                        platforms.list[i].targetToReach++;
                    }
                }
                #endregion
            }
        }
    }
}
