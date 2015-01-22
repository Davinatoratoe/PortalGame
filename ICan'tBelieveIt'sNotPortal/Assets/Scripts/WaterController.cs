using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.gameObject == GameObject.Find("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().playerHealth = 0;
        }
    }
}
