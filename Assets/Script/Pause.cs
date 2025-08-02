using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu; // Assign the pause menu GameObject in the inspector
    public GameObject gameOverMenu; // Assign the game over menu GameObject in the inspector
    public string mainMenu;

    public int playerHealth = 100; // Reference to the player's health
    public int enemyHealth = 100;  // Reference to the enemy's health

    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);  // Hide the pause menu initially
        }
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(false);  // Hide the game over menu initially
        }
        Time.timeScale = 1f;  // Ensure the game is running when it starts
        Cursor.visible = false;  // Hide the cursor initially
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center of the screen initially
    }

    void Update()
    {
        // Toggle the pause menu when the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        // Check for game over condition
        if (playerHealth <= 0 || enemyHealth <= 0)
        {
            ShowGameOverMenu();
        }
    }

    public void Resume()
    {
        // Hide the pause menu and resume the game
        TogglePauseMenu();
        Time.timeScale = 1f;  // Resume game time
        Cursor.visible = false;  // Hide cursor when resuming gameplay
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor back to the center when gameplay resumes
    }

    public void MainMenu()
    {
        Time.timeScale = 1f; // Reset time scale before loading the scene
        Cursor.visible = true; // Make sure the cursor is visible in the main menu
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        SceneManager.LoadScene(mainMenu); // Load the main menu scene
    }



    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif    
    }

    private void TogglePauseMenu()
    {
        if (pauseMenu != null)
        {
            bool isPaused = !pauseMenu.activeSelf;
            pauseMenu.SetActive(isPaused);

            // Pause or resume the game based on the state of the pause menu
            Time.timeScale = isPaused ? 0f : 1f;

            // Show the cursor when the game is paused
            if (isPaused)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;  // Unlock the cursor so the player can interact with the UI
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor back to center when the game resumes
            }
        }
    }

    private void ShowGameOverMenu()
    {
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(true); // Show the game over menu
            Time.timeScale = 0f; // Pause the game
            Cursor.visible = true; // Show the cursor
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        }
    }
}