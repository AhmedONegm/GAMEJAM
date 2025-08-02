using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    private float _mouseSensitivity = 250f;
    private float _topClamp = -90f;
    private float _bottomClamp = 90f;
    private float _xRotation = 0f;
    private float _yRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, _topClamp, _bottomClamp);

        _yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }
}
