using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AbilityLoadout _abilityLoadout;
    [SerializeField] Ability[] _abilities;
    [SerializeField] Ability _startingAbility;

    [SerializeField] Transform _testTarget = null;


    public Transform CurrentTarget { get; private set; }
    public enum EquippedAbility
    {
        Cure,
        Fireball,

        Blink
    }
    public EquippedAbility equippedAbility;

    private void Awake()
    {
        if (_startingAbility != null)
        {
            _abilityLoadout?.EquipAbility(_startingAbility);
        }
    }
    public void SetTarget(Transform newTarget)
    {
        CurrentTarget = newTarget;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _abilityLoadout.UseEquippedAbility(CurrentTarget);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            ChangeAbility();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SetTarget(_testTarget);
        }
    }
    private void ChangeAbility()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            switch (equippedAbility)
            {
                case EquippedAbility.Cure:
                    {
                        equippedAbility = EquippedAbility.Fireball;
                        Debug.Log("Equipped " + _abilities[1].name);
                        _abilityLoadout.EquipAbility(_abilities[1]);
                    }
                    break;
                case EquippedAbility.Fireball:
                    {
                        equippedAbility = EquippedAbility.Blink;
                        Debug.Log("Equipped " + _abilities[2].name);
                        _abilityLoadout.EquipAbility(_abilities[2]);
                    }
                    break;
                case EquippedAbility.Blink:
                    {
                        equippedAbility = EquippedAbility.Cure;
                        Debug.Log("Equipped " + _abilities[0].name);
                        _abilityLoadout.EquipAbility(_abilities[0]);
                    }
                    break;
            }
        }


    }
}
