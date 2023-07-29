using UnityEngine;

public class EnemyDirection :MonoBehaviour, IMovementDirectionHandler
{
    [SerializeField] private Transform _player;

    public Vector2 GetMovementVector()
    {
        Vector3 directionToPlayer = _player.position - transform.position;
        return new Vector2(directionToPlayer.x, directionToPlayer.z).normalized;
    }
}