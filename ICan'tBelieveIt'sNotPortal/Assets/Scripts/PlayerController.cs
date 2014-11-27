using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed; //The speed at which the player moves.
    public bool isGrounded; //Whether the player is connected to the ground.
    public float jumpForce; //The force applied when the player jumps.

    //Update function - gets called every frame.
    void Update()
    {
        //Check if the player wants to move left or right.
        if (Input.GetAxis("Horizontal") != 0)
        {
            float hor = Input.GetAxis("Horizontal");
            Vector3 movePos = new Vector3(moveSpeed, 0, 0);
            if (hor < 0)
            {
                transform.position -= movePos;
            }
            if (hor > 0)
            {
                transform.position += movePos;
            }
        }

        //Check if the player wants to jump and if it can.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    void OnCollisionEnter2D(Collision2D Coll)
    {
        //Check if player collided with a 'block' and is not falling.
        if (Coll.transform.tag == "Block" && rigidbody2D.velocity.y < 0.1f)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D Coll)
    {
        if (Coll.transform.tag == "Block" && isGrounded)
        {
            isGrounded = false;
        }
    }
}
