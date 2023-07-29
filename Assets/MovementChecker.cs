using System.Collections.Generic;
using UnityEngine;

public class MovementChecker
{
    private GameFieldData _gameFieldData;
    private List<Transform> _obstacles;
    private List<float> _obstacleRadii;
    private float _movingBorderX;
    private float _movingBorderZ;

    public MovementChecker(GameFieldData gameFieldData)
    {
        _gameFieldData = gameFieldData;
        SetObstacles();
        SetBorderAndGateData();
    }

    private void SetObstacles()
    {
        int obstacleCount = _gameFieldData.Obstacles.Count;
        _obstacles = new List<Transform>();
        _obstacleRadii = new List<float>();

        for (int i = 0; i < obstacleCount; i++)
        {
            _obstacles.Add(_gameFieldData.Obstacles[i].transform);
            _obstacleRadii.Add(_gameFieldData.Obstacles[i].GetComponent<Renderer>().bounds.extents.magnitude);
        }
    }

    private void SetBorderAndGateData()
    {
        _movingBorderX = _gameFieldData.SizeX / 2f - _gameFieldData.BorderWidth / 2f;
        _movingBorderZ = _gameFieldData.SizeZ / 2f - _gameFieldData.BorderWidth / 2f;
    }

    public bool IsInValidMovementRange(Vector3 position)
    {
        for (int i = 0; i < _obstacles.Count; i++)
        {
            Vector3 obstaclePosition = _obstacles[i].position;
            float obstacleRadius = _obstacleRadii[i];
            float distanceToObstacle = Vector3.Distance(position, obstaclePosition);

            if (distanceToObstacle <= obstacleRadius)
            {
                return false;
            }
        }

        if (position.x > _movingBorderX || position.x < -_movingBorderX)
        {
            return false;
        }

        if (position.z > _movingBorderZ || position.z < -_movingBorderZ)
        {
            return false;
        }

        return true;
    }
}
