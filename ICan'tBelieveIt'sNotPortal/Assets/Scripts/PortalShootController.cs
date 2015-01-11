using UnityEngine;
using System.Collections;

public class PortalShootController : MonoBehaviour {

    Vector3 mousePos; //The mouse's position.
    Vector3 direction; //The direction towards the mouse.
    float speed = 0.2f; //The speed the PortalShoot will travel.
    public GameObject BluePortal, OrangePortal; //Declare portal prefabs.
    public string portalColour; //The colour of the portal.
    public bool firstPortal; //Whether it is the first portal in the scene.
    GameObject Player; //The player as a gameobject.
    string facing; //The direction the portal will be facing.

    void Start()
    {
        Player = GameObject.Find("Player"); //Assign Player.
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Assign mousePos to the mouse's position in world-point.
        mousePos.z = transform.position.z; //Keep the z equal to the PortalShoot's z.
        direction = mousePos - transform.position; //Assign direction to the direction from PortalShoot to the mouse.
    }

    //Called every frame.
    void Update()
    {
        transform.position += direction.normalized * speed; //Move the gameobject in the direction of the mouse.
    }

    //When the collider hits another collider with a trigger.
    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.transform.tag == "PortableBlock") //If collided with a portable surface.
        {
            //Clear portals in scene
            if (portalColour == "blue" && GameObject.Find("BluePortal(Clone"))
            {
                Destroy(GameObject.Find("BluePortal(Clone").gameObject);
            }
            if (portalColour == "orange" && GameObject.Find("OrangePortal(Clone"))
            {
                Destroy(GameObject.Find("OrangePortal(Clone").gameObject);
            }

            if (portalColour == "blue" && firstPortal)
            {
                GameObject.Find("Cursor").GetComponent<CursorController>().ChangeSprite("orange"); //Set the cursor sprite to orange.
                Player.GetComponent<PlayerController>().bluePortalActive = true;
            }
            if (portalColour == "blue" && !firstPortal)
            {
                GameObject.Find("Cursor").GetComponent<CursorController>().ChangeSprite("empty");
                Player.GetComponent<PlayerController>().bluePortalActive = true;
            }
            if (portalColour == "orange" && firstPortal)
            {
                GameObject.Find("Cursor").GetComponent<CursorController>().ChangeSprite("blue");
                Player.GetComponent<PlayerController>().orangePortalActive = true;
            }
            if (portalColour == "orange" && !firstPortal)
            {
                GameObject.Find("Cursor").GetComponent<CursorController>().ChangeSprite("empty");
                Player.GetComponent<PlayerController>().orangePortalActive = true;
            }

            //Check the direction of the portal.
            if (Coll.transform.name.EndsWith("_Left")) //Surface is to the left
            {
                facing = "right"; //The portal will be facing right.
            }
            else if (Coll.transform.name.EndsWith("_Right"))
            {
                facing = "left";
            }
            else if (Coll.transform.name.EndsWith("_Up"))
            {
                facing = "down";
            }
            else if (Coll.transform.name.EndsWith("_Down"))
            {
                facing = "up";
            }

            SpawnPortal(); //Call the SpawnPortal() method to create the portal.
            Destroy(gameObject); //Destroy this gameobject.
        }
        
        if (Coll.transform.tag == "NonPortableBlock") //If collided with a non-portable surface.
        {
            Destroy(gameObject); //Destroy this gameobject.
        }
    }

    void SpawnPortal()
    {
        GameObject Obj = null; //The new portal gameobject.
        if (portalColour == "blue") //If the portal is blue.
        {
            Obj = Instantiate(BluePortal, transform.position, Quaternion.identity) as GameObject; //Instantiate a blue portal.
        }
        if (portalColour == "orange") //If the portal is orange.
        {
            Obj = Instantiate(OrangePortal, transform.position, Quaternion.identity) as GameObject; //Instantiate an orange portal.
        }
        Obj.GetComponent<PortalController>().facing = facing;
    }
}
