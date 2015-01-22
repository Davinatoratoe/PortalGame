using UnityEngine;
using System.Collections;

public class EndController : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.gameObject == GameObject.Find("Player"))
        {
            if (Application.levelCount != Application.loadedLevel + 1)
            {
                Application.LoadLevel(Application.loadedLevel + 1);
            }
        }
    }
}
