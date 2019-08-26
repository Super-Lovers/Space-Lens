using UnityEngine;

public class ZoomedViewController : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField]
    private float _movementSpeed = 0.01f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.A))
        {
            Move(new Vector3(-_movementSpeed, 0, 0));
        } else if (Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.D))
        {
            Move(new Vector3(_movementSpeed, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) ||
          Input.GetKeyDown(KeyCode.W))
        {
            Move(new Vector3(0, _movementSpeed, 0));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
          Input.GetKeyDown(KeyCode.S))
        {
            Move(new Vector3(0, -_movementSpeed, 0));
        }
    }

    private void Move(Vector3 valuesToUpdateToPosition)
    {
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(
            currentPosition.x + valuesToUpdateToPosition.x,
            currentPosition.y + valuesToUpdateToPosition.y,
            currentPosition.z
            );
    }
}
