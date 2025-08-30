using Cinemachine;
using NUnit;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform sleepPoint;
    [SerializeField] Transform weakupPoint;
    [SerializeField] Transform chairPoint;
    [SerializeField] GameObject sprite;
    [SerializeField] GameObject vase;
    [SerializeField] GameObject videoManager;
    public bool isStucking = false;
    //public bool isSleeping = false;
    float duration = 1.0f;
    float elapsed = 0f;
    bool isInBedTrigger = false;
    bool isInOfficeTrigger = false;
   public bool isBusy = false;

    [SerializeField] Collider deskCollider;
    [SerializeField] Collider pcCollider;
    [SerializeField] CinemachineVirtualCamera pcAimCamera;
    [SerializeField] CinemachineVirtualCamera bedAimCamera;

    Collider currentTrigger;

    Animator animator;
    public static PlayerController instance;

    private void Awake()
    {
        
        instance = this;
    }

    void Start()
    {
        deskCollider.isTrigger = false;
        pcCollider.isTrigger = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isBusy) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInBedTrigger) 
            {
                StartCoroutine(MoveToSleepPoint());
            }


            if (isInOfficeTrigger) 
            {
                StartCoroutine(TypingOnPC());

            }
        }

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bed")
        {
            isInBedTrigger = true;
            currentTrigger = other;
            
        }
        if (other.tag == "Office" )
        {
            isInOfficeTrigger = true;
            currentTrigger = other;
         
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bed")
        {
            isInBedTrigger = false;
            currentTrigger = null;
        }
        if (other.tag == "Office")
        {
            isInOfficeTrigger = false;
            currentTrigger = null;
        }
    }



    IEnumerator MoveToSleepPoint()
    {
        // Go to sleep
        transform.SetParent(sleepPoint);
        transform.position = sleepPoint.position;   // use world position
        transform.rotation = sleepPoint.rotation;

        isStucking = true;
        //isSleeping = true;
        isBusy = true;
        AnimStatus(isStucking);
        bedAimCamera.Priority = 40;
        yield return new WaitForSeconds(10f); // Sleep duration

        // Wake up
        transform.SetParent(null); // detach before moving
        transform.position = weakupPoint.position;
        transform.rotation = weakupPoint.rotation;

        isStucking = false;
        //isSleeping = false;
        isBusy = false;
        AnimStatus(isStucking);
        bedAimCamera.Priority = 0;
    }



    IEnumerator TypingOnPC()
    {
        transform.SetParent(chairPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        PlayOnPC(true);
        yield return new WaitForSeconds(30f); // Example duration
        PlayOnPC(false);
        transform.SetParent(null);
    }


    private void PlayOnPC(bool isPlaying)
    {
        deskCollider.isTrigger = isPlaying;
        pcCollider.isTrigger = isPlaying;
        isStucking = isPlaying;
        isBusy = isPlaying;
        sprite.SetActive(isPlaying);
        vase.SetActive(isPlaying);
        videoManager.SetActive(isPlaying);
        animator.SetBool("isStanding", isPlaying);
        pcAimCamera.Priority =isPlaying? 40:0;
    }

    private void AnimStatus(bool isSleeping)
    {
        if (animator != null)
        {
            animator.SetBool("isLaying", isSleeping);
            animator.SetBool("isSleeping", isSleeping);
        }
    }
}
