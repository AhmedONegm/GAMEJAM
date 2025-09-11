using System;
using System.Collections;
using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform holdingPoint;
    [SerializeField] private Animator animator;

    private GameObject pickableGO;

    private void OnTriggerStay(Collider other)
    {
        // The object we collided with -- GIFT --
        pickableGO = other.gameObject;

        if (pickableGO.CompareTag("Pickable") && Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("Pickup");
            PlayerController.instance.isStucking = true;
            
            // To remove the camera glitch happens because of the collider of the box
            BoxCollider collider = pickableGO.GetComponent<BoxCollider>();
            if (collider != null) collider.enabled = false;

            StartCoroutine(PlayerDownTime());
        }
    }

    private IEnumerator PlayerDownTime()
    {
        yield return new WaitForSeconds(1.3f);

        if (pickableGO != null)
        {
            // Attach to the player's hand 
            pickableGO.transform.SetParent(handTransform);
            pickableGO.transform.localPosition = holdingPoint.transform.localPosition;
            pickableGO.transform.localRotation = Quaternion.identity;
            
            Destroy(pickableGO, 3f);
        }

        yield return new WaitForSeconds(5.6f);
        PlayerController.instance.isStucking = false;
    }
}
