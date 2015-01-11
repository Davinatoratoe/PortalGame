using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour 
{
    [HideInInspector]
    public string facing; //The direction the portal is facing.

    void Start()
    {
        Quaternion portalRotation = Quaternion.identity; //Local variable for the rotation of the portal.

        //Change portalRotation based on the facing variable.
        if (facing == "up" || facing == "down") //If portal is on the roof or floor.
        {
            transform.Rotate(new Vector3(0, 0, 90)); //Rotate the portal 90 degrees.
        }
    }
}
