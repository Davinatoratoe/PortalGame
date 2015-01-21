using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public GameObject PortalShoot; //The prefab for PortalShoot.
    public Sprite Idle, Walk, Jump, Fall;
    public float speed; //The speed of the player.
    public float jumpForce; //The force applied when the player jumps.
    private bool facingLeft = false; //The direction the player is facing.

    void Update()
    {
        HandleInputs();
        HandlePhysics();
        FaceDirection();
    }

    //Handle all inputs from the player.
    private void HandleInputs()
    {
        if (Input.GetAxis("Horizontal") != 0) //If the arrow keys are being pressed.
        {
            if (GetComponent<SpriteRenderer>().sprite != Walk && rigidbody2D.velocity.y == 0) //If the player is not falling or jumping.
            {
                GetComponent<SpriteRenderer>().sprite = Walk; //Set the sprite to walk.
            }
            float moveAmount = speed * Input.GetAxis("Horizontal");
            transform.position += new Vector3(moveAmount, 0, 0); //Move the player.
        }
        else if (rigidbody2D.velocity.y == 0) //If the player is not pressing any arrow keys and is not moving on the y: change sprite to idle.
        {
            GetComponent<SpriteRenderer>().sprite = Idle;
        }
        if (Input.GetKeyDown(KeyCode.Space) && rigidbody2D.velocity.y == 0) //If player jumps.
        {
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
        if (Input.GetMouseButtonDown(0)) //If player left-clicks.
        {
            ShootPortal("blue");
        }
        if (Input.GetMouseButtonDown(1)) //If player right-clicks.
        {
            ShootPortal("orange");
        }
        if (Input.GetKeyDown(KeyCode.R)) //If the player wants to clear all portals.
        {
            GameObject[] AllPortals = GameObject.FindGameObjectsWithTag("Portal"); //Get all portals in the scene.

            foreach (GameObject PortalObj in AllPortals) //Go through each portal.
            {
                Destroy(PortalObj); //Destroy each portal.
            }
            GameObject.Find("Cursor").GetComponent<CursorController>().ChangeSprite("BothUnused");
        }
    }

    //When the player shoots a portal.
    private void ShootPortal(string colour)
    {
        bool canShoot = true;
        GameObject[] AllShoots = GameObject.FindGameObjectsWithTag("PortalShoot");

        foreach (GameObject Shoot in AllShoots)
        {
            canShoot = false;
        }
        if (canShoot)
        {
            GameObject Obj = Instantiate(PortalShoot, transform.position, Quaternion.identity) as GameObject; //Spawn a PortalShoot.
            Obj.GetComponent<PortalShootController>().SetColour(colour); //Pass through the colour.
        }
    }

    private void HandlePhysics()
    {
        if (rigidbody2D.velocity.y > 0) //If player is jumping.
        {
            GetComponent<SpriteRenderer>().sprite = Jump;
        }
        else if (rigidbody2D.velocity.y < 0) //If player is falling.
        {
            GetComponent<SpriteRenderer>().sprite = Fall;
        }
    }

    //Switch the player's facing direction if the mouse has moved to the other side of the screen.
    private void FaceDirection()
    {
        if (Input.mousePosition.x < Screen.width / 2 && !facingLeft || Input.mousePosition.x > Screen.width / 2 && facingLeft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); //Flip the player's sprite.
            facingLeft = !facingLeft;
        }
    }
}
