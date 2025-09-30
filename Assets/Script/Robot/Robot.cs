using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    public bool needsFixing = false;
    
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent agent;
    
    [Header("Values")]
    [SerializeField] private float rotationSpeed = 720f; 
    [SerializeField] private float yawOffset = 180f;       // set 180 if your model faces backward

    [Header("UI")] 
    [SerializeField] private GameObject happyFacePanel;
    [SerializeField] private GameObject angryFacePanel;
    [SerializeField] private GameObject textPanel;
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

        if (needsFixing == true)
        {
            happyFacePanel.SetActive(false);
            textPanel.SetActive(false);
            
            angryFacePanel.SetActive(true);
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

    public void Fix()
    {
        angryFacePanel.SetActive(false);
        textPanel.SetActive(false);
            
        happyFacePanel.SetActive(true); 
    }
}
