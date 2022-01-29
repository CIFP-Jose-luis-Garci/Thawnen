using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeerahMove : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    float speed;
    Vector3 dir;

    InputActions inputActions;
    Vector2 playerMove;
    float direction;

    bool running;
    bool desp;

    bool isGrounded;
    float jumpForce;


    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.Walk.performed += ctx => playerMove = ctx.ReadValue<Vector2>();
        inputActions.Player.Walk.canceled += ctx => playerMove = Vector2.zero;

        inputActions.Player.Run.started += _ => StartRun();
        inputActions.Player.Run.canceled += _ => StopRun();

        inputActions.Player.WeakAttack.performed += _ => WeakAttack();

        inputActions.Player.StrongAttack.performed += _ => StrongAttack();

        inputActions.Player.Jump.performed += _ => Jump();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {

      
       
    }
    private void FixedUpdate()
    {
        Walk();
        Turn();
        if (running && playerMove.y > 0)
        {
            animator.SetBool("Run", true);

            speed = 9f;

            dir = transform.TransformDirection(Vector3.forward);
            rb.velocity = (dir * speed);
        }
    }

    void Walk()
    {
        speed = 2.5f;
        dir = transform.TransformDirection(Vector3.forward);
        rb.velocity = (dir * speed * playerMove.y);

        animator.SetFloat("Walk", playerMove.y);
        

    }
    void StartRun()
    {

        running = true;

    }
    void StopRun()
    {
        animator.SetBool("Run", false);
        speed = 2.5f;
        running = false;
    }
    void Turn()
    {
        transform.Rotate(0f, playerMove.x * 0.6f, 0f);
        animator.SetFloat("SideWalk", playerMove.x);
    }

    void WeakAttack()
    {
        animator.SetTrigger("Weak Attack");
    }
    void StrongAttack()
    {
        animator.SetTrigger("Strong Attack");
    }

    void Jump()
    {
        if (isGrounded == true)
        {
            jumpForce = 50f;
            animator.SetTrigger("Jump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);

        }

    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
