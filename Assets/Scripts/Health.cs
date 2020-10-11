using UnityEngine;

public class Health : MonoBehaviour
{
    public int _currentHealth = 50;
    public enum HealthType
    {
        player,
        enemy01,
        enemy02
    }
    public HealthType healthType;
    public int player = 100;
    public int enemy01 = 10;
    public int enemy02 = 20;
    private int maxHealth;

    private void Start()
    {
        switch (healthType)
        {
            case HealthType.player:
                maxHealth = player;
                break;
            case HealthType.enemy01:
                maxHealth = enemy01;
                break;
            case HealthType.enemy02:
                maxHealth = enemy02;
                break;
        }
        Debug.Log("I am health type " + healthType + " with a max health of " + maxHealth);
    }

    public void Heal(int amount)
    {

        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        Debug.Log(gameObject.name + " has healed " + amount);
    }
    public void Kill()
    {
        if(_currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
