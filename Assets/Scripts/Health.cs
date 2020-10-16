using UnityEngine;

public class Health : MonoBehaviour
{
    public int _currentHealth;
    private HealthUI healthUI;
    private GameObject healthBar;
    private GameObject abilityUI;
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
        healthUI = GameObject.FindGameObjectWithTag("MainUI").GetComponent<HealthUI>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        abilityUI = GameObject.FindGameObjectWithTag("SkillSelect");
        switch (healthType)
        {
            case HealthType.player:
                maxHealth = player;
                _currentHealth = player;
                break;
            case HealthType.enemy01:
                maxHealth = enemy01;
                _currentHealth = enemy01;
                break;
            case HealthType.enemy02:
                maxHealth = enemy02;
                _currentHealth = enemy02;
                break;
        }
        healthUI.UpdateHP();
        Debug.Log("I am health type " + healthType + " with a max health of " + maxHealth);

    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        Debug.Log(gameObject.name + " has healed " + amount);
        if(healthType == HealthType.player)
        {

            healthUI.UpdateHP();
        }
    }

    public void Damage(int amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        Debug.Log(gameObject.name + " has been hurt " + amount);
        if (healthType == HealthType.player)
        {
            healthUI.UpdateHP();
            healthBar.AddComponent<FeedbackUI>();
            abilityUI.AddComponent<FeedbackUI>();
        }
        if (_currentHealth <= 0)
        {
            Kill();
        }
    }
    public void Kill()
    {
        Destroy(this.gameObject);
    }
}
