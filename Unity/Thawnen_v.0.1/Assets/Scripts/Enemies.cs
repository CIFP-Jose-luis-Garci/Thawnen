using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    Vector3 goal;

    NavMeshAgent agent;

    Transform player;
    bool detected = false;

    [SerializeField] float visionRange = 150f;
    [SerializeField] float visionConeAngle = 60f;
    float goalDistance;

    Animator animator;

    //RigidBody
    Rigidbody rb;
    //Si estoy reculando
    bool stepBack = false;

    //Distancia a la que se para del jugador
    [SerializeField] float stoppingDistance;

    bool round;

    float damage = 0;

    [SerializeField] GameObject hit;
    [SerializeField] Transform hitPosition;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        stoppingDistance = 1.5f;

        animator = GetComponent<Animator>();
        
        StartCoroutine("Round");

        player = GameObject.Find("NeraahPrefab").GetComponent<Transform>();
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
            animator.SetBool("walk", true);
            animator.SetBool("idle", false);
        }
        else
        {
            if(detected)
            {
                
                rb.AddForce(-transform.forward * 500);
                stepBack = true;
            }

            animator.SetBool("walk", false);
            animator.SetBool("idle", true);
         }

        agent.SetDestination(goal);
    }


    void Attack()
    {
        if(goalDistance > 1.5f && goalDistance < 3f && detected)
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
                visionConeAngle = 60;
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

    void Damage()
    {
        print(damage);
        damage++;
        animator.SetBool("hit", true);
        animator.SetFloat("die", damage);
        
        

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
            goal = SetRandomGoal(transform.position, 15f);
            yield return new WaitForSeconds(10f);
            agent.SetDestination(goal);
        }
    }
}
