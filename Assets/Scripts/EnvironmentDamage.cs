using UnityEngine;

public class EnvironmentDamage : MonoBehaviour
{
    public enum EnvironmentDamageType
    {
        spikes,
        lava,
        pit
    }
    public EnvironmentDamageType dmgObject;
    public int spikeDmg = 10;
    public int lavaDmg = 20;
    public int pitDmg = 100;
    private int dmgValue;
    
    private void SetDmg()
    {
        switch (dmgObject)
        {
            case EnvironmentDamageType.spikes:
                dmgValue = spikeDmg;
                break;
            case EnvironmentDamageType.lava:
                dmgValue = lavaDmg;
                break;
            case EnvironmentDamageType.pit:
                dmgValue = pitDmg;
                break;
        }
        Debug.Log("I am hazard type " + dmgObject + " with a damage value of " + dmgValue);
    }
    private void OnTriggerEnter(Collider other)
    {
        SetDmg();
        if(other.gameObject.tag == "Player")
        {

            GameObject player = other.gameObject;
            Debug.Log("I damaged the player for " + dmgValue + " and they currently have " + player.GetComponent<Health>()._currentHealth + " hp");
            player.GetComponent<Health>()._currentHealth -= dmgValue;
            if(player.GetComponent<Health>()._currentHealth <= 0)
            {
                player.GetComponent<Health>().Kill();
            }
        }
    }
}
