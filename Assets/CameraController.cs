using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;

    private void Start()
    {
        // Change a specific camera setting
        mainCamera.backgroundColor = Color.red; // Change the background color to red
    }
}
