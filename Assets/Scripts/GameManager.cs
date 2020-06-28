using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI scoreText;
    [SerializeField]
    public int score;

    [SerializeField]
    public TextMeshProUGUI finalScoreText;

    public enum Stage { StageOne, StageTwo, StageFinal};

    private static GameManager instance;

    [SerializeField]
    public GameObject gameOver;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            if (instance == null)
            {
                instance = value;
            }

        }
    }

    void Start()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
            Debug.Log("Deleted - GameManager");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Debug.Log("Game Manager Instance Created");
        }
    }

    public bool gamePaused = false;

    public void GameOver()
    {
        PauseGame();
        finalScoreText.text = "Score: " + score;
        gameOver.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            gamePaused = false;
            gameOver.SetActive(false);
            score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
