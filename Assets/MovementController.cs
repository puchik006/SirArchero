using UnityEngine;

public class MovementController
{
    private float _speed;
    private IMovementDirectionHandler _movementDirectionHandler;
    private MovementChecker _movementChecker;
    private bool _isPlayer;

    public MovementController(MovementChecker movementChecker, IMovementDirectionHandler movementDirectionHandler, bool isPlayer, float speed)
    {
        _movementChecker = movementChecker;
        _movementDirectionHandler = movementDirectionHandler;
        _isPlayer = isPlayer;
        _speed = speed;
    }

    public void Move(Transform transform)
    {
        var direction = _movementDirectionHandler.GetMovementVector();
        Vector3 newPosition = transform.position + _speed * Time.deltaTime * direction;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(default, angle, default);

        if (_isPlayer)
        {
            if (_movementChecker.IsInValidMovementRange(newPosition))
            {
                transform.position = newPosition;
            }
        }
        else
        {
            if (_movementChecker.IsInValidMovementRange(newPosition))
            {
                transform.position = newPosition;
            }
            else
            {
                Vector3 adjustedDirection = GetAdjustedDirection(direction, transform);
                Vector3 adjustedPosition = transform.position + _speed * Time.deltaTime * adjustedDirection;

                if (_movementChecker.IsInValidMovementRange(adjustedPosition))
                {
                    transform.position = adjustedPosition;
                }
            }
        }
    }

    private Vector3 GetAdjustedDirection(Vector3 originalDirection, Transform transform)
    {
        Vector3 adjustedDirection = originalDirection;
        float angleStep = 15f;

        for (int i = 0; i < 24; i++) 
        {
            adjustedDirection = Quaternion.Euler(default, angleStep, default) * adjustedDirection;

            if (_movementChecker.IsInValidMovementRange(transform.position + _speed * Time.deltaTime * adjustedDirection))
            {
                return adjustedDirection;
            }
        }
        return originalDirection;
    }
}
