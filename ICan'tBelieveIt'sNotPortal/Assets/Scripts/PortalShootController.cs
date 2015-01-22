using UnityEngine;
using System.Collections;

public class PortalShootController : MonoBehaviour 
{
    public GameObject Portal; //Prefab of Portal.
    public float speed; //The speed of the portalShoot.
    private Vector3 dir; //Direction towards the mouse.
    private string colour; //The colour of the portal to be passed on.
    private string portalDirection; //The position of the platform the portal will be spawned on.

    void Start()
    {
        dir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get the direction towards the mouse.
    }

    void Update()
    {
        transform.position += new Vector3(-dir.x, -dir.y, transform.position.z) / speed; //Move towards dir.
    }

    //Set the colour of the portal.
    public void SetColour(string c)
    {
        colour = c;
    }

    private void CreatePortal() 
    {
        bool otherSame = false, otherOther = false;
        GameObject[] AllPortals = GameObject.FindGameObjectsWithTag("Portal"); //Get all portals in the scene - if any.

        foreach (GameObject Obj in AllPortals) //Go through all portals in the scene - if any.
        {
            if (Obj.GetComponent<PortalController>().colour == colour) //If the portal is this colour.
            {
                otherSame = true;
                Destroy(Obj); //Destroy the same colour portal that is already present in the scene.
            }
            else //If the portal is a different colour.
            {
                otherOther = true;
            }
        }

        GameObject Cursor = GameObject.Find("Cursor");
        if (otherSame && otherOther) //2 Portals
        {
            Cursor.GetComponent<CursorController>().ChangeSprite("BothUsed");
        }
        if (!otherSame && !otherOther) //No Portals
        {
            Cursor.GetComponent<CursorController>().ChangeSprite("OnlyBlue");
        }
        if (!otherSame && otherOther) //1 Other
        {
            Cursor.GetComponent<CursorController>().ChangeSprite("BothUsed");
        }
        if (otherSame && !otherOther && colour == "blue") //1 Same
        {
            Cursor.GetComponent<CursorController>().ChangeSprite("OnlyBlue");
        }
        if (otherSame && !otherOther && colour == "orange") //1 Same
        {
            Cursor.GetComponent<CursorController>().ChangeSprite("OnlyOrange");
        }

        GameObject PortalObj = Instantiate(Portal, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject; //Create the portal.
        PortalObj.GetComponent<PortalController>().SetColour(colour); //Pass the colour through.
        PortalObj.GetComponent<PortalController>().SetFacingDirection(portalDirection); //Pass through the direction of the portal.

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.transform.tag == "PortableSurface")
        {
            if (Coll.transform.name.EndsWith("_Up"))
            {
                portalDirection = "down";
            }
            if (Coll.transform.name.EndsWith("_Down"))
            {
                portalDirection = "up";
            }
            if (Coll.transform.name.EndsWith("_Left"))
            {
                portalDirection = "right";
            }
            if (Coll.transform.name.EndsWith("_Right"))
            {
                portalDirection = "left";
            }
            CreatePortal();
        }
        if (Coll.transform.tag == "UnPortableSurface" || Coll.transform.name == "PortalGrill")
        {
            Destroy(gameObject);
        }
    }
}
