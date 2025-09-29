using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.UI;
using System.Collections;

public class PianoGameManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text letterText;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public GameObject panelUI;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip correctSfx; // "Tenn"
    public AudioClip wrongSfx;   // "Bimm"

    [Header("Game Settings")]
    //public float roundTime = 30f; // seconds for the game
    private float timer;
    private int score = 0;
    private char currentLetter;

    private bool gameActive = false;
    public static PianoGameManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //StartGame();
    }

    void Update()
    {
        if (!gameActive) return;

        // Timer countdown
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();

        if (timer <= 0)
        {
            EndGame();
        }

        // Player input
        if (Input.anyKeyDown)
        {
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    string pressedKey = kcode.ToString();

                    // Only check letters (A-Z)
                    if (pressedKey.Length == 1 && char.IsLetter(pressedKey[0]))
                    {
                        CheckInput(pressedKey[0]);
                        break;
                    }
                }
            }
        }
    }

    public void StartGame(float roundTime)
    {
        panelUI.gameObject.SetActive(true);
        score = 0;
        timer = roundTime;
        gameActive = true;

        if (musicSource != null) musicSource.Play();

        GenerateNewLetter();
        UpdateScoreUI();
    }

    void EndGame()
    {
        gameActive = false;
        if (musicSource != null) musicSource.Stop();
        letterText.text = "Game Over!";
        StartCoroutine(HideUIAfterDelay());
    }
    private IEnumerator HideUIAfterDelay()
    {
        yield return new WaitForSeconds(5f); // wait 5 seconds
        panelUI.SetActive(false);
    }
    void GenerateNewLetter()
    {
        currentLetter = (char)Random.Range(65, 91); // A-Z
        letterText.text = currentLetter.ToString();
        letterText.color = new Color(1f, 1f, 1f, 0.5f); // semi-light
    }

    void CheckInput(char input)
    {
        if (char.ToUpper(input) == currentLetter)
        {
            // Correct input
            score++;
            letterText.color = Color.green;
            if (sfxSource && correctSfx) sfxSource.PlayOneShot(correctSfx);
        }
        else
        {
            // Wrong input
            letterText.color = Color.red;
            if (sfxSource && wrongSfx) sfxSource.PlayOneShot(wrongSfx);
        }

        UpdateScoreUI();
        Invoke(nameof(GenerateNewLetter), 0.5f); // delay before showing new letter
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
