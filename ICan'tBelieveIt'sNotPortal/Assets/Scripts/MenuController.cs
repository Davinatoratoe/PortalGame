using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
    void OnMouseDown()
    {
        if (transform.name == "Play")
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
    }
}
