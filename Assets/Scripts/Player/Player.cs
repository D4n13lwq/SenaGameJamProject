using UnityEngine;
using UnityEngine.InputSystem;

#region crimen de guerra 2
[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(Detector))]
[RequireComponent(typeof(PlayerInput))]
#endregion

public class Player : MonoBehaviour
{
    PlayerInput _playerInput;

    Animator _anim;
    Rigidbody _rb;
    Detector _groundDetector;

    bool _ohMyGodTheyKilledKenny = false;

    public PlayerInput PlayerInput { get => _playerInput; set => _playerInput = value; }
    public Animator Anim { get => _anim; set => _anim = value; }
    public Rigidbody Rb { get => _rb; set => _rb = value; }
    public Detector GroundDetector { get => _groundDetector; set => _groundDetector = value; }
    public bool OhMyGodTheyKilledKenny { get => _ohMyGodTheyKilledKenny; set => _ohMyGodTheyKilledKenny = value; }

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _playerInput = GetComponent<PlayerInput>();

        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _groundDetector = GetComponent<Detector>();

        _playerInput.actions.FindActionMap("Player").Enable();
        _playerInput.actions.FindActionMap("UI").Disable();
    }

    private void Update()
    {
        CheckAnimatorTransitions();
    }

    #region crimen de guerra 1
    public void CheckAnimatorTransitions()
    {
        //Settear verificacion de caminar
        _anim.SetFloat("HorizontalMovementValue", _playerInput.actions.FindActionMap("Player").FindAction("Move").ReadValue<Vector2>().x);

        //Settear verificacion de velocidad en Y
        _anim.SetFloat("RBVerticalSpeed", _rb.linearVelocity.y);

        //Settear verifiacion de suelo
        _anim.SetBool("OnGround", _groundDetector.GetSomethingDetected());

        //Settear botones de escalar
        _anim.SetBool("ClimbUp", _playerInput.actions.FindActionMap("Player").FindAction("Move").ReadValue<Vector2>().y > 0.5f);
        _anim.SetBool("ClimbDown", _playerInput.actions.FindActionMap("Player").FindAction("Move").ReadValue<Vector2>().y < -0.5f);

        //Settear salto
        _anim.SetBool("PressJumpButton", _playerInput.actions.FindActionMap("Player").FindAction("Jump").IsPressed());

        //Settear muerte
        _anim.SetBool("OhMyGodTheyKilledKenny", _ohMyGodTheyKilledKenny);
    }
    #endregion
}
