using Database;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject enemyPrefaf;
    public float minInitiateValue;
    public float maxInitiateValue;
    public float enemyDestroyTime = 10f;

    public GameObject starPrefab;
    public float minStarInitiateValue = -4f;
    public float maxStarInitiateValue = 4f;
    public float starDestroyTime = 50f;

    public Text scoreText;
    public int score = 0;
    public float increaseSpeedRate = 0.1f;
    public int scorePerStar = 10;
    public Text totalScoreText;
    [Header("Particle Effects")] public GameObject explosion;
    public GameObject muzzleFlash;

    [Header("Panels")] public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject endGameMenu;

    [Header("Player")]
    public GameObject player;
    public Transform playerSpawnPosition;

    [Header("Top3Scores")]
    public Text top1Text;
    public Text top2Text;
    public Text top3Text;

    [Header("Database")]
    public SqLiteGameDb database;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        endGameMenu.SetActive(false);
        Time.timeScale = 0f;
        InstantiatePlayer();
        InvokeRepeating("InstantiateEnemy", 1f, 2f);
        InvokeRepeating("InstantiateStar", 1f, 2f);
        UpdateScoreUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void InstantiateEnemy()
    {
        var enemyPos = new Vector3(Random.Range(minInitiateValue, maxInitiateValue), 6f);
        var enemy = Instantiate(enemyPrefaf, enemyPos, Quaternion.Euler(0f, 0f, 180f));
        var ec = enemy.GetComponent<EnemyController>();

        ec.speed += score * increaseSpeedRate;
        Destroy(enemy, enemyDestroyTime);
    }

    public void InstantiateParticles(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        var gm = Instantiate(gameObject, position, rotation);
        Destroy(gm, 2f);
    }

    public void InstantiateStar()
    {
        var starPos = new Vector3(Random.Range(minStarInitiateValue, maxStarInitiateValue), 6f);
        var star = Instantiate(starPrefab, starPos, Quaternion.Euler(0f, 0f, 180f));
        Destroy(star, starDestroyTime);
    }

    public void StartGameButton()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1f;
        ResetScore();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }


    public void QuitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    public void RestartGame()
    {
        endGameMenu.SetActive(false);
        Time.timeScale = 1f;
        InstantiatePlayer();
        ResetScore();

        scoreText.gameObject.SetActive(true);
        top1Text.gameObject.SetActive(false);
        top2Text.gameObject.SetActive(false);
        top3Text.gameObject.SetActive(false);
    }
    public void ShowEndGameMenu()
    {
        endGameMenu.SetActive(true);
        scoreText.gameObject.SetActive(false);
        totalScoreText.text = "Total Score: " + score;
        top1Text.gameObject.SetActive(true);
        top2Text.gameObject.SetActive(true);
        top3Text.gameObject.SetActive(true);

        if (database != null)
        {
            var topScores = database.getTop3Scores();

            top1Text.text = topScores.Count > 0 ? "Top 1: " + topScores[0] : "Top 1: -";
            top2Text.text = topScores.Count > 1 ? "Top 2: " + topScores[1] : "Top 2: -";
            top3Text.text = topScores.Count > 2 ? "Top 3: " + topScores[2] : "Top 3: -";
        }
    }

    public void RestartGameButton()
    {
        GameManager.instance.RestartGame();
    }
    void InstantiatePlayer()
    {
        Vector3 spawnPos = playerSpawnPosition != null
            ? playerSpawnPosition.position
            : Vector3.zero;

        Instantiate(player, spawnPos, Quaternion.identity);
    }

}