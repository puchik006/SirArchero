using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;

    private void LateUpdate()
    {
        if (_player != null)
        {
            float targetX = _player.position.x; 
            float targetY = transform.position.y;
            float targetZ = _player.position.z;

            Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

            transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
        }
    }
}
