using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
    private Transform Target; //Target transform.

    void Start()
    {
        Target = GameObject.Find("Player").transform; //Assign the target to the player's transform.
    }

    void Update()
    {
        if (Target != null)
        {
            if (transform.position != Target.position)
            {
                transform.position = new Vector3(Target.position.x, Target.position.y + 1, transform.position.z); //Follow the player.
            }
        }
    }
}
