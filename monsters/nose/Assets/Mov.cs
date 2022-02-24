using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour
{

    CharacterController cc;

    Vector3 dir;

    Vector2 playerMove;
   

    float speed;

    InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Walk.performed += ctx => playerMove = ctx.ReadValue<Vector2>();
        inputActions.Player.Walk.canceled += ctx => playerMove = Vector2.zero;
    }


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    void Walk()
    {

        
            speed = 1.9f;
            dir = transform.TransformDirection(Vector3.forward);
          
       


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
