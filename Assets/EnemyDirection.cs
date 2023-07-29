using UnityEngine;

public class EnemyDirection :MonoBehaviour, IMovementDirectionHandler
{
    [SerializeField] private Transform _player;

    public Vector3 GetMovementVector()
    {
        Vector3 directionToPlayer = _player.position - transform.position;
        return new Vector3(directionToPlayer.x,default, directionToPlayer.z).normalized;
    }
}