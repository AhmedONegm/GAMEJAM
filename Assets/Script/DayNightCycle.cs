using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light Light;
    public float rotationSpeed;

    private void Update()
    {
        Light.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
