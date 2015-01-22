using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    public Vector2 dir;
    public float speed;

    void Update()
    {
        transform.position += new Vector3(-dir.x, -dir.y, transform.position.z) / speed; //Move towards dir.
    }

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.gameObject == GameObject.Find("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().playerHealth -= 10;
            Destroy(gameObject);
        }
        if (Coll.transform.name.StartsWith("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
