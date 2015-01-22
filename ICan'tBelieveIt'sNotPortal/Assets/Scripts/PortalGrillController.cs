using UnityEngine;
using System.Collections;

public class PortalGrillController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.gameObject == GameObject.Find("Player"))
        {
            GameObject[] AllPortals = GameObject.FindGameObjectsWithTag("Portal"); //Get all portals in the scene.

            foreach (GameObject PortalObj in AllPortals) //Go through each portal.
            {
                Destroy(PortalObj); //Destroy each portal.
            }
            GameObject.Find("Cursor").GetComponent<CursorController>().ChangeSprite("BothUnused");
        }
    }
}
