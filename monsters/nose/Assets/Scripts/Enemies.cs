using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    Animator animator;
    [SerializeField] Transform player;

    [SerializeField] Vector3 goal;

    float goalDistance;

    bool detected;

    NavMeshAgent agent;

    [SerializeField] float visionRange = 25f; 
    [SerializeField] float visionConeAngle = 60f; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (goal != null)
        {
            goalDistance = Vector3.Distance(goal, transform.position);

            if (goalDistance > 0.9f && !detected)
            {
                animator.SetBool("walk", true);
            }
            else if (goalDistance > 0.9f && detected)
            {
               animator.SetBool("attack01", true);
                animator.SetBool("walk", false);
            }
            else if (goalDistance > 0.9f && !detected)
            {
                animator.SetBool("attack01", false);
                animator.SetBool("walk", false);

            }
        }
        if (detected)
        {
            print(goalDistance);
            if (goalDistance > 0.9f)
            {
                agent.speed = 4f;
            }
            else
            {
                agent.speed = 0f;
            }
            goal = player.position;

            agent.SetDestination(goal);

        }
        Detectar();
    }
    void Detectar()
    {
        Vector3 playerPosition = player.position;
        Vector3 vectorToPlayer = playerPosition - transform.position;


        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        float angleToPlayer = Vector3.Angle(transform.forward, vectorToPlayer);

        //Si está en mi rango y en mi ángulo de visión
        if (distanceToPlayer <= visionRange && angleToPlayer <= visionConeAngle)
        {
            print("Me han pillado");
            detected = true;

        }
        else
        {
            detected = false;
        }



    }



}
