using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour 
{
    public Sprite Full, Empty, Blue, Orange; //All the cursor sprites.
    Vector3 oldPos;
    bool useMouse = true;

    void Update()
    {
        if (Input.GetButtonDown("CursorMovement") && useMouse) //If the joystick is being used.
        {
            useMouse = false; //Set useMouse to false.
            oldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Set oldPos to the current mouse position.
        }
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition) != oldPos && !useMouse) //If the mouse cursor was moved.
        {
            useMouse = true; //Set useMouse to true.
        }

        if (useMouse)
        {
            Vector3 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get the mouse's position and convert it to world point.
            mousePos.z = transform.position.z; //Keep the z-axis to the current z-axis.
            transform.position = mousePos; //Change the position to mousePos.
        }
        else if (!useMouse)
        {
            
        }
    }

    public void ChangeSprite(string changeTo) //Public method for changing the cursor's sprite.
    {
        switch (changeTo)
        {
            case "full":
                GetComponent<SpriteRenderer>().sprite = Full;
                break;
            case "empty":
                GetComponent<SpriteRenderer>().sprite = Empty;
                break;
            case "orange":
                GetComponent<SpriteRenderer>().sprite = Orange;
                break;
            case "blue":
                GetComponent<SpriteRenderer>().sprite = Blue;
                break;
        }
    }
}
