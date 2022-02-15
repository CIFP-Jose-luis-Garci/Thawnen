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

    Vector3 despl;

    bool isGrounded;
    float jumpHeight = 4f;
    float gravityValue = -9.81f;
    bool saltando = false;
    Vector3 dirJump;

    float groundDistance;

    public float jumpSpeed = 58.0F;
    public float gravity = 9.8F;

    //Salto
    Vector3 moveDirection;

    [SerializeField] Camera freeCamera;
    float turnSmoothTime = 0.2f;
    float turnSmoothVelocity = 30f;

    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.Walk.performed += ctx => playerMove = ctx.ReadValue<Vector2>();
        inputActions.Player.Walk.canceled += ctx => playerMove = Vector2.zero;

        inputActions.Player.Run.started += _ => StartRun();
        inputActions.Player.Run.canceled += _ => StopRun();

        inputActions.Player.WeakAttack.performed += _ => WeakAttack();

        inputActions.Player.StrongAttack.performed += _ => StrongAttack();

        inputActions.Player.Jump.performed += _ => { saltando = true; };
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
        Rotar();
        //Turn();
       
    }
    private void FixedUpdate()
    {
        Walk();

       
        if (running && playerMove.y > 0)
        {
            animator.SetBool("Run", true);

            speed = 9f;

            dir = transform.TransformDirection(Vector3.forward);
            cc.SimpleMove(dir * speed * playerMove.y);
           //rb.velocity = (dir * speed);
        }
    }

    void CheckGround()
    {
       
        isGrounded = cc.isGrounded;

    }

    void Walk()
    {
        speed = 2.5f;
        dir = transform.TransformDirection(Vector3.forward);
        //rb.velocity = (dir * speed * playerMove.y);
        cc.SimpleMove(dir * speed * playerMove.y);

        animator.SetFloat("Walk", playerMove.y);

        if (saltando && cc.isGrounded)
        {
            //Aplicamos un empuje hacia arriba en el vector de desplazamiento del salto

            dirJump.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            //Lo ponemos en false para que este IF se ejecute solo una vez
            saltando = false;
            //cc.Move(dirJump * Time.deltaTime);
            animator.SetTrigger("Jump");
        }
        //Hacemos que la caída sea suave
        dirJump.y += gravityValue * Time.deltaTime;
        //El vector de movimiento final será el de hacia adelante más el del salto
        Vector3 dirFinal = (dir * speed * playerMove.y) + dirJump;
        cc.Move(dirFinal * Time.deltaTime);

    }
    /*void Turn()
    {
        transform.Rotate(0f, playerMove.x * 0.6f, 0f);
        animator.SetFloat("SideWalk", playerMove.x);
    }*/
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
  

    void WeakAttack()
    {
        animator.SetTrigger("Weak Attack");
    }
    void StrongAttack()
    {
        animator.SetTrigger("Strong Attack");
    }

    /*void Jump()
    {


        if (cc.isGrounded && despl.y < 0)
        {
            despl.y = 0f;
        }
        {
            moveDirection.y = 0f;
        }
        if (cc.isGrounded)
        {
            //Aplicamos un empuje hacia arriba en el vector de desplazamiento
            despl.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        despl.y += gravityValue * Time.deltaTime;
        cc.Move(despl * Time.deltaTime);
        animator.SetTrigger("Jump");

    }*/
    void Rotar()
    {
        
        float targetAngle = Mathf.Atan2(despl.x, despl.z) * Mathf.Rad2Deg + freeCamera.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        animator.SetFloat("SideWalk", freeCamera.transform.eulerAngles.y);
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
