using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UIElements;

public class PlayerPull : MonoBehaviour
{
    public Rig ikRig;
    public float rayDistance = 10f;
    public float pullSpeed = 5f;
    public bool isPulling = false;
    
    private Transform pulledObject;

    private Vector3 velocity = Vector3.zero; // for SmoothDamp
    private Quaternion lockedRotation;       // fixed rotation while pulling
    private float pullDistance;              // keep constant distance
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPulling)
        {
            StartPull();
        }

        if (Input.GetKeyUp(KeyCode.P) && isPulling)
        {
            StopPull();
        }

        if (isPulling && pulledObject != null)
        {
            // Keep the player locked facing the box
            transform.rotation = lockedRotation;

            // Keep the same distance as when pulling started
            Vector3 targetPos = transform.position + transform.forward * pullDistance;
            targetPos.y = pulledObject.position.y;

            pulledObject.position = Vector3.SmoothDamp(
                pulledObject.position,
                targetPos,
                ref velocity,
                0.05f,
                pullSpeed
            );
        }
    }

    private void StartPull()
    {
        
        Vector3 pos = transform.position + new Vector3(0, 0.5f, 0);

        if (Physics.Raycast(pos, transform.forward, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.CompareTag("Pullable"))
            {
                isPulling = true;
                pulledObject = hit.collider.transform;

                // Lock rotation toward the box once
                Vector3 dir = pulledObject.position - transform.position;
                dir.y = 0; // only horizontal
                if (dir != Vector3.zero)
                {
                    lockedRotation = Quaternion.LookRotation(dir);
                }

                // Store the starting distance
                pullDistance = Vector3.Distance(transform.position, pulledObject.position);
                
                ikRig.weight = 1f;
            }
        }
    }

    private void StopPull()
    {
        ikRig.weight = 0f;
        isPulling = false;
        pulledObject = null;
    }
    
}
