using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
    Vector2 playerPos; //The player's position to be updated every frame.

    void Start()
    {
        playerPos = GameObject.Find("Player").transform.position; //Update playerPos.
    }

    void Update()
    {
        if (GameObject.Find("Player"))
        {
            playerPos = GameObject.Find("Player").transform.position; //Update playerPos.
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z); //Move the camera to follow the player.
        }
    }
}
