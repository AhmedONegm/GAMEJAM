using System;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    public bool needsFixing = false;
    
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent agent;
    
    [SerializeField] private float rotationSpeed = 720f; 
    [SerializeField] private float yawOffset = 180f;       // set 180 if your model faces backward

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        if (needsFixing == false)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        agent.updateRotation = false;
        agent.updatePosition = true;
        agent.SetDestination(player.transform.position);
        
        Vector3 toPlayer = player.transform.position - transform.position;
        toPlayer.y = 0f; 
        if (toPlayer.sqrMagnitude > 0.0001f)
        {
            Quaternion target = Quaternion.LookRotation(toPlayer) * Quaternion.Euler(0f, yawOffset, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed * Time.deltaTime);
        }
    }
}
