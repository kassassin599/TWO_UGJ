using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneController : MonoBehaviour
{
    [SerializeField]
    float playerSpeed = 3f;

    [SerializeField]
    LineRenderer line;

    public float lineLength = 10.0f;
    public LayerMask layerMask;

    RaycastHit2D hit;

    bool goRight = true;

    public GameManager.Stage stage;

    float playerSize;

    Animator playerAnimator;

    float initialSpeed;

    AudioSource audioSource;
    [SerializeField]
    AudioClip gulpSound;

    [SerializeField]
    Transform rayOrigin;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialSpeed = playerSpeed;
        Physics2D.queriesStartInColliders = false;
        playerSize = transform.localScale.x;
        playerAnimator = GetComponent<Animator>();
    }

    bool gamePause = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && goRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
            playerAnimator.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !goRight)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
            playerAnimator.SetBool("Walk", true);
        }
        else
        {
            playerAnimator.SetBool("Walk", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            goRight = !goRight;
        }

        hit = Physics2D.Raycast(rayOrigin.position, Vector2.right, lineLength, layerMask);

        if (hit && !gamePause)
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                GameManager.Instance.GameOver();
                gamePause = true;
                print("GAME PAUSED!!!!!");
            }
            else if(hit.collider.CompareTag("Player"))
            {
                line.transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
                float distance = ((Vector2)hit.point - (Vector2)transform.position).magnitude;
                line.SetPosition(1, Vector2.right* distance);
                Debug.DrawLine(rayOrigin.position, hit.point, Color.red);
                print(hit);
            }

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
