using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Canvas ui;
    public GameObject healthBar;
    public GameObject hpValue;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        UpdateHP();
    }


    public void UpdateHP()
    {
        int newHPValue = playerHealth._currentHealth;
        healthBar.GetComponent<Image>().fillAmount = Mathf.InverseLerp(0,100,newHPValue);
        hpValue.GetComponent<TextMeshProUGUI>().text = "Current Health: " + newHPValue;
    }



}
