﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed; //The speed at which the player moves.
    public bool isGrounded; //Whether the player is connected to the ground.
    public float jumpForce; //The force applied when the player jumps.
    public PlayerState currentPlayerState; //Create the variable to contain the enum.
    public Sprite idleSprite, jumpSprite, crouchSprite, fallingSprite, walkSprite;
    public Texture2D cursorEmpty, cursorFill, cursorBlue, cursorOrange;
    public Sprite[] runningSprites = new Sprite[6];
    bool facingLeft = false; //For switching direction.
    public GameObject Portal; //Prefab Portal.
    bool bluePortalActive = false, orangePortalActive = false;

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
        Cursor.SetCursor(cursorFill, new Vector2(cursorOrange.width / 2, cursorBlue.height / 2), CursorMode.Auto);
        currentPlayerState = PlayerState.IDLE;
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
            currentPlayerState = PlayerState.JUMP;
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }

        //Check if the player wants to shoot a blue portal.
        if (Input.GetButtonDown("LeftMouse"))
        {
            if (bluePortalActive) //Blue is already active.
            {
                //NONONO!
            }
            if (!bluePortalActive && !orangePortalActive) //Only blue is active.
            {
                Cursor.SetCursor(cursorOrange, new Vector2(cursorOrange.width / 2, cursorBlue.height / 2), CursorMode.Auto);
                bluePortalActive = true;
            }
            if (!bluePortalActive && orangePortalActive) //Both active.
            {
                Cursor.SetCursor(cursorEmpty, new Vector2(cursorEmpty.width / 2, cursorBlue.height / 2), CursorMode.Auto);
                bluePortalActive = true;
            }
        }

        //Check if the player wants to shoot an orange portal.
        if (Input.GetButtonDown("RightMouse"))
        {
            if (orangePortalActive) //Orange is already active.
            {
                //NONONO!
            }
            if (!orangePortalActive && !bluePortalActive) //Only orange is active.
            {
                Cursor.SetCursor(cursorBlue, new Vector2(cursorOrange.width / 2, cursorBlue.height / 2), CursorMode.Auto);
                orangePortalActive = true;
            }
            if (!orangePortalActive && bluePortalActive) //Both active.
            {
                Cursor.SetCursor(cursorEmpty, new Vector2(cursorEmpty.width / 2, cursorBlue.height / 2), CursorMode.Auto);
                orangePortalActive = true;
            }
        }

        //Check if the player wants to crouch.
        if (Input.GetButton("Crouch"))
        {
            currentPlayerState = PlayerState.CROUCH;
        }

        if (Input.GetButtonDown("Clear"))
        {
            Debug.Log("Clear");
            orangePortalActive = false;
            bluePortalActive = false;
            Cursor.SetCursor(cursorFill, new Vector2(cursorOrange.width / 2, cursorBlue.height / 2), CursorMode.Auto);
        }

        //!!! SPRITE MANAGER !!!
        Debug.Log(currentPlayerState.ToString());

        //Check if the player is standing still on the ground.
        if (Input.GetAxis("Horizontal") == 0 && rigidbody2D.velocity.y == 0 && isGrounded)
        {
            currentPlayerState = PlayerState.IDLE;
        }
        if (rigidbody2D.velocity.y > 0)
        {
            currentPlayerState = PlayerState.JUMP;
        }
        if (rigidbody2D.velocity.y < 0 && !isGrounded)
        {
            currentPlayerState = PlayerState.FALL;
        }

        //Change the player's facing direction depending on the mouse position.
        if (Input.mousePosition.x < Screen.width / 2 && !facingLeft || Input.mousePosition.x > Screen.width / 2 && facingLeft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
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

    //When the player enters a collision.
    void OnCollisionEnter2D(Collision2D Coll)
    {
        //Check if player collided with a 'block' and is not falling.
        if (Coll.transform.tag == "Block" && rigidbody2D.velocity.y < 0.1f)
        {
            isGrounded = true;
        }
    }

    //When the player stays in a collision.
    void OnCollisionStay2D(Collision2D Coll)
    {
        //Check if player collided with a 'block' and is not falling.
        if (Coll.transform.tag == "Block" && rigidbody2D.velocity.y < 0.1f)
        {
            isGrounded = true;
        }
    }

    //When the player exits a collision.
    void OnCollisionExit2D(Collision2D Coll)
    {
        //Check if the player exited the collision with the 'block'
        if (Coll.transform.tag == "Block" && isGrounded)
        {
            isGrounded = false;
        }
    }
}
