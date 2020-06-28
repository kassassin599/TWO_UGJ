using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;

    public enum Stage { StageOne, StageTwo, StageFinal};

    private static GameManager instance;

    public Stage stage;

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

    void Awake()
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

    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }

}
