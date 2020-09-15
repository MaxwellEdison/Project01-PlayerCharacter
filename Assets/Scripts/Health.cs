using UnityEngine;

public class Health : MonoBehaviour
{
    public int _currentHealth = 50;

    public void Heal(int amount)
    {
        _currentHealth += amount;
        Debug.Log(gameObject.name + " has healed " + amount);
    }
}
