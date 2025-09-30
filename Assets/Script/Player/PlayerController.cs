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
    [SerializeField] Transform pianoPoint;
    [SerializeField] GameObject sprite;
    [SerializeField] GameObject vase;
    [SerializeField] GameObject videoManager;
    public bool isStucking = false;
    public bool isReading = false;
    //public bool isSleeping = false;
    [SerializeField] float pianoTime = 30f;
    float duration = 1.0f;
    float elapsed = 0f;
    bool isInBedTrigger = false;
    bool isInOfficeTrigger = false;
    bool isInPianoTrigger = false;
    public bool isOnDiskTrigger = false;
    public bool isBusy = false;
    [SerializeField] Transform book;
    [SerializeField] Transform playerLookTarget;
    [SerializeField] Collider deskCollider;
    [SerializeField] Collider pcCollider;
    [SerializeField] Collider pianoCollider;
    [SerializeField] CinemachineVirtualCamera pcAimCamera;
    [SerializeField] CinemachineVirtualCamera bedAimCamera;
    [SerializeField] CinemachineVirtualCamera PianoAimCamera;

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
                if (GameDayManager.instance.IsTaskAllowed(GameTask.PlayPC))
                    StartCoroutine(TypingOnPC());

            }
            if (isInPianoTrigger)
            {
                if (GameDayManager.instance.IsTaskAllowed(GameTask.PlayPiano))
                    StartCoroutine(PlayOnPiano());

            }

            if (isOnDiskTrigger)
            {
                if (GameDayManager.instance.IsTaskAllowed(GameTask.ReadBook))
                {
                    if (isReading)
                    {
                        isReading = false;
                        ReadingScript.instance.read(book, 8, CursorLockMode.None, "ReadyToRead", true);
                    }
                    else
                    {
                        // Start reading
                        isReading = true;
                        ReadingScript.instance.read(playerLookTarget, 40, CursorLockMode.Locked, "ReadyToRead", false);
                    }
                }
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
        else if (other.tag == "Office" )
        {
            isInOfficeTrigger = true;
            currentTrigger = other;
         
        }
        else if (other.tag == "Piano")
        {
            isInPianoTrigger = true;
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
        if (other.tag == "Piano")
        {
            isInPianoTrigger = false;
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
        yield return new WaitForSeconds(20f); // Example duration
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


    IEnumerator PlayOnPiano()
    {
        transform.SetParent(pianoPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        PlayPiano(true);
        yield return new WaitForSeconds(pianoTime);
        PlayPiano(false);
        transform.SetParent(null);

    }
    private void PlayPiano(bool isPlaying)
    {
        pianoCollider.isTrigger = isPlaying;
        if (isPlaying) PianoGameManager.instance.StartGame(pianoTime);
        isStucking = isPlaying;
        isBusy = isPlaying;
        animator.SetBool("isPlayingPiano", isPlaying);
        PianoAimCamera.Priority = isPlaying ? 40 : 0;
         
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
