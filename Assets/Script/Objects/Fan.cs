using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 250f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}