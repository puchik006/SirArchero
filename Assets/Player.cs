using UnityEngine;

public class Player: MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _HP;

    private MovementController _movementController;

    public void Initialise(MovementChecker movementChecker, IMovementDirectionHandler directionHandler)
    {
        _movementController = new MovementController(movementChecker, directionHandler, true, _speed);
    }

    private void Update()
    {
        if (_movementController != null)
        {
            _movementController.Move(transform);
        }
    }
}
