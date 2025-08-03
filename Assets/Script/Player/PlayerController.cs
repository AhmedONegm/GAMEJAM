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
    public bool isSleeping = false;
    float duration = 1.0f;
    float elapsed = 0f;
    bool isInBedTrigger = false;
    bool isInOfficeTrigger = false;
   public bool isBusy = false;

    Collider currentTrigger;

    Animator animator;
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
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

            if (isSleeping)
            {
                StartCoroutine(WakeUpRoutine());
            }

            if (isInOfficeTrigger) 
            {
                StartCoroutine(TypingOnPC());
                //isBusy = true;
                //sprite.SetActive(true);
                //vase.SetActive(true);
                //videoManager.SetActive(true);
                //Debug.Log("typing");
                //transform.position = chairPoint.position;
                //transform.rotation = chairPoint.rotation;
                //animator.SetBool("isStanding", true);
                //isStucking = true;

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



    IEnumerator MoveToSleepPoint()
    {
     
        Vector3 start = transform.position;
        Vector3 end = sleepPoint.position;



        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / duration);
            transform.rotation = Quaternion.Lerp(transform.rotation, sleepPoint.rotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = end;

        isStucking = true;
        isSleeping = true;
        isBusy = true;
        AnimStatus(isSleeping);
    }

    IEnumerator WakeUpRoutine()
    {
        isStucking = false;
        isSleeping = false;
        AnimStatus(isSleeping); // Start wake-up animation
        Debug.Log("weakup");
        yield return new WaitForSeconds(0.5f); // Optional: let animation start first

        Vector3 startPos = transform.position;
        Vector3 endPos = weakupPoint.position;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            transform.rotation = Quaternion.Slerp(transform.rotation, weakupPoint.rotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        isBusy = true;
    }

    IEnumerator TypingOnPC()
    {
        isBusy = true;
        sprite.SetActive(true);
        vase.SetActive(true);
        videoManager.SetActive(true);
        Debug.Log("typing");
        transform.position = chairPoint.position;
        transform.rotation = chairPoint.rotation;
        animator.SetBool("isStanding", true);
        isStucking = true;

        yield return new WaitForSeconds(65f); // Example duration
        isStucking = false;
        isBusy = false;
        sprite.SetActive(false);
        vase.SetActive(false);
        videoManager.SetActive(false);
        animator.SetBool("isStanding", false);
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
