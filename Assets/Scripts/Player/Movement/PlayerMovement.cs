using Scripts.Player;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float rotationSpeed = 20f;

    private Rigidbody rb;
    private bool canMove = true;

    private PlayerAnimationManager _animationManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _animationManager = GetComponent<PlayerAnimationManager>();
    }

    void OnEnable()
    {
        GameManager.Instance.OnStateChanged += HandleStateChanged;
        HandleStateChanged(GameManager.Instance.CurrentState);
    }

    void OnDisable()
    {
        GameManager.Instance.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(GameState state)
    {
        canMove = (state == GameState.Roaming);

        rb.isKinematic = (state == GameState.Combat);

        if (!rb.isKinematic)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {

        if (rb.isKinematic)
            return;

        if (!canMove)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

        Vector2 input = joystick.GetInput();
        Vector3 movement = new Vector3(input.x, 0f, input.y);

        bool isMoving = movement.magnitude > 0.2f;


        _animationManager.SetMoving(isMoving);

        if (isMoving)
        {
            rb.velocity = movement.normalized * moveSpeed;

            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
