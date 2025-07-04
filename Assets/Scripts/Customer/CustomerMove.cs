using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMove : MonoBehaviour
{
    public Transform target;      // Initial target (counter)
    public Transform door;        // Exit door target
    public Transform lookSpot;    // Spot to face at the counter

    private NavMeshAgent agent;
    private Animator animator;
    private HeadController head;
    private Transform player;

    private float lookRotationSpeed = 180f; // Degrees per second
    private bool hasStoppedAtCounter = false;
    private bool isLeaving = false;

    void Start()
    {
        target = GameObject.FindWithTag("standspot").transform;
        door = GameObject.FindWithTag("door").transform;
        lookSpot = GameObject.FindWithTag("desk").transform;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        head = GetComponent<HeadController>();

        player = GameObject.FindWithTag("Player").transform;

        agent.updateRotation = false;
        head.Reset();

        agent.SetDestination(target.position);
    }

   void Update()
    {
        float speed = agent.velocity.magnitude;
        animator.SetBool("walk", speed > 0.1f);

        if (isLeaving && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && agent.velocity.sqrMagnitude == 0f)
        {
            CustomerSpawner.readyToSpawn = true;
            Destroy(gameObject);
            return;
        }

        if (!isLeaving && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!hasStoppedAtCounter)
            {
                AttachToCounter();
                hasStoppedAtCounter = true;
            }

            RotateTowards(lookSpot.position);
            LockToTargetPosition();
            head.TurnPlayerHead(player.position + Vector3.up * 2f);
        }
        else
        {
            RotateTowards(agent.destination);
        }
    }


    void AttachToCounter()
    {
        if (!Counter.orderReady)
        {
            Counter.GenerateOrder(this);
        }

        agent.isStopped = true;
    }

    public void Remove()
    {
        isLeaving = true;
        hasStoppedAtCounter = false;
        agent.SetDestination(door.position);
        agent.isStopped = false;
    }

    void RotateTowards(Vector3 direction)
    {
        direction -= transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized) * Quaternion.Euler(0, -90f, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, lookRotationSpeed * Time.deltaTime);
        }
    }

    void LockToTargetPosition()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }
}
