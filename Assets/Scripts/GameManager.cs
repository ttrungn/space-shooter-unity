using UnityEditor;
using UnityEngine;

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

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;

    [Header("Panels")]
    public GameObject startMenu;
    public GameObject pauseMenu;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        InvokeRepeating("InstantiateEnemy", 1f, 2f);
        InvokeRepeating("InstantiateStar", 1f, 2f);
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
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }


    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}