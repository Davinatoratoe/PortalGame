using UnityEngine;
using System.Collections;

public class OrangeGelController : MonoBehaviour 
{
    public float speedLimit; //The speed limit.
    private float oldSpeed; //For keeping a copy of the player's original speed.

    private void SpeedUp(GameObject Player)
    {
        if (Player.GetComponent<PlayerController>().speed < speedLimit)
        {
            Player.GetComponent<PlayerController>().speed *= 1.1f; //Amplify the player's speed.
        }
    }

    void OnCollisionEnter2D(Collision2D Coll)
    {
        if (Coll.transform.tag == "Player") //If the player enters the orange gel collision.
        {
            oldSpeed = Coll.gameObject.GetComponent<PlayerController>().speed; //Save the player's original speed.
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
            Coll.gameObject.GetComponent<PlayerController>().speed = oldSpeed; //Reset the player's speed.
        }
    }
}
