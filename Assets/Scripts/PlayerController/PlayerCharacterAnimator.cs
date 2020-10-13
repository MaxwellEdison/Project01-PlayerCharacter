using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerCharacterAnimator : MonoBehaviour
{
    [SerializeField] ThirdPersonMovement _thirdPersonMovement = null;

    const string IdleState = "Idle";
    const string RunState = "Run";
    const string JumpState = "JumpingUp";
    const string Falling = "Falling";
    const string SprintState = "Sprint";
    public float animRunSpd = 1.0f;
    Animator _animator = null;


    private void Update()
    {
        _animator.SetFloat("animRunSpd", animRunSpd);
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void OnIdle()
    {
        _animator.CrossFadeInFixedTime(IdleState, 0.2f);
    }
    private void OnEnable()
    {
        _thirdPersonMovement.Idle += OnIdle;
        _thirdPersonMovement.StartRunning += OnStartRunning;
        _thirdPersonMovement.StartJumping += OnStartJumping;
        _thirdPersonMovement.StartFalling += OnStartFalling;
        _thirdPersonMovement.StopFalling += OnStopFalling;
        _thirdPersonMovement.StartSprinting += OnStartSprinting;
    }
    private void OnDisable()
    {
        _thirdPersonMovement.Idle -= OnIdle;
        _thirdPersonMovement.StartRunning -= OnStartRunning;
        _thirdPersonMovement.StartJumping -= OnStartJumping;
        _thirdPersonMovement.StartFalling -= OnStartFalling;
        _thirdPersonMovement.StopFalling -= OnStopFalling;
        _thirdPersonMovement.StartSprinting -= OnStartSprinting;
    }

    private void OnStartJumping()
    {
        _animator.SetFloat("Direction", 1);
        _animator.CrossFadeInFixedTime(JumpState, 0.2f);
    }
    private void OnStartFalling()
    {
        _animator.CrossFadeInFixedTime(Falling, 0.2f);
    }
    private void OnStartSprinting()
     {
         //_animator.CrossFadeInFixedTime(RunState, 0.2f);
         _animator.CrossFadeInFixedTime(SprintState, 0.2f);
     }
    private void OnStopFalling()
    {
        _animator.SetFloat("Direction", -1);
        _animator.CrossFadeInFixedTime(JumpState, 0.2f);
    }
    private void OnStartRunning()
    {

        _animator.CrossFadeInFixedTime(RunState, 0.2f);
    }
    // Update is called once per frame

}