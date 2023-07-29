using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] private float _speed;
    private MovementController _movementController;

    public void Initialise(MovementChecker movementChecker, IMovementDirectionHandler movementDirection)
    {
        _movementController = new MovementController(movementChecker, movementDirection, false, _speed);
    }

    private void Update()
    {
        if (_movementController != null)
        {
            _movementController.Move(transform);
        }
    }
}
