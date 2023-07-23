using UnityEngine;
using UnityEngine.UI;

public class CollectibleItem : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle item collection here
            levelManager.CollectItem(); // Notify the LevelManager that an item has been collected
            Debug.Log("haaalllooooo");
            Destroy(gameObject); // Destroy the collectible item object
        }
    }
}
