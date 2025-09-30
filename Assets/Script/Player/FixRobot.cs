using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FixRobot : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject robot;

    private Robot roboScript;

    private void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        roboScript = robot.GetComponent<Robot>();
    }

    private void Update()
    {
        if (GameDayManager.instance.IsTaskAllowed(GameTask.FixRobot))
        {
            if (Vector3.Distance(robot.transform.position, gameObject.transform.position) < 1f
                && roboScript.needsFixing == true && Input.GetKeyDown(KeyCode.E))
            {
                Vector3 direction = robot.transform.position - transform.position;
                direction.y = 0f;

                if (direction.sqrMagnitude > 0.001f)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = lookRotation;
                }
                anim.SetTrigger("Fix");
                PlayerController.instance.isStucking = true;

                StartCoroutine(WaitForAnimationToFinish());
            }
        }
    }

    private IEnumerator WaitForAnimationToFinish()
    {
        yield return new WaitForSeconds(15f);
        PlayerController.instance.isStucking = false;
        roboScript.needsFixing = false;
    }

    
}
