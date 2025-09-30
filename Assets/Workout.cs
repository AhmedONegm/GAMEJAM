using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static Unity.Burst.Intrinsics.X86.Avx;
using System;
using System.Collections;

public class Workout : MonoBehaviour
{

    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject UIScreen;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (GameDayManager.instance.IsTaskAllowed(GameTask.Fitness))

            UIScreen.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        UIScreen.SetActive(false);
    }
    void Start()
    {
        UIScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameDayManager.instance.IsTaskAllowed(GameTask.Fitness))
        {
            if (UIScreen.active)
            {
                if (Input.GetKey(KeyCode.G))
                {
                    playerAnimator.SetTrigger("Crunch");
                    StartCoroutine(TimerFitness());

                }
                else if (Input.GetKey(KeyCode.H))
                {
                    playerAnimator.SetTrigger("Burpee");
                    StartCoroutine(TimerFitness());

                }
                else if (Input.GetKey(KeyCode.J))
                {
                    playerAnimator.SetTrigger("Squat");
                    StartCoroutine(TimerFitness());

                }
                else if (Input.GetKey(KeyCode.K))
                {
                    playerAnimator.SetTrigger("Kick");
                    StartCoroutine(TimerFitness());

                }
                else if (Input.GetKey(KeyCode.L))
                {
                    playerAnimator.SetTrigger("PushUp");
                    StartCoroutine(TimerFitness());

                }
                else if (Input.GetKey(KeyCode.F))
                {
                    playerAnimator.SetTrigger("Rotation");
                    StartCoroutine(TimerFitness());

                }
            }
        }
    }



private IEnumerator TimerFitness()
{
    PlayerController.instance.isStucking = true;
    yield return new WaitForSeconds(5f);
    PlayerController.instance.isStucking = false;

}
}