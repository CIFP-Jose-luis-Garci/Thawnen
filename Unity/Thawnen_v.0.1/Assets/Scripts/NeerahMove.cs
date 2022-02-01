using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeerahMove : MonoBehaviour
{
    Animator animator;
    //Rigidbody rb;
    CharacterController cc;
    float speed;
    Vector3 dir;

    InputActions inputActions;
    Vector2 playerMove;
    float direction;

    bool running;
    bool desp;

    bool isGrounded;
    float jumpForce;

    float groundDistance;

    public float jumpSpeed = 58.0F;
    public float gravity = 9.8F;

    //Salto
    Vector3 moveDirection;

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
        //rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();

        groundDistance = 1.10f; 
        // isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {

        CheckGround();
       
    }
    private void FixedUpdate()
    {
        Walk();

        Turn();
        if (running && playerMove.y > 0)
        {
            animator.SetBool("Run", true);

            speed = 50f;

            dir = transform.TransformDirection(Vector3.forward);
           //rb.velocity = (dir * speed);
        }
    }

    void CheckGround()
    {
        /*
        Vector3 rayorigin = transform.position + new Vector3(0, 0, 0);
        Debug.DrawRay(rayorigin, Vector3.down * groundDistance, Color.red);


        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.2f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);

            isGrounded = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            isGrounded = false;
        }

        */

        isGrounded = cc.isGrounded;

       print(isGrounded);
    }

    void Walk()
    {
        speed = 2.5f;
        dir = transform.TransformDirection(Vector3.forward);
        //rb.velocity = (dir * speed * playerMove.y);
        cc.SimpleMove(dir * speed * playerMove.y);

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


        if (isGrounded)
        {
            //Creamos un nuevo Vector
            moveDirection = new Vector3(0, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
           // moveDirection *= speed;

            moveDirection.y = jumpSpeed;
            animator.SetTrigger("Jump");
        }
        else
        {
            moveDirection.y = 0f;
        }
            //Hacemos que en todo momento la gravedad se aplique en todo momento
            moveDirection.y -= gravity * Time.deltaTime;
           cc.Move(moveDirection * Time.deltaTime);

        


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
