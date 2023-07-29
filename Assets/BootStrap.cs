using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [SerializeField] private GameFieldData _data;

    [SerializeField] private MovementController _playerController;
    [SerializeField] private MovementController _enemyController;
    [SerializeField] private JoystickController _joystickController;
    [SerializeField] private EnemyDirection _enemyDirection;

    private GameFieldDrawer _drawer;
    private MovementChecker _movementChecker;

    private void Awake()
    {
        _drawer = new GameFieldDrawer(_data); ;
        _movementChecker = new MovementChecker(_data);

        _playerController.Initialise(_movementChecker, _joystickController);
        _enemyController.Initialise(_movementChecker, _enemyDirection);
    }
}
