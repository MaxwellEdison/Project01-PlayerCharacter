using UnityEngine;

public class AbilityUI : MonoBehaviour
{

    public GameObject fireSkillUI;
    public GameObject healSkillUI;
    public GameObject blinkSkillUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public void SkillUIChange(string skill)
    {
        if(skill == "fireball")
        {
            fireSkillUI.SetActive(true);
            healSkillUI.SetActive(false);
            blinkSkillUI.SetActive(false);
        } else if (skill == "cure")
        {
            fireSkillUI.SetActive(false);
            healSkillUI.SetActive(true);
            blinkSkillUI.SetActive(false);
        } else if (skill == "blink")
        {
            fireSkillUI.SetActive(false);
            healSkillUI.SetActive(false);
            blinkSkillUI.SetActive(true);
        }
    }
}
