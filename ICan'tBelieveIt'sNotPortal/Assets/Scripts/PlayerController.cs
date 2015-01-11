using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed; //The speed at which the player moves.
    public bool isGrounded; //Whether the player is connected to the ground.
    public float jumpForce; //The force applied when the player jumps.
    public PlayerState currentPlayerState; //Create the variable to contain the enum.
    public Sprite idleSprite, jumpSprite, crouchSprite, fallingSprite, walkSprite;
    public Sprite[] runningSprites = new Sprite[6];
    bool facingLeft = false; //For switching direction.
    public GameObject PortalShoot; //Prefab PortalShoot.
    public bool bluePortalActive = false, orangePortalActive = false;
    bool portalCooldown = false; //Whether the player is in cooldown before using another portal.
    bool orangeGel = false;

    //Create an enum for the player's state.
    public enum PlayerState
    {
        IDLE,
        WALK,
        JUMP,
        FALL,
        CROUCH
    };

    void Start()
    {
        currentPlayerState = PlayerState.IDLE; //Start the player as IDLE.
    }

    //Update function - gets called every frame.
    void Update()
    {
        //!!! INPUT MANAGER !!!
        //Check if the player wants to move left or right.
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (isGrounded)
            {
                currentPlayerState = PlayerState.WALK;
            }
            float hor = Input.GetAxis("Horizontal");
            Vector3 movePos = new Vector3(moveSpeed, 0, 0);
            if (hor < 0)
            {
                //transform.position -= movePos;
                if (isGrounded && !orangeGel)
                {
                    transform.rigidbody2D.velocity = new Vector2(-moveSpeed * 30, transform.rigidbody2D.velocity.y);
                }
                else if (isGrounded && orangeGel)
                {
                    transform.rigidbody2D.velocity = new Vector2(-moveSpeed * 90, transform.rigidbody2D.velocity.y);
                }
                else if (!isGrounded && transform.rigidbody2D.velocity.x >= -4)
                {
                    transform.rigidbody2D.velocity = new Vector2(transform.rigidbody2D.velocity.x - moveSpeed, transform.rigidbody2D.velocity.y);
                }
            }
            if (hor > 0)
            {
                //transform.position += movePos;
                if (isGrounded && !orangeGel)
                {
                    transform.rigidbody2D.velocity = new Vector2(moveSpeed * 30, transform.rigidbody2D.velocity.y);
                }
                else if (isGrounded && orangeGel)
                {
                    transform.rigidbody2D.velocity = new Vector2(moveSpeed * 90, transform.rigidbody2D.velocity.y);
                }
                else if (!isGrounded && transform.rigidbody2D.velocity.x <= 4)
                {
                    transform.rigidbody2D.velocity = new Vector2(transform.rigidbody2D.velocity.x + moveSpeed, transform.rigidbody2D.velocity.y);
                }
            }
        }

        if (Input.GetAxis("Horizontal") == 0 && isGrounded && currentPlayerState != PlayerState.JUMP && currentPlayerState != PlayerState.FALL)
        {
            if (transform.rigidbody2D.velocity.x > 0 || transform.rigidbody2D.velocity.x < 0) 
            {
                transform.rigidbody2D.velocity = new Vector2(0, 0);
            }
        }

        //Check if the player wants to jump and if it can.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            currentPlayerState = PlayerState.JUMP;
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }

        //Check if the player wants to shoot a blue portal.
        if (Input.GetButtonDown("LeftMouse"))
        {
            if (bluePortalActive) //Blue is already active.
            {
                Destroy(GameObject.Find("BluePortal(Clone)").gameObject);
                bluePortalActive = false;
            }
            if (!bluePortalActive && !orangePortalActive) //Only blue is active.
            {
                //Attempt to shoot a Portal.
                if (!GameObject.Find("PortalShoot(Clone)"))
                {
                    GameObject Obj = Instantiate(PortalShoot, transform.position, Quaternion.identity) as GameObject; //Create a PortalShoot.
                    Obj.GetComponent<PortalShootController>().portalColour = "blue"; //Set the variables in PortalShoot.
                    Obj.GetComponent<PortalShootController>().firstPortal = true;
                }
            }
            if (!bluePortalActive && orangePortalActive) //Both active.
            {
                //Attempt to shoot a Portal.
                if (!GameObject.Find("PortalShoot(Clone)"))
                {
                    GameObject Obj = Instantiate(PortalShoot, transform.position, Quaternion.identity) as GameObject; //Create a PortalShoot.
                    Obj.GetComponent<PortalShootController>().portalColour = "blue"; //Set the variables in PortalShoot.
                    Obj.GetComponent<PortalShootController>().firstPortal = false;
                }
            }
        }

        //Check if the player wants to shoot an orange portal.
        if (Input.GetButtonDown("RightMouse"))
        {
            if (orangePortalActive) //Orange is already active.
            {
                Destroy(GameObject.Find("OrangePortal(Clone)").gameObject);
                orangePortalActive = false;
            }
            if (!orangePortalActive && !bluePortalActive) //Only orange is active.
            {
                //Attempt to shoot a Portal.
                if (!GameObject.Find("PortalShoot(Clone)"))
                {
                    GameObject Obj = Instantiate(PortalShoot, transform.position, Quaternion.identity) as GameObject; //Create a PortalShoot.
                    Obj.GetComponent<PortalShootController>().portalColour = "orange"; //Set the variables in PortalShoot.
                    Obj.GetComponent<PortalShootController>().firstPortal = true;
                }
            }
            if (!orangePortalActive && bluePortalActive) //Both active.
            {
                //Attempt to shoot a Portal.
                if (!GameObject.Find("PortalShoot(Clone)"))
                {
                    GameObject Obj = Instantiate(PortalShoot, transform.position, Quaternion.identity) as GameObject; //Create a PortalShoot.
                    Obj.GetComponent<PortalShootController>().portalColour = "orange"; //Set the variables in PortalShoot.
                    Obj.GetComponent<PortalShootController>().firstPortal = false;
                }
            }
        }

        //Check if the player wants to crouch.
        if (Input.GetButton("Crouch") && currentPlayerState != PlayerState.WALK) //If the crouch button is pressed and the player is not walking.
        {
            currentPlayerState = PlayerState.CROUCH; //Set the player state to CROUCH.
        }
        if (Input.GetButtonUp("Crouch")) //If the crouch button is released.
        {
            currentPlayerState = PlayerState.IDLE; //Set the player state to IDLE.
        }

        if (Input.GetButtonDown("Clear")) //If the clear button is pressed.
        {
            orangePortalActive = false; //Make all portals inactive.
            bluePortalActive = false;
            if (GameObject.Find("BluePortal(Clone)").gameObject) //Destroy all portals in the scene.
            {
                Destroy(GameObject.Find("BluePortal(Clone)"));
            }
            if (GameObject.Find("OrangePortal(Clone)"))
            {
                Destroy(GameObject.Find("OrangePortal(Clone)").gameObject);
            }
            
            GameObject.Find("Cursor").GetComponent<CursorController>().ChangeSprite("full"); //Reset the cursor.
        }

        //!!! SPRITE MANAGER !!!
        //Check if the player is standing still on the ground.
        if (Input.GetAxis("Horizontal") == 0 && rigidbody2D.velocity.y == 0 && isGrounded && currentPlayerState != PlayerState.CROUCH)
        {
            currentPlayerState = PlayerState.IDLE; //Set the player state to IDLE.
        }
        if (rigidbody2D.velocity.y > 0) //If the player's velocity is over 0.
        {
            currentPlayerState = PlayerState.JUMP; //Set the player state to JUMP.
        }
        if (rigidbody2D.velocity.y < 0 && !isGrounded) //If the player's velocity is under 0 and the player is not grounded.
        {
            currentPlayerState = PlayerState.FALL; //Set the player state to FALL.
        }

        //Change the player's facing direction depending on the mouse position.
        if (Input.mousePosition.x < Screen.width / 2 && !facingLeft || Input.mousePosition.x > Screen.width / 2 && facingLeft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); //Flip the player's sprite.
            facingLeft = !facingLeft;
        }

        //Create a switch statement to test the player's state
        switch (currentPlayerState.ToString())
        {
            case "IDLE":
                GetComponent<SpriteRenderer>().sprite = idleSprite;
                break;
            
            case "WALK":
                GetComponent<SpriteRenderer>().sprite = walkSprite;
                break;

            case "JUMP":
                GetComponent<SpriteRenderer>().sprite = jumpSprite;
                break;

            case "FALL":
                GetComponent<SpriteRenderer>().sprite = fallingSprite;
                break;

            case "CROUCH":
                GetComponent<SpriteRenderer>().sprite = crouchSprite;
                break;
        }
    }

    IEnumerator PortalCooldown()
    {
        portalCooldown = true;
        yield return new WaitForSeconds(0.5f);
        portalCooldown = false;
    }

    //Method for travelling through portals.
    void EnterPortal(GameObject Portal)
    {
        StartCoroutine(PortalCooldown());
        string facing = Portal.GetComponent<PortalController>().facing; //Get the facing direction of the portal.
        GameObject OtherPortal = null;
        string otherPortalColour; //The colour of the other portal.
        Vector2 otherPortalPos; //The position of the other portal.
        float offset = 1f; //The offset added/subtracted from otherPortalPos when changing position.
        Vector2 velocity = transform.rigidbody2D.velocity; //The player's current velocity.
        Vector2 newVelocity = velocity; //The player's new velocity when exiting the other portal.
        
        //Assign portalColour.
        if (Portal.transform.tag == "BluePortal")
        {
            otherPortalColour = "Orange";
        }
        else
        {
            otherPortalColour = "Blue";
        }

        //Check if other portal exists and assign to OtherPortal.
        if (GameObject.Find(otherPortalColour + "Portal(Clone)"))
        {
            OtherPortal = GameObject.Find(otherPortalColour + "Portal(Clone)");
            otherPortalPos = OtherPortal.transform.position;
        }
        else
        {
            return;
        }

        //Setup the new velocity depending on which portal the player is exiting.
        int avg = 1; //Used to keep track of what the newVelocity should be divided by.
        if (facing == "left" && velocity.x > 0)
        {
            newVelocity.x += velocity.x * (-1); //Change velocity from moving right - to moving left.
            ++avg;
        }
        if (facing == "left" && velocity.y < 0)
        {
            newVelocity.x += velocity.y; //Change velocity from falling - to moving left.
            ++avg;
        }
        if (facing == "left" && velocity.y > 0)
        {
            newVelocity.x -= velocity.y; //Change velocity from jumping - to moving left.
            ++avg;
        }
        if (facing == "left")
        {
            //newVelocity.x /= avg; //Set newVelocity.x to the average.
            //newVelocity.y = velocity.y * (-1);
            newVelocity.y = 0;
        }

        avg = 1; //Reset avg to 1.
        if (facing == "right" && velocity.x < 0)
        {
            newVelocity.x += velocity.x * (-1); //Change velocity from moving left - to moving right.
            ++avg;
        }
        if (facing == "right" && velocity.y < 0)
        {
            newVelocity.x += velocity.y * (-1); //Change velocity from falling - to moving right.
            ++avg;
        }
        if (facing == "right" && velocity.y > 0)
        {
            newVelocity.x += velocity.y; //Change velocity from jumping - to moving right.
            ++avg;
        }
        if (facing == "right")
        {
            //newVelocity.x /= avg; //Set newVelocity.x to the average.
            //newVelocity.y = velocity.y * (-1);
            newVelocity.y = 0;
        }

        avg = 1; //Reset avg to 1.
        if (facing == "up" && velocity.y < 0)
        {
            newVelocity.y += velocity.y * (-1); //Change velocity from falling - to jumping.
            ++avg;
        }
        if (facing == "up" && velocity.x < 0)
        {
            newVelocity.y += velocity.x; //Change velocity from moving left - to jumping.
            ++avg;
        }
        if (facing == "up" && velocity.x > 0)
        {
            newVelocity.y -= velocity.x; //Change velocity from moving right - to jumping.
            ++avg;
        }
        if (facing == "up")
        {
            //newVelocity.y /= avg; //Set newVelocity.y to the average.
            //newVelocity.x = velocity.x * (-1);
            newVelocity.x = 0;
        }

        avg = 1; //Reset avg to 1.
        if (facing == "down" && velocity.y > 0)
        {
            newVelocity.y += velocity.y * (-1); //Change velocity from jumping - to falling.
            ++avg;
        }
        if (facing == "down" && velocity.x < 0)
        {
            newVelocity.y += velocity.x; //Change velocity from moving left - to falling.
            ++avg;
        }
        if (facing == "down" && velocity.x > 0)
        {
            newVelocity.y -= velocity.x; //Change velocity from moving right - to falling.
            ++avg;
        }
        if (facing == "down")
        {
            //newVelocity.y /= avg; //Set newVelocity.y to the average.
            //newVelocity.x = velocity.x * (-1);
            newVelocity.x = 0;
        }

        //Setup where the player will be teleported to
        Vector3 pos = otherPortalPos;
        pos.z = transform.position.z;
        switch (facing)
        {
            case "left":
                pos.x += offset;
                break;

            case "right":
                pos.x -= offset;
                break;

            case "up":
                pos.y -= offset;
                break;

            case "down":
                pos.y += offset;
                break;
        }

        //Change the player's position.
        transform.position = pos;

        //Change the player's velocity.
        transform.rigidbody2D.velocity = newVelocity * (-1);

        return;
    }

    //Method for colliding with Blue Gel.
    void BounceBlueGel()
    {
        if (rigidbody2D.velocity.y < 0) //If the player is falling.
        {
            float bounceForce = rigidbody2D.velocity.y * 2; //Set bounceForce to the player's velocity * 2.
            rigidbody2D.velocity += new Vector2(0, -bounceForce); //Add the new velocity to the old velocity.
        }
    }

    //When the player enters a collision.
    void OnCollisionEnter2D(Collision2D Coll)
    {
        //Check if player collided with a 'block' and is not falling.
        if (Coll.transform.tag.EndsWith("Block") && rigidbody2D.velocity.y < 0.1f)
        {
            isGrounded = true;
        }
    }

    //When the player stays in a collision.
    void OnCollisionStay2D(Collision2D Coll)
    {
        //Check if player collided with a 'block' and is not falling.
        if (Coll.transform.tag.EndsWith("Block") && rigidbody2D.velocity.y < 0.1f)
        {
            isGrounded = true;
        }
    }

    //When the player exits a collision.
    void OnCollisionExit2D(Collision2D Coll)
    {
        //Check if the player exited the collision with the 'block'
        if (Coll.transform.tag.EndsWith("Block") && isGrounded)
        {
            isGrounded = false;
        }
    }

    //When the player enters a trigger.
    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.transform.tag == "BluePortal" || Coll.transform.tag == "OrangePortal")
        {
            if (!portalCooldown)
            {
                EnterPortal(Coll.gameObject);
            }
        }
        if (Coll.transform.tag == "BlueGel") //If collided with blue gel.
        {
            BounceBlueGel();
        }
        if (Coll.transform.tag == "OrangeGel") //If collided with orange gel.
        {
            orangeGel = true; //Set orangeGel to true.
        }
        if (Coll.transform.tag == "Finish") //If collided with the finish.
        {
            //FINISH
            Destroy(Coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D Coll)
    {
        if (Coll.transform.tag == "OrangeGel") //If exited the collision with Orange Gel.
        {
            orangeGel = false; //Set orangeGel to false.
        }
    }
}
