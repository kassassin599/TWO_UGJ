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

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        
    }

    bool gamePause = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && goRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
        }
        else if (Input.GetKey(KeyCode.A) && !goRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * -playerSpeed);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            goRight = !goRight;
        }

        hit = Physics2D.Raycast(transform.position, Vector2.right, lineLength, layerMask);

        if (hit && !gamePause)
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                GameManager.Instance.PauseGame();
                gamePause = true;
                print("GAME PAUSED!!!!!");
            }
            else if(hit.collider.CompareTag("Player"))
            {
                line.transform.position = transform.position;
                float distance = ((Vector2)hit.point - (Vector2)transform.position).magnitude;
                line.SetPosition(1, Vector2.right* distance);
                Debug.DrawLine(transform.position, hit.point, Color.red);
                print(hit);
            }

        }
    }

    public void StageOneIncrement()
    {
        gameObject.transform.localScale = new Vector2((0.3f + (0.3f * 0.25f)), (0.3f + (0.3f * 0.25f)));
        playerSpeed = playerSpeed * 0.75f;
    }
    
    public void StageTwoIncrement()
    {
        gameObject.transform.localScale = new Vector2((0.3f + (0.3f * 1.25f)), (0.3f + (0.3f * 1.25f)));
        playerSpeed = playerSpeed * 0.25f;
    }
    
    public void StageFinalIncrement()
    {
        gameObject.transform.localScale = new Vector2(0.3f, 0.3f);
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
    }
}
