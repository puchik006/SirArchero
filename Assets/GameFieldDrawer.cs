using UnityEngine;

public class GameFieldDrawer : MonoBehaviour
{
    [SerializeField] private GameFieldData _data;

    private void Start()
    {
        CreateGameField();
    }

    private void CreateGameField()
    {
        CreateFigure("RightBorder",PrimitiveType.Cube, _data.HorisontalBorderPosition, _data.HorisontalBorderScale, _data.BorderColor);
        CreateFigure("LeftBorder",PrimitiveType.Cube, -_data.HorisontalBorderPosition, _data.HorisontalBorderScale, _data.BorderColor);
        CreateFigure("TopBorder",PrimitiveType.Cube, _data.VerticalBorderPositon, _data.VerticalBorderScale, _data.BorderColor);
        CreateFigure("BottomBorder",PrimitiveType.Cube, -_data.VerticalBorderPositon, _data.VerticalBorderScale, _data.BorderColor);
        CreateFigure("Gate",PrimitiveType.Cube, _data.GatePosition,_data.GateScale, _data.GateColor);
        CreateFigure("Floor",PrimitiveType.Plane,Vector3.zero,_data.FloorScale, _data.FloorColor);
    }

    private void CreateFigure(string name,PrimitiveType type, Vector3 position, Vector3 scale, Color color)
    {
        GameObject figure = GameObject.CreatePrimitive(type);
        figure.name = name;
        figure.transform.position = position;
        figure.transform.localScale = scale;
        figure.GetComponent<Renderer>().material.color = color;
    }
}



