using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoController : MonoBehaviour
{
    [SerializeField]
    float playerSpeed = 3f;

    bool goRight = true;

    public GameManager.Stage stage;

    float playerSize;

    Animator playerAnimator;
    float initialSpeed;

    AudioSource audioSource;
    [SerializeField]
    AudioClip gulpSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialSpeed = playerSpeed;
        initialSpeed = playerSpeed;
        playerSize = transform.localScale.x;
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && goRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
            playerAnimator.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !goRight)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
            playerAnimator.SetBool("Walk", true);
        }
        else
        {
            playerAnimator.SetBool("Walk", false);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            goRight = !goRight;
        }
    }

    public void StageOneIncrement()
    {
        gameObject.transform.localScale = new Vector2((playerSize + (playerSize * 0.25f)), (playerSize + (playerSize * 0.25f)));
        playerSpeed = playerSpeed * 0.75f;
    }

    public void StageTwoIncrement()
    {
        gameObject.transform.localScale = new Vector2((playerSize + (playerSize * 1.25f)), (playerSize + (playerSize * 1.25f)));
        playerSpeed = playerSpeed * 0.25f;
    }

    public void StageFinalIncrement()
    {
        gameObject.transform.localScale = new Vector2(playerSize, playerSize);
        GameManager.Instance.GameOver();
        GameManager.Instance.PauseGame();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Limit"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.collider.CompareTag("Obstacle"))
        {
            audioSource.clip = gulpSound;
            audioSource.Play();
            switch (stage)
            {
                case GameManager.Stage.StageOne:
                    StageOneIncrement();
                    stage = GameManager.Stage.StageTwo;
                    break;
                case GameManager.Stage.StageTwo:
                    StageTwoIncrement();
                    stage = GameManager.Stage.StageFinal;
                    break;
                case GameManager.Stage.StageFinal:
                    StageFinalIncrement();
                    stage = GameManager.Stage.StageOne;
                    break;
                default:
                    break;
            }
        }
        else if (collision.collider.CompareTag("PowerUp"))
        {
            audioSource.clip = gulpSound;
            audioSource.Play();
            switch (stage)
            {
                case GameManager.Stage.StageOne:
                    break;
                case GameManager.Stage.StageTwo:
                    gameObject.transform.localScale = new Vector2(playerSize, playerSize);
                    playerSpeed = initialSpeed;
                    stage = GameManager.Stage.StageOne;
                    break;
                case GameManager.Stage.StageFinal:
                    StageOneIncrement();
                    stage = GameManager.Stage.StageTwo;
                    break;
                default:
                    break;
            }
        }
    }
}
