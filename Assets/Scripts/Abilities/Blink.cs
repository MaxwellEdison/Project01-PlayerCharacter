using UnityEngine;

public class Blink : Ability
{
    [SerializeField] GameObject _blinkBoxSpawned = null;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 tempPosition;

    float duration = .3f;

    private void Start()
    {
        tempPosition = player.transform.position;
    }
    public override void Use(Transform origin, Transform target)
    {

        Quaternion wobble = new Quaternion(1+Random.Range(0.0f, 0.1f), 1+Random.Range(0.0f, 0.1f), 1+Random.Range(0.0f, 0.1f), 1+Random.Range(0.0f, 0.1f));
        GameObject o_blinkBox = Instantiate(_blinkBoxSpawned, origin.position, origin.rotation);
        GameObject t_blinkBox = Instantiate(_blinkBoxSpawned, target.position, target.rotation);
        CharacterController plrControl = player.GetComponent<CharacterController>();
        //this is supposed to swap places with the player and target, but it won't work.  Player won't move. 
        //Disabling and Enabling the CharacterController is *supposed to* fix it, but doesn't.  Everything else moves, but not the fucking player.  Fuck you player.
        plrControl.enabled = false;
        player.transform.position = target.position;
        plrControl.enabled = true;
        target.transform.position = tempPosition;
        target.transform.rotation *= wobble;

        
        Debug.Log("Blinked to " + target.name);
        Destroy(o_blinkBox, duration);
        Destroy(t_blinkBox, duration);
    }
}
