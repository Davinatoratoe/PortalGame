using UnityEngine;
using System.Collections;

public class EndController : MonoBehaviour 
{
    public Sprite closed;

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.gameObject == GameObject.Find("Player"))
        {
            StartCoroutine("Finish");
        }
    }

    private IEnumerator Finish()
    {
        GameObject.Find("Player").GetComponent<SpriteRenderer>().enabled = false;
        Destroy(GameObject.Find("Player").GetComponent<PlayerController>());
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().sprite = closed;
        yield return new WaitForSeconds(1f);
        //if (Application.levelCount != Application.loadedLevel + 1)
        //{
        //    Application.LoadLevel(Application.loadedLevel + 1);
        //}
        GameObject.Find("EndText").GetComponent<MeshRenderer>().enabled = true;
    }
}
