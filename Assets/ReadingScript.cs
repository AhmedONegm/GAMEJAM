using Cinemachine;
using System;
using UnityEngine;

public class ReadingScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject book;
    [SerializeField] private Transform playerLookTarget;
    [SerializeField] Animator animator;

    private void Start()
    {
        book.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            virtualCamera.Follow=book.transform;
            virtualCamera.LookAt = book.transform;
            virtualCamera.m_Lens.FieldOfView = 5;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            animator.SetBool("ReadyToRead", true);
            book.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            virtualCamera.Follow= playerLookTarget.transform;
            virtualCamera.LookAt = null;
            virtualCamera.m_Lens.FieldOfView = 40;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            animator.SetBool("ReadyToRead", false);
            book.SetActive(false);
        }
    }
}
