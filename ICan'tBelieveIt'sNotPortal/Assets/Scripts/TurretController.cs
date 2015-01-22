using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour
{
    public GameObject sight; //The turret sight's transform.
    public GameObject target; //The player's transform.
    private bool canShoot = true; //Whether the turret can shoot.
    public float range; //The range of the turret's view distance.
    public GameObject Bullet; //Prefab for the bullet.
    public bool facingLeft = true; //The direction the turret is facing.

    void Start()
    {
        target = GameObject.Find("Player");
    }

    void Update()
    {
        if (target != null)
        {
            if ((facingLeft && target.transform.position.x < transform.position.x) || (!facingLeft && target.transform.position.x > transform.position.x)) //Check if the player is to the left of the turret.
            {
                RaycastHit2D hit = Physics2D.Raycast(sight.transform.position, (target.transform.position - sight.transform.position).normalized, range); //Create a ray towards the player.
                if (hit.collider != null) //If the ray hit something.
                {
                    if (hit.collider.gameObject == GameObject.Find("Player")) //If the player is in range.
                    {
                        if (canShoot) //If the turret can shoot.
                        {
                            Shoot((transform.position - hit.transform.position).normalized);
                        }
                    }
                }
            }
        }
    }

    private void Shoot(Vector3 direction)
    {
        canShoot = false;
        GameObject BulletObj = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject; //Create the bullet.
        BulletObj.GetComponent<BulletController>().dir = direction * 10; //Pass through the direction.
        StartCoroutine("ResetCanShoot");
    }

    private IEnumerator ResetCanShoot()
    {
        yield return new WaitForSeconds(0.05f); //Wait 0.2 seconds before continuing.
        canShoot = true;
    }
}
