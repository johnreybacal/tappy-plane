using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;

    public AudioSource scoreSound;
    public AudioSource gameOverSound;

    public bool IsPlaying { get; private set; } = true;

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
    }

    void Start()
    {
        score = 0;
        UpdateScoreText();

        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore()
    {
        score++;
        UpdateScoreText();
        scoreSound.pitch = Random.Range(0.7f, 1.3f);
        scoreSound.Play();
    }

    public void GameOver()
    {
        // Time.timeScale = 0;
        IsPlaying = false;
        gameOverSound.PlayDelayed(1f);
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
