using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    //Player is dead.
    public IEnumerator PlayerDie()
    {
        GameObject.Find("Health").GetComponent<TextMesh>().text = "GAME OVER!"; //Change health text to say GAME OVER!
        yield return new WaitForSeconds(2f); //Wait 2 seconds.
        Application.LoadLevel(Application.loadedLevel); //Reload the level.
    }
}
