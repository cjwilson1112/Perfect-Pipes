using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    void Update()
    {
        if(LevelSelect.instance.currentLevel != null)
            transform.position = Input.mousePosition;
    }
}
