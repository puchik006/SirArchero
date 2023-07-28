using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;
    [SerializeField] private GameFieldData _gameField;

    private Vector3 _cameraPosition;

    private void Awake()
    {
        _cameraPosition = transform.position;
        Camera.main.orthographicSize = _gameField.SizeX;
    }

    private void LateUpdate()
    {
        if (_player != null)
        {
            float targetX = default;
            float targetY = transform.position.y;
            float targetZ = _player.position.z + _cameraPosition.z;

            Vector3 targetPosition = new(targetX, targetY, targetZ);
            transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
        }
    }
}
