using UnityEngine;

public class Timer : MonoBehaviour
{
    float currentTime;
    public float startingTime = 10f;
    [SerializeField] TMPro.TextMeshProUGUI countdownText;
    void Start()
    {
        currentTime = (int)startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime-= 1*Time.deltaTime;
        countdownText.text = ((int)currentTime).ToString();
         if (currentTime <= 0f)
         {
            currentTime = 0;
            countdownText.text = "Time's Up!";
            GameDayManager.instance.currentDay++;



        }
    }
}
