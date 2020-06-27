using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoController : MonoBehaviour
{
    [SerializeField]
    float playerSpeed = 3f;

    bool goRight = true;

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
}
