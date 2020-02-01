using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField]
    private float moveSpeed = 1f;

    private Rigidbody2D rb;
    //private Animator animator;

    [HideInInspector]
    public Vector2 movement { get; private set; }

    enum InputMethod
    {
        WASD,
        ARROWS
    }

    [SerializeField]
    private InputMethod inputMethod;

    private void Awake()
    {
        movement = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement = DetectMovement (movement);
        SetMovementAnimation();
    }

    private void FixedUpdate()
    {
        Move(rb, moveSpeed, movement);
    }

    Vector2 DetectMovement(Vector2 movement)
    {
        movement.x = 0;
        movement.y = 0;
        if (inputMethod == InputMethod.WASD)
        {
            if (Input.GetKey (KeyCode.D))
                movement.x = 1;

            if (Input.GetKey (KeyCode.A))
                movement.x = -1;

            if (Input.GetKey (KeyCode.W))
                movement.y = 1;

            if (Input.GetKey (KeyCode.S))
                movement.y = -1;
        }
        else if (inputMethod == InputMethod.ARROWS)
        {
            if (Input.GetKey (KeyCode.RightArrow))
                movement.x = 1;

            if (Input.GetKey (KeyCode.LeftArrow))
                movement.x = -1;

            if (Input.GetKey (KeyCode.UpArrow))
                movement.y = 1;

            if (Input.GetKey (KeyCode.DownArrow))
                movement.y = -1;
        }
        return movement;
    }
    public void Move(Rigidbody2D rb, float speed, Vector2 movement)
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    private void SetMovementAnimation()
    {
    }
}