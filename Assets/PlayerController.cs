using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private JoystickController _joystick;
    [SerializeField] private GameFieldData _gameFieldData;
    private float _movingBorderX;
    private float _movingBorderZ;
    //private Transform[] _obstacles;
    //private float[] _obstacleRadii;
    private MovementChecker _movementChecker;

    private void Awake()
    {
        _movingBorderX = _gameFieldData.SizeX / 2f - _gameFieldData.BorderWidth / 2f;
        _movingBorderZ = _gameFieldData.SizeZ / 2f - _gameFieldData.BorderWidth / 2f;

        //int obstacleCount = _gameFieldData.Obstacles.Count;
        //_obstacles = new Transform[obstacleCount];
        //_obstacleRadii = new float[obstacleCount];

        //for (int i = 0; i < obstacleCount; i++)
        //{
        //    _obstacles[i] = _gameFieldData.Obstacles[i].transform;
        //    _obstacleRadii[i] = _gameFieldData.Obstacles[i].GetComponent<Renderer>().bounds.extents.magnitude;
        //}
    }

    private void Update()
    {
        float horizontalInput = _joystick.GetInputVector().x;
        float verticalInput = _joystick.GetInputVector().y;
        Vector3 direction = new Vector3(horizontalInput, default, verticalInput).normalized;

        MovePlayer(direction);
    }

    public void Initialise(MovementChecker movementChecker)
    {
        _movementChecker = movementChecker;
    }

    private void MovePlayer(Vector3 direction)
    {
        Vector3 newPosition = transform.position + _speed * Time.deltaTime * direction;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        newPosition.x = Mathf.Clamp(newPosition.x, -_movingBorderX, _movingBorderX);
        newPosition.z = Mathf.Clamp(newPosition.z, -_movingBorderZ, _movingBorderZ);

        if (_movementChecker.IsInValidMovementRange(newPosition))
        {
            transform.position = newPosition;
        }
    }

    //private bool IsInValidMovementRange(Vector3 position)
    //{
    //    for (int i = 0; i < _obstacles.Length; i++)
    //    {
    //        Vector3 obstaclePosition = _obstacles[i].position;
    //        float obstacleRadius = _obstacleRadii[i];
    //        float distanceToObstacle = Vector3.Distance(position, obstaclePosition);

    //        if (distanceToObstacle <= obstacleRadius)
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}
}

public class MovementChecker
{
    private GameFieldData _gameFieldData;
    private Transform[] _obstacles;
    private float[] _obstacleRadii;

    public MovementChecker(GameFieldData gameFieldData)
    {
        _gameFieldData = gameFieldData;
        SetObstacles();
    }

    private void SetObstacles()
    {
        int obstacleCount = _gameFieldData.Obstacles.Count;
        _obstacles = new Transform[obstacleCount];
        _obstacleRadii = new float[obstacleCount];

        for (int i = 0; i < obstacleCount; i++)
        {
            _obstacles[i] = _gameFieldData.Obstacles[i].transform;
            _obstacleRadii[i] = _gameFieldData.Obstacles[i].GetComponent<Renderer>().bounds.extents.magnitude;
        }
    }

    public bool IsInValidMovementRange(Vector3 position)
    {
        for (int i = 0; i < _obstacles.Length; i++)
        {
            Vector3 obstaclePosition = _obstacles[i].position;
            float obstacleRadius = _obstacleRadii[i];
            float distanceToObstacle = Vector3.Distance(position, obstaclePosition);

            if (distanceToObstacle <= obstacleRadius)
            {
                return false;
            }
        }

        return true;
    }
}
