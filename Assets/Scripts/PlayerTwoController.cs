using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoController : MonoBehaviour
{
    [SerializeField]
    float playerSpeed = 3f;

    bool goRight = true;

    public GameManager.Stage stage;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && goRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !goRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * -playerSpeed);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            goRight = !goRight;
        }
    }

    public void StageOneIncrement()
    {
        gameObject.transform.localScale = new Vector2((0.3f + (0.3f * 0.25f)), (0.3f + (0.3f * 0.25f)));
    }

    public void StageTwoIncrement()
    {
        gameObject.transform.localScale = new Vector2((0.3f + (0.3f * 1.25f)), (0.3f + (0.3f * 1.25f)));
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
