using UnityEngine;
using System.Collections;

public class BlueGelController : MonoBehaviour 
{
    private string facingDir; //The direction the gel is facing.

    void Start()
    {
        //Set the direction of the gel.
        if (transform.name.EndsWith("Down"))
        {
            facingDir = "up";
        }
        if (transform.name.EndsWith("Up"))
        {
            facingDir = "down";
        }
        if (transform.name.EndsWith("Left"))
        {
            facingDir = "right";
        }
        if (transform.name.EndsWith("Right"))
        {
            facingDir = "left";
        }
    }

    private void BouncePlayer(GameObject Player)
    {
        float bounceAmountX = 0, bounceAmountY = 0;
        if (facingDir == "up")
        {
            bounceAmountY = -Player.rigidbody2D.velocity.y; //Bounce the player up.
        }
        if (facingDir == "down")
        {
            bounceAmountY = -Player.rigidbody2D.velocity.y; //Bounce the player down.
        }
        if (facingDir == "left")
        {
            bounceAmountX = -Player.rigidbody2D.velocity.x; //Bounce the player left.
        }
        if (facingDir == "right")
        {
            bounceAmountX = Player.rigidbody2D.velocity.x; //Bounce the player right.
        }
        Player.rigidbody2D.AddForce(new Vector2(bounceAmountX, bounceAmountY) * 50);
    }

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.transform.tag == "Player") //If the player hits the BounceGel.
        {
            BouncePlayer(Coll.gameObject);
        }
    }
}
