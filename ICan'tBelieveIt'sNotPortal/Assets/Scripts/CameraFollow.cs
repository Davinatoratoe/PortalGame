using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    Vector2 playerPos;

    void Start()
    {
        playerPos = GameObject.Find("Player").transform.position;
    }

    void Update()
    {
        if (GameObject.Find("Player"))
        {
            playerPos = GameObject.Find("Player").transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        }
    }
}
