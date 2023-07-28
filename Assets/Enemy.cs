using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _movementLength = 5f;
    [SerializeField] private float _idleTime = 2f;
    [SerializeField] private int _maxHP = 100;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private int _fireDamage = 10;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;

    [SerializeField] private Transform _player;
    private float _nextMoveTime;
    private float _nextFireTime; // Add this variable
    private int _currentHP;

    private void Start()
    {
        _nextMoveTime = Time.time + _idleTime;
        _nextFireTime = 0f; // Initialize the _nextFireTime
        _currentHP = _maxHP;
    }

    private void Update()
    {
        // Check if the enemy can see the player
        bool canSeePlayer = CanSeePlayer();

        if (canSeePlayer)
        {
            // Enemy can see the player, so it will shoot at the player
            ShootAtPlayer();
        }
        else
        {
            // Enemy can't see the player, so it will move in a direction for a certain distance
            if (Time.time >= _nextMoveTime)
            {
                Move();
            }
        }
    }

    private bool CanSeePlayer()
    {
        // Check if the player is within the sight range of the enemy
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        return distanceToPlayer <= _movementLength;
    }

    private void ShootAtPlayer()
    {
        // Perform shooting logic here based on the fire rate
        if (Time.time >= _nextFireTime)
        {
            // Instantiate a projectile towards the player
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Calculate the direction towards the player
            Vector3 directionToPlayer = (_player.position - transform.position).normalized;

            // Set the fire rate for the next shot
            _nextFireTime = Time.time + 1f / _fireRate;

            // Start the coroutine to move the projectile towards the player
            StartCoroutine(MoveProjectile(projectile, directionToPlayer));
        }
    }

    private IEnumerator MoveProjectile(GameObject projectile, Vector3 direction)
    {
        // Define the projectile's speed
        float projectileSpeed = 10f;

        // Move the projectile towards the player over time
        while (Vector3.Distance(projectile.transform.position, _player.position) > 0.1f)
        {
            projectile.transform.position += direction * projectileSpeed * Time.deltaTime;
            yield return null;
        }

        // Destroy the projectile once it reaches the player or goes too far
        Destroy(projectile);
    }

    private void Move()
    {
        // Randomly select a new direction to move
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 newPosition = transform.position + new Vector3(randomDirection.x, 0f, randomDirection.y) * _movementLength;

        // Clamp the new position within the bounds of the game field or any other restrictions
        // You can add additional checks here based on your game's specific requirements

        // Set the new position and update the next move time
        transform.position = newPosition;
        _nextMoveTime = Time.time + _idleTime;
    }

    public void TakeDamage(int damage)
    {
        // Reduce HP when the enemy gets hit
        _currentHP -= damage;

        if (_currentHP <= 0)
        {
            // If HP is less than or equal to 0, destroy the enemy
            Destroy(gameObject);
        }
    }
}