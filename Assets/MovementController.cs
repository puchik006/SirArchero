using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private IMovementDirectionHandler _movementDirectionHandler;
    private MovementChecker _movementChecker;

    private void Update()
    {
        float horizontalInput = _movementDirectionHandler.GetMovementVector().x;
        float verticalInput = _movementDirectionHandler.GetMovementVector().y;
        Vector3 direction = new Vector3(horizontalInput, default, verticalInput).normalized;

        Move(direction);
    }

    public void Initialise(MovementChecker movementChecker, IMovementDirectionHandler movementDirectionHandler)
    {
        _movementChecker = movementChecker;
        _movementDirectionHandler = movementDirectionHandler;
    }

    private void Move(Vector3 direction)
    {
        Vector3 newPosition = transform.position + _speed * Time.deltaTime * direction;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (_movementChecker.IsInValidMovementRange(newPosition))
        {
            transform.position = newPosition;
        }
    }
}
