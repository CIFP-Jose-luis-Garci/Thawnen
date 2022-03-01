using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    
    Vector3 goal;

    NavMeshAgent agent;


    [SerializeField] Transform player;
    bool detected = false;

    [SerializeField] float visionRange = 150f;
    [SerializeField] float visionConeAngle = 360f;
    float goalDistance;

    Animator animator;

    //RigidBody
    Rigidbody rb;
    //Si estoy reculando
    bool stepBack = false;

    //Distancia a la que se para del jugador
    [SerializeField] float stoppingDistance;

    bool round;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        stoppingDistance = 1.5f;

        animator = GetComponent<Animator>();


        StartCoroutine("Round");

        //goal = transform.position;
    }



    // Update is called once per frame
    void Update()
    {

        
        Movimiento();
      
        Detect();

        Attack();
    }

    void Movimiento()
    {

            goalDistance = Vector3.Distance(transform.position, goal);
            print(goalDistance);
            agent.speed = 5f;
        if(detected)
        {
            agent.stoppingDistance = stoppingDistance;
        }
        else
        {
            agent.stoppingDistance = 0f;
        }
            
            if (goalDistance > agent.stoppingDistance)
            {
                //agent.speed = 5f;
                animator.SetBool("walk", true);
                animator.SetBool("idle", false);
            }
            else
            {
                if(detected)
                {
                print("empujon");
                    rb.AddForce(-transform.forward * 500);
                    stepBack = true;
                }
                    
                //Vector3 newGoal = -transform.forward;
                //agent.SetDestination(newGoal);
                //agent.speed = 0f;
                animator.SetBool("walk", false);
                animator.SetBool("idle", true);
            }

        agent.SetDestination(goal);


    }


    void Attack()
    {
        if(goalDistance > 1.5f && goalDistance < 3f)
        {
            animator.SetBool("attack01", true);
        }
    }
    

    Vector3 SetRandomGoal(Vector3 zombiPos, float randomValue = 15f)
    {

        Vector3 returnPos;

        float newX = zombiPos.x + Random.Range(-randomValue, randomValue);
        float newZ = zombiPos.z + Random.Range(-randomValue, randomValue);

        returnPos = new Vector3(newX, zombiPos.y, newZ);

        return returnPos;
    }

    void Detect()
    {

        Vector3 playerPosition = player.position;
        Vector3 vectorToPlayer = playerPosition - transform.position;


        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        float angleToPlayer = Vector3.Angle(transform.forward, vectorToPlayer);

        if (distanceToPlayer <= visionRange && angleToPlayer <= visionConeAngle)
        {

            detected = true;
            goal = playerPosition;
            if (distanceToPlayer < 5)
            {
                visionConeAngle = 360;
            }
            else
            {
                visionConeAngle = 360;
            }

            if (round)
            {
                StopCoroutine("Round");
                round = false;
            }


        }
        else
        {

            detected = false;
            if (!round)
            {
                StartCoroutine("Round");
            }
        }



    }

    IEnumerator Round()
    {
        
        round = true;
        agent.speed = 0f;
        goal = transform.position;
        animator.SetBool("walk", false);
        animator.SetBool("attack01", false);
        while (!detected)
        {
            
            //agent.speed = 0.4f;
            goal = SetRandomGoal(transform.position, 15f);
            print("Haciendo ronda" + goal);
            yield return new WaitForSeconds(10f);

            agent.SetDestination(goal);

        }
    }



}
