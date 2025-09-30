using System;
using System.Collections;
using UnityEngine;

public class MakeCoffee : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject cup;
    [SerializeField] private GameObject cupPosition;
    [SerializeField] private GameObject leftHand;

    private bool isDrinking = false;

    private void Update()
    {
        if (isDrinking)
        {
            cupPosition.transform.position = leftHand.transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameDayManager.instance.IsTaskAllowed(GameTask.DrinkCoffee))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (other.gameObject.CompareTag("CoffeeMachine"))
                {
                    anim.SetTrigger("MakeCoffee");
                    PlayerController.instance.isStucking = true;
                    StartCoroutine(Extras());
                }
            }
        }
    }

    private IEnumerator Extras()
    { 
        yield return new WaitForSeconds(1.5f);
        cup.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        isDrinking = true;
        yield return new WaitForSeconds(9f);
        Reset();
    }

    private void Reset()
    {
        PlayerController.instance.isStucking = false;
        cup.transform.position = cupPosition.transform.position;    
        cup.SetActive(false);
    }
}
