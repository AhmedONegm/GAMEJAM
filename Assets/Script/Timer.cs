using UnityEngine;

public class Timer : MonoBehaviour
{
    float currentTime;
    public float startingTime = 10f;
    [SerializeField] TMPro.TextMeshProUGUI countdownText;
    [SerializeField] TMPro.TextMeshProUGUI day;

    void Start()
    {
        currentTime = (int)startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = ((int)currentTime).ToString();
        if (currentTime <= 0f)
        {
            currentTime = 0;
            countdownText.text = "Time's Up!";
            GameDayManager.instance.currentDay++;
            day.text = "Day " + GameDayManager.instance.currentDay;
            if (GameDayManager.instance.currentDay == 5)
            {
                // All days completed, show game over or victory message
                countdownText.text = "All Days Completed!";
                Invoke("ResetGame", 2f); // Wait for 2 seconds before resetting
                currentTime = startingTime;
            }
            else
            {
                // Reset the timer for the next day
                currentTime = startingTime;
            }
        }
    }

    void ResetGame()
    {
        GameDayManager.instance.currentDay = 0;
        // Reset the timer for the next day
        // You might want to pause the game or show a victory screen here
        day.text = "Try again!";
    }
}
