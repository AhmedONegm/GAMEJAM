using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Workout : MonoBehaviour
{

    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject UIScreen;
    
    
    private void OnTriggerEnter(Collider other)
    {
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
        if (UIScreen.active)
        {
            if (Input.GetKey(KeyCode.G))
            {
                playerAnimator.SetTrigger("Crunch");
            }
            else if (Input.GetKey(KeyCode.H))
            {
                playerAnimator.SetTrigger("Burpee");
            }
            else if (Input.GetKey(KeyCode.J))
            {
                playerAnimator.SetTrigger("Squat");
            }
            else if (Input.GetKey(KeyCode.K))
            {
                playerAnimator.SetTrigger("Kick");
            }
            else if (Input.GetKey(KeyCode.L))
            {
                playerAnimator.SetTrigger("PushUp");
            }
            else if (Input.GetKey(KeyCode.F))
            {
                playerAnimator.SetTrigger("Rotation");
            }
        }
    }
}
