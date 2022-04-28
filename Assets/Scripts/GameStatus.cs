using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStatus : MonoBehaviour
{
    Image livesIndicator;
    RectTransform rectTransform;
    TextMeshProUGUI keysCounter;
    ScenePersistance scenePersistance;

    public readonly int MAX_LIVES = 4;

    [Range(0, 4)] [SerializeField] public int lives = 3;
    [SerializeField] public int keys = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        //Singleton
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        GameObject temp;
        temp = GameObject.Find("LivesIndicator");
        livesIndicator = temp.GetComponent<Image>();
        rectTransform = livesIndicator.GetComponent<RectTransform>();
        temp = GameObject.Find("TextKeysCounter");
        keysCounter = temp.GetComponent<TextMeshProUGUI>();
        UpdateUILives(lives);
        UpdateKeysNumber(keys);
        scenePersistance = FindObjectOfType<ScenePersistance>();
        scenePersistance.DeleteObjects();
    }

    // Update is called once per frame
    void Update()
    {
        scenePersistance.DeleteObjects();
    }

    public void UpdateUILives(int playerLives)
    {
        if (playerLives > MAX_LIVES)
            playerLives = MAX_LIVES;
        else if (playerLives < 0)
            playerLives = 0;
        rectTransform.sizeDelta = new Vector2(45 * playerLives, rectTransform.sizeDelta.y);
    }

    public void UpdateKeysNumber(int keys)
    {
        keysCounter.text = "x " + keys;
    }

    public void ProcessPlayerDeath()
    {
        DecLives();
        if (HasFinishedLives())
            ResetGameManager();
        else
            RestartLevel();
    }

    private static void RestartLevel()
    {
        var currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndexScene);
    }

    private void ResetGameManager()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Restart()
    {
        Destroy(gameObject);
    }

    public bool IncLives()
    {
        if (lives < MAX_LIVES)
        {
            lives++;
            UpdateUILives(lives);
            return true;
        }
        return false;
    }

    private void DecLives()
    {
        lives--;
        UpdateUILives(lives);
    }

    public bool HasFinishedLives()
    {
        return lives < 0;
    }

    public void IncKeys()
    {
        keys++;
        UpdateKeysNumber(keys);
    }

    public void DecKeys()
    {
        if (keys > 0)
        {
            keys--;
            UpdateKeysNumber(keys);
        }
    }
}
