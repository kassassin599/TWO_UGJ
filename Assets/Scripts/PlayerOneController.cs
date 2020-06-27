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

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.right * Time.deltaTime * -playerSpeed);
        }

        hit = Physics2D.Raycast(transform.position, Vector2.right, lineLength, layerMask);

        if (hit)
        {
            line.transform.position = transform.position;
            float distance = ((Vector2)hit.point - (Vector2)transform.position).magnitude;
            line.SetPosition(1, Vector2.right* distance);
            Debug.DrawLine(transform.position, hit.point, Color.red);
            print(hit);
        }
    }
}
