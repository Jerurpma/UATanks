using UnityEngine;

public class Foreach : MonoBehaviour
{
    public GameObject[] gameObjects;

    private void Start()
    {
        // Iterate over each GameObject in the gameObjects array using a foreach loop
        foreach (GameObject obj in gameObjects)
        {
            // Do something with each GameObject
            Debug.Log("Game Object");
        }
    }
}
