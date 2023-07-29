using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [SerializeField] private GameFieldData _data;
    [SerializeField] private JoystickController _joystickController;
    [SerializeField] private EnemyDirection _enemyDirection;
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;

    private GameFieldDrawer _drawer;
    private MovementChecker _movementChecker;

    private void Awake()
    {
        _drawer = new GameFieldDrawer(_data);
        _movementChecker = new MovementChecker(_data);
        _player.Initialise(_movementChecker, _joystickController);
        _enemy.Initialise(_movementChecker, _enemyDirection);
    }
}
