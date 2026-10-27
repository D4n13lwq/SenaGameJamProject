using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(Detector))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] float acceleration = 60f;
    [SerializeField] float deceleration = 80f;
    [SerializeField] float rotationSpeed = 720f;

    [SerializeField] int maxAirJumps = 1;

    [SerializeField] float fallGravityMultiplier = 2f;

    public PlayerInput PlayerInput { get; private set; }
    public Rigidbody Rb { get; private set; }
    public Animator Anim { get; private set; }

    Detector groundDetector;

    InputAction moveAction;
    InputAction jumpAction;

    public Vector2 MoveInput { get; private set; }
    public bool JumpHeld => jumpAction != null && jumpAction.IsPressed();
    public bool IsGrounded => groundDetector.GetSomethingDetected();

    public bool MovementEnabled { get; set; } = true;
    public float HorizontalSpeed { get; set; } = 5f;

    int airJumpsUsed = 0;
    public bool CanAirJump => airJumpsUsed < maxAirJumps;
    public void ConsumeAirJump() => airJumpsUsed++;

    public HangZone CurrentHangZone { get; private set; }
    public HangZone ActiveHangZone { get; private set; }
    public bool IsHanging => ActiveHangZone != null;
    public bool CanHang => CurrentHangZone != null && IsGrounded && !IsHanging;

    public Vector3 StandPosition { get; private set; }
    public Vector3 KinematicTarget { get; set; }

    public void BeginHang(HangZone zone)
    {
        ActiveHangZone = zone;
        StandPosition = Rb.position;
        KinematicTarget = Rb.position;
        MovementEnabled = false;

        Rb.linearVelocity = Vector3.zero;
        Rb.isKinematic = true;
    }

    public void EndHang()
    {
        if (!IsHanging) return;

        Rb.isKinematic = false;
        Rb.position = KinematicTarget;
        Rb.linearVelocity = Vector3.zero;

        ActiveHangZone = null;
        MovementEnabled = true;
    }

    bool OhMyGodTheyKilledKenny = false;
    float facingAngle = 90f;

    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        PlayerInput = GetComponent<PlayerInput>();
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        groundDetector = GetComponent<Detector>();

        Rb.interpolation = RigidbodyInterpolation.Interpolate;
        Rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        Rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        InputActionMap playerMap = PlayerInput.actions.FindActionMap("Player");
        playerMap.Enable();
        PlayerInput.actions.FindActionMap("UI").Disable();

        moveAction = playerMap.FindAction("Move");
        jumpAction = playerMap.FindAction("Jump");
    }

    void Update()
    {
        MoveInput = moveAction.ReadValue<Vector2>();

        bool grounded = IsGrounded;

        if (grounded && !IsHanging && Rb.linearVelocity.y <= 0.01f)
            airJumpsUsed = 0;

        if (!IsHanging && jumpAction != null && jumpAction.WasPressedThisFrame())
        {
            if (grounded)
                Anim.SetTrigger("Jump");
            else if (CanAirJump)
                Anim.SetTrigger("DoubleJump");
        }

        CheckAnimatorTransitions();
    }

    void FixedUpdate()
    {
        if (IsHanging)
        {
            Rb.MovePosition(KinematicTarget);
            return;
        }

        if (!MovementEnabled) return;

        Vector3 velocity = Rb.linearVelocity;

        float targetX = MoveInput.x * HorizontalSpeed;
        float rate = Mathf.Abs(targetX) > 0.01f ? acceleration : deceleration;
        velocity.x = Mathf.MoveTowards(velocity.x, targetX, rate * Time.fixedDeltaTime);
        velocity.z = 0f;

        if (velocity.y < 0f)
            velocity += Physics.gravity * (fallGravityMultiplier - 1f) * Time.fixedDeltaTime;

        Rb.linearVelocity = velocity;

        if (Mathf.Abs(MoveInput.x) > 0.01f)
            facingAngle = MoveInput.x > 0f ? 90f : -90f;

        Quaternion targetRotation = Quaternion.Euler(0f, facingAngle, 0f);
        Rb.MoveRotation(Quaternion.RotateTowards(Rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HangZone zone))
            CurrentHangZone = zone;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out HangZone zone) && zone == CurrentHangZone)
            CurrentHangZone = null;
    }

    public void CheckAnimatorTransitions()
    {
        Anim.SetFloat("HorizontalMovementValue", MoveInput.x);
        Anim.SetFloat("RBVerticalSpeed", Rb.linearVelocity.y);
        Anim.SetBool("OnGround", IsGrounded);
        Anim.SetBool("ClimbUp", MoveInput.y > 0.5f);
        Anim.SetBool("ClimbDown", MoveInput.y < -0.5f);
        Anim.SetBool("CanHang", CanHang);
        Anim.SetBool("OhMyGodTheyKilledKenny", OhMyGodTheyKilledKenny);
    }
}