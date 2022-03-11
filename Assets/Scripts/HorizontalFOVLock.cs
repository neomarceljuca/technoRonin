using UnityEngine;

// Lock the cameras horizontal field of view so it will frame the same view in the horizontal regardless of aspect ratio.

[RequireComponent(typeof(Camera))]
public class HorizontalFOVLock : MonoBehaviour
{

    public float fixedHorizontalFOV = 30;

    Camera cam;

    void Awake()
    {
        GetComponent<Camera>().fieldOfView = 2 * Mathf.Atan(Mathf.Tan(fixedHorizontalFOV * Mathf.Deg2Rad * 0.5f) / GetComponent<Camera>().aspect) * Mathf.Rad2Deg;
    }
}