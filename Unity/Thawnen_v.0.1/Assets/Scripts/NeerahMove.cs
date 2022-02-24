using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeerahMove : MonoBehaviour
{
    Animator animator;
    CharacterController cc;
    float speed;
    Vector3 dir;

    InputActions inputActions;
    Vector2 playerMove;
    bool running;

    Vector3 despl;

    float jumpHeight = 0.3f;
    float gravityValue = -9.81f;
    bool saltando = false;
    Vector3 dirJump;

    //Giro
    Vector2 rightStick;

    [SerializeField] Camera freeCamera;
    float turnSmoothTime = 0.2f;
    float turnSmoothVelocity = 30f;

    //Ataque giro
    [SerializeField] GameObject slash;
    [SerializeField] Transform slashPosition;  

   
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

        inputActions.Camera.Pivot.performed += ctx => rightStick = ctx.ReadValue<Vector2>();
        inputActions.Camera.Pivot.canceled += ctx => rightStick = Vector2.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
      
        cc = GetComponent<CharacterController>();


        slash.SetActive(true);

        slashPosition = GameObject.Find("Slash").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (running && playerMove.y > 0)    
        {
            animator.SetBool("Run", true);

            speed = 3f;

            dir = transform.TransformDirection(Vector3.forward);
            cc.SimpleMove(dir * speed * playerMove.y);
     
        }
        else
        {
            Walk();
        }

        Rotar();
        Salto();
    }

    void Walk()
    {
        speed = 1.9f;
        dir = transform.TransformDirection(Vector3.forward);
        animator.SetFloat("Walk", playerMove.y);

       
    }

    void Salto()
    {
        //Si estoy tocando suelo, y cayendo me paro
        if (cc.isGrounded && dirJump.y < 0)
        {
            dirJump = Vector3.zero;
        }

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
    void StartRun()
    {

        running = true;

    }
    void StopRun()
    {
        animator.SetBool("Run", false);
        speed = 1.9f;
        running = false;
    }
  

    void WeakAttack()
    {
        animator.SetTrigger("Weak Attack");
    }
    void StrongAttack()
    {
        animator.SetTrigger("Strong Attack");
        Instantiate(slash, slashPosition);
        //Transform particula = GameObject.Find("white-blue bolder(Clone)").GetComponent<Transform>();
        //Destroy(particula);
        //print(particula.position);
    }

    void Rotar()
    {
        
        float targetAngle = Mathf.Atan2(despl.x, despl.z) * Mathf.Rad2Deg + freeCamera.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        animator.SetFloat("SideWalk", rightStick.x);
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
