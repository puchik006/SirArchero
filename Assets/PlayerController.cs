using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private JoystickController _joystick;
    [SerializeField] private GameFieldData _gameFieldData;
    private float _movingBorderX;
    private float _movingBorderZ;

    private void Awake()
    {
        _movingBorderX = _gameFieldData.SizeX / 2f - _gameFieldData.BorderWidth / 2f;
        _movingBorderZ = _gameFieldData.SizeZ / 2f - _gameFieldData.BorderWidth / 2f;
    }

    private void Update()
    {
        float horizontalInput = _joystick.GetInputVector().x;
        float verticalInput = _joystick.GetInputVector().y;
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        MovePlayer(direction);
    }

    private void MovePlayer(Vector3 direction)
    {
        Vector3 newPosition = transform.position + _speed * Time.deltaTime * direction;
        newPosition.x = Mathf.Clamp(newPosition.x, -_movingBorderX, _movingBorderX);
        newPosition.z = Mathf.Clamp(newPosition.z, -_movingBorderZ, _movingBorderZ);

        if (IsInValidMovementRange(newPosition))
        {
            transform.position = newPosition;
        }
    }

    private bool IsInValidMovementRange(Vector3 position)
    {
        foreach (Transform obstacle in _gameFieldData.Obstacles)
        {
            Vector3 obstaclePosition = obstacle.transform.position;
            float obstacleRadius = obstacle.GetComponent<Renderer>().bounds.extents.magnitude;
            float distanceToObstacle = Vector3.Distance(position, obstaclePosition);

            if (distanceToObstacle <= obstacleRadius)
            {
                return false;
            }
        }

        return true;
    }
}
