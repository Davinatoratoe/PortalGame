using UnityEngine;
using System.Collections;

public class OrangeGelController : MonoBehaviour 
{
    public float speedLimit; //The speed limit.
    private float speed = 0; //The speed being added.

    private void SpeedUp(GameObject Player)
    {
        float hor = Input.GetAxis("Horizontal"); //Get the input on the horizontal axis.
        if (hor > 0.1f) //If moving right.
        {
            if (speed < speedLimit) //If speed is below the speed limit.
            {
                speed += 0.2f; //Add to speed.
                Player.rigidbody2D.velocity += new Vector2(speed, 0); //Add to the player's velocity.
            }
        }
        else if (hor < 0.1f) //If moving left.
        {
            if (speed < speedLimit) //If speed is below the speed limit.
            {
                speed += 0.2f; //Add to speed.
                Player.rigidbody2D.velocity += new Vector2(-speed, 0); //Add to the player's velocity.
            }
        }
        else
        {
            speed = 0; //Reset the speed.
        }
    }

    void OnCollisionStay2D(Collision2D Coll)
    {
        if (Coll.transform.tag == "Player") //If the player is walking on the gel.
        {
            SpeedUp(Coll.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D Coll)
    {
        if (Coll.transform.tag == "Player") //If the player exits the collision.
        {
            speed = 0;
        }
    }
}
