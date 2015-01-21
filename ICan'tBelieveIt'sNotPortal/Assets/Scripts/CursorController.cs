using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour 
{
    public Sprite OnlyBlue, OnlyOrange, BothUsed, BothUnused;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        transform.position = mousePos;
    }

    public void ChangeSprite(string changeTo)
    {
        switch (changeTo)
        {
            case "OnlyBlue":
                GetComponent<SpriteRenderer>().sprite = OnlyBlue;
                break;
            case "OnlyOrange":
                GetComponent<SpriteRenderer>().sprite = OnlyOrange;
                break;
            case "BothUsed":
                GetComponent<SpriteRenderer>().sprite = BothUsed;
                break;
            case "BothUnused":
                GetComponent<SpriteRenderer>().sprite = BothUnused;
                break;
        }
    }
}
