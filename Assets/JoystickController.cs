using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private const float INPUT_RANGE = 2.0f;
    private const float INPUT_MIN = -1.0f;
    private const int KNOB_MAX_DISPLACEMENT_RATIO = 3;

    private RectTransform joystickBackground;
    private RectTransform joystickKnob;
    private Vector2 inputVector;

    private void Start()
    {
        joystickBackground = GetComponent<RectTransform>();
        joystickKnob = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / joystickBackground.sizeDelta.x);
            position.y = (position.y / joystickBackground.sizeDelta.y);

            inputVector = new Vector2(position.x * INPUT_RANGE, position.y * INPUT_RANGE);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickKnob.anchoredPosition = new Vector2(
                inputVector.x * (joystickBackground.sizeDelta.x / KNOB_MAX_DISPLACEMENT_RATIO),
                inputVector.y * (joystickBackground.sizeDelta.y / KNOB_MAX_DISPLACEMENT_RATIO)
            );
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickKnob.anchoredPosition = Vector2.zero;
    }

    public Vector2 GetInputVector()
    {
        return inputVector;
    }
}
