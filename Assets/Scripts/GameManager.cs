using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int highScore = 0;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public GameObject gameOverPanel;
    public GameObject getReadyPanel;

    public AudioSource scoreSound;
    public AudioSource gameOverSound;

    private const string HIGHSCORE_KEY = "HighScore";

    public bool IsPlaying { get; private set; } = false;
    public bool IsStarting { get; private set; } = true;

    private PlayerController playerController;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        playerController = FindFirstObjectByType<PlayerController>();
    }

    void Start()
    {
        score = 0;
        UpdateScoreText();

        highScore = PlayerPrefs.GetInt(HIGHSCORE_KEY);
        UpdateHighScoreText();

        gameOverPanel.SetActive(false);
        // Time.timeScale = 0;
        IsStarting = true;
        playerController.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    void Update()
    {
        if (IsStarting)
        {
            bool isTapped = Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0);
            if (isTapped)
            {
                getReadyPanel.SetActive(false);
                IsStarting = false;
                IsPlaying = true;
                // Time.timeScale = 1;
                playerController.GetComponent<Rigidbody2D>().gravityScale = 2f;
                playerController.Jump();
            }
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
    void UpdateHighScoreText()
    {
        highScoreText.text = $"HI: {highScore}";
    }

    public void AddScore()
    {
        score++;
        UpdateScoreText();

        if (score == highScore + 1)
        {
            scoreSound.pitch = Random.Range(0.7f, 1.3f);
            scoreSound.PlayDelayed(1);
            scoreSound.pitch = Random.Range(0.7f, 1.3f);
            scoreSound.PlayDelayed(2);
        }
        scoreSound.pitch = Random.Range(0.7f, 1.3f);
        scoreSound.Play();
    }

    public void GameOver(float delay = 0)
    {
        // Time.timeScale = 0;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HIGHSCORE_KEY, highScore);
            PlayerPrefs.Save();
            UpdateHighScoreText();
        }
        IsPlaying = false;

        if (delay > 0)
        {
            StartCoroutine(ShowDelayedGameOver(delay));
        }
        else
        {
            ShowGameOver();
        }
    }

    private void ShowGameOver()
    {
        gameOverSound.PlayDelayed(.5f);
        gameOverPanel.SetActive(true);
    }

    private IEnumerator ShowDelayedGameOver(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowGameOver();
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
