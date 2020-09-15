using UnityEngine;

public class Fireball : Ability
    //tutorial ability, see "blink" for new ability

{
    [SerializeField] GameObject _projectileSpawned = null;
    int _rank = 1;
    float duration = 3.5f;
    public override void Use(Transform origin, Transform target)
    {
        Vector3 playeroffset = new Vector3(0, 1.8f, 0);
        GameObject projectile = Instantiate(_projectileSpawned, origin.position+playeroffset, origin.rotation);
        Debug.Log("Cast a rank " + _rank + " fireball on " + target);
        if(target != null)
        {
            projectile.transform.LookAt(target);
        }
        Destroy(projectile, duration);
    }
}
