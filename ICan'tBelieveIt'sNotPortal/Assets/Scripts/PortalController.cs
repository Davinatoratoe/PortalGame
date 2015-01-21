using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour 
{
    public Sprite BluePortal, OrangePortal; //Sprites for the portal.
    public string colour; //Colour of the portal.
    private GameObject otherPortal; //The gameobject for the other portal.
    private string facingDir; //The direction the portal is facing.
    public float adjust;
    private float adjustX = 0, adjustY = 0; //The amount the player is adjusted when teleporting to the other portal.
    private bool jump; //Whether the player can enter the portal.

    void Start()
    {
        CheckForTarget(); //Find the other portal and link it to this portal.
    }

    void Update()
    {
        if (otherPortal == null) //If the other portal doesn't exist.
        {
            CheckForTarget(); //Begin checking for it.
        }
    }

    //Check for the other portal.
    private void CheckForTarget()
    {
        GameObject[] AllPortals = GameObject.FindGameObjectsWithTag("Portal"); //Get all portals.
        foreach (GameObject PortalObj in AllPortals) //Check all portals.
        {
            if (PortalObj != gameObject) //If the portal is not this portal.
            {
                otherPortal = PortalObj; //Set otherPortal to PortalObj.
            }
        }
    }

    //Set the direction of the portal and rotate it.
    public void SetFacingDirection(string d)
    {
        facingDir = d;

        if (facingDir == "left")
        {
            transform.Rotate(new Vector3(0, 0, 180));
        }
        if (facingDir == "up")
        {
            transform.Rotate(new Vector3(0, 0, -90));
        }
        if (facingDir == "down")
        {
            transform.Rotate(new Vector3(0, 0, 90));
        }
    }

    //Set the colour of this portal.
    public void SetColour(string c)
    {
        colour = c;
        if (colour == "orange") //If the portal is orange.
        {
            GetComponent<SpriteRenderer>().sprite = OrangePortal; //Change the sprite to orange.
        }
        if (colour == "blue" && GetComponent<SpriteRenderer>().sprite != BluePortal) //If the portal is blue.
        {
            GetComponent<SpriteRenderer>().sprite = BluePortal; //Change the sprite to blue.
        }
    }

    //Teleport the player through the other portal.
    private void Teleport(GameObject Player)
    {
        //Set the adjust for teleporting to the other portal.
        switch (otherPortal.GetComponent<PortalController>().facingDir) //Get the the other portal's facing direction.
        {
            case "up":
                adjustY = adjust;
                break;
            case "down":
                adjustY = -adjust;
                break;
            case "left":
                adjustX = -adjust;
                break;
            case "right":
                adjustX = adjust;
                break;
        }

        otherPortal.GetComponent<PortalController>().jump = true; //Set the other portal's jump to true. Let it know we're coming.
        Player.transform.position = new Vector3(otherPortal.transform.position.x + adjustX, otherPortal.transform.position.y + adjustY, Player.transform.position.z); //Teleport the player.

        string otherPortalDirection = otherPortal.GetComponent<PortalController>().facingDir; //Get the other portal's facing direction.

        //Set the velocity for coming out of the otherportal.
        switch (otherPortalDirection)
        {
            case "right":
                Player.rigidbody2D.velocity = transform.up * Player.rigidbody2D.velocity.magnitude;
                break;
            case "left":
                Player.rigidbody2D.velocity = transform.up * -Player.rigidbody2D.velocity.magnitude;
                break;
            case "up":
                Player.rigidbody2D.velocity = new Vector2(0, 1) * Player.rigidbody2D.velocity.magnitude;
                break;
            case "down":
                Player.rigidbody2D.velocity = new Vector2(0, 1) * -Player.rigidbody2D.velocity.magnitude;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.transform.tag == "Player" && otherPortal != null && !jump) //If the player enters the portal.
        {
            Teleport(Coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D Coll)
    {
        if (Coll.transform.tag == "Player") //If the player exits this portal.
        {
            jump = false; //Set jump to false - enabling the player to use the portal again.
        }
    }
}
