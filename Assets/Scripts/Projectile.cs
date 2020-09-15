using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float fireballSpeed = 20f;
    public ProjectileType projectileType;
    public  enum ProjectileType
    {
        fireball
        //etc
    }
    private void Awake()
    {
        //idk, get player direction?
    }
    void Update()
    {
        switch (projectileType)
        {
            case ProjectileType.fireball:
                {
                    projectileSpeed = fireballSpeed;
                }
                //etc
                break;
        }
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        //trying to destroy on collision with non-player
        Debug.Log("collided");
        if(collision.collider.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }

}
