using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [SerializeField] private GameFieldData _data;
    [SerializeField] private PlayerController _playerController;

    private GameFieldDrawer _drawer;
    private MovementChecker _movementChecker;

    private void Awake()
    {
        _drawer = new GameFieldDrawer(_data); ;
        _movementChecker = new MovementChecker(_data);
        _playerController.Initialise(_movementChecker);
    }
}
