    using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField] InputActionAsset _inputActions;

    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    [SerializeField] float swipeThreshold = 50f;

    private bool _swipeLeft, _swipeRight, _swipeUp, _swipeDown;
    public bool SwipeLeft => _swipeLeft;
    public bool SwipeDown => _swipeDown;
    public bool SwipeUp => _swipeUp;
    public bool SwipeRight => _swipeRight;

    private void OnEnable()
    {
        var touchMap = _inputActions.FindActionMap("Touch");

        _touchPositionAction = touchMap.FindAction("Primary Touch");
        _touchPressAction = touchMap.FindAction("Touch Press");

        _touchPressAction.started += OnTouchStarted;
        _touchPressAction.canceled += OnTouchEnded;

        touchMap.Enable();
    }

    private void OnDisable()
    {
        _touchPressAction.started -= OnTouchStarted;
        _touchPressAction.canceled -= OnTouchEnded;
    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
        _startTouchPosition = _touchPositionAction.ReadValue<Vector2>();
    }

    private void OnTouchEnded(InputAction.CallbackContext context)
    {
        _endTouchPosition = _touchPositionAction.ReadValue<Vector2>();
        DetectSwipe();
    }

    public void ResetFlags()
    {
        _swipeDown = false;
        _swipeLeft = false;
        _swipeRight = false;
        _swipeUp = false;
    }
    private void DetectSwipe()
    {
        Vector2 swipeDelta = _endTouchPosition - _startTouchPosition;
        if (swipeDelta.magnitude < swipeThreshold)
            return;

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            if (swipeDelta.x > 0)
            {
                Debug.Log("Swipe Right");
                _swipeRight = true;
            }
            else
            {
                Debug.Log("Swipe Left");
                _swipeLeft = true;
            }
        }
        else
        {
            if (swipeDelta.y > 0)
            {
                Debug.Log("Swipe Up");
                _swipeUp = true;
            }

            else
            {
                Debug.Log("Swipe Down");
                _swipeDown = true;
            }

        }
    }
}
