using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int totalItems;
    private int collectedItems;
    public Text itemsCollectedText;

    public int level;

    public TextMeshProUGUI levelText;

    // Timer variables
    public float levelTime = 15f;
    private float currentTime;
    public TextMeshProUGUI timerText; // Reference to the UI Text for displaying the timer

    // Add the scene index of the next level in the build settings
    public int nextLevelSceneIndex = 0;
    public int currentLevel = 0;

    private void Start()
    {

        level = nextLevelSceneIndex + 1;
        levelText.text = "Level: " + currentLevel.ToString();

        collectedItems = 0;
        UpdateItemsCollectedText();

    

        // Initialize the timer
        currentTime = levelTime;
        UpdateTimerText();

        
    }

    public void CollectItem()
    {
        collectedItems++;
        UpdateItemsCollectedText();

        if (collectedItems >= totalItems)
        {
            nextLevelSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            FinishLevel();
        }
    }

    private void UpdateItemsCollectedText()
    {
        if (itemsCollectedText != null)
        {
            itemsCollectedText.text = "Items Collected: " + collectedItems + " / " + totalItems;
        }
    }

    private void FinishLevel()
    {
        
        Debug.Log("Level Finished!");

        // Load the next level scene
        SceneManager.LoadScene(nextLevelSceneIndex);
    }

    private void Update()
    {
        
        if (collectedItems < totalItems)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();

            // Check if the timer is up
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                nextLevelSceneIndex = SceneManager.GetActiveScene().buildIndex;
                FinishLevel();
            }
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = currentTime.ToString("0");
        }
    }
}
