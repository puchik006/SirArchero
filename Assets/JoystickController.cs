using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler, IMovementDirectionHandler
{
    private const float INPUT_RANGE = 2.0f;
    private const float INPUT_MIN = 1.0f;
    private const int KNOB_MAX_DISPLACEMENT_RATIO = 3;

    private RectTransform _joystickBackground;
    private RectTransform _joystickKnob;
    private Vector2 _inputVector;

    private void Start()
    {
        _joystickBackground = GetComponent<RectTransform>();
        _joystickKnob = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground, eventData.position, eventData.pressEventCamera, out Vector2 position))
        {
            position.x /= _joystickBackground.sizeDelta.x;
            position.y /= _joystickBackground.sizeDelta.y;

            _inputVector = new Vector2(position.x * INPUT_RANGE, position.y * INPUT_RANGE);
            _inputVector = (_inputVector.magnitude > INPUT_MIN) ? _inputVector.normalized : _inputVector;

            _joystickKnob.anchoredPosition = new Vector2(
                _inputVector.x * (_joystickBackground.sizeDelta.x / KNOB_MAX_DISPLACEMENT_RATIO),
                _inputVector.y * (_joystickBackground.sizeDelta.y / KNOB_MAX_DISPLACEMENT_RATIO)
            );
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _joystickKnob.anchoredPosition = Vector2.zero;
    }

    public Vector3 GetMovementVector()
    {
        float horizontalInput = _inputVector.x;
        float verticalInput = _inputVector.y;

        return new Vector3(horizontalInput, default, verticalInput).normalized; 
    }
}
