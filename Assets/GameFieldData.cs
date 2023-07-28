using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameFieldData", menuName = "GameFieldData")]
public class GameFieldData : ScriptableObject
{
    [Header("Gamefield dimensions")]
    [SerializeField] private float _sizeX;
    [SerializeField] private float _sizeZ;
    [SerializeField] private float _borderHeight;
    [SerializeField] private float _borderWidth;
    [Header("Gate dimensions")]
    [SerializeField] private float _gateWidth;
    [SerializeField] private float _gateLenght;
    [SerializeField] private float _gateHeight;
    [Header("Gamefield colors")]
    [SerializeField] private Color _borderColor = Color.cyan;
    [SerializeField] private Color _floorColor = Color.white;
    [SerializeField] private Color _gateColor = Color.black;

    [SerializeField] private List<Transform> _obstacles;
    public List<Transform> Obstacles => _obstacles;

    public float SizeX => _sizeX;
    public float SizeZ => _sizeZ;
    public float BorderHeight => _borderHeight;
    public float BorderWidth => _borderWidth;
    public float GateWidth => _gateWidth;
    public float GateLength => _gateLenght;
    public float GateHeight => _gateHeight;
    public Color BorderColor => _borderColor;
    public Color FloorColor => _floorColor;
    public Color GateColor => _gateColor;

    public Vector3 HorisontalBorderPosition => new(SizeX/2, default, default);
    public Vector3 HorisontalBorderScale => new(_borderWidth, _borderHeight, SizeZ);
    public Vector3 VerticalBorderPositon => new(default, default, -SizeZ/2);
    public Vector3 VerticalBorderScale => new(SizeX, _borderHeight, _borderWidth);
    public Vector3 GatePosition => new(default, default, SizeZ/2);
    public Vector3 GateScale => new(_gateWidth, _gateHeight, _gateLenght);
    public Vector3 FloorScale => new(_sizeX, 1, _sizeZ);

    
}
