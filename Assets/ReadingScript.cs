using Cinemachine;
using System;
using UnityEngine;

public class ReadingScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject book;
    [SerializeField] private Transform playerLookTarget;
    [SerializeField] Animator animator;
    public static ReadingScript instance;
    private void Start()
    {
        book.SetActive(false);
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.isOnDiskTrigger = true;
        }
    }
    public void read(Transform lookAtTransform, int cameraFOV, CursorLockMode mode, string animatorBool, bool Bool)
    {
        virtualCamera.Follow = lookAtTransform.transform;
        virtualCamera.LookAt = lookAtTransform.transform;
        virtualCamera.m_Lens.FieldOfView = cameraFOV;
        Cursor.lockState = mode;
        Cursor.visible = Bool;
        if (Bool)
        {
            animator.SetBool(animatorBool, false); // Reset first
            animator.SetBool(animatorBool, true);  // Then enable
        }
        else
        {
            animator.SetBool(animatorBool, false);
        }
        book.SetActive(Bool);
        PlayerController.instance.isStucking = Bool;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.isOnDiskTrigger = false;
            if (PlayerController.instance.isReading)
            {
                PlayerController.instance.isReading = false;
                read(playerLookTarget, 40, CursorLockMode.Locked, "ReadyToRead", false);
            }
        }
    }
}
