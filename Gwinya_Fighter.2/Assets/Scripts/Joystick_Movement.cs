using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick_Movement : MonoBehaviour
{
    [SerializeField] private GameObject joyStick;
    [SerializeField] private GameObject joysStickBG;
    public Vector2 joystickVector;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    void Start()
    {
        joystickOriginalPos = joysStickBG.transform.position;
        joystickRadius = joysStickBG.GetComponent<RectTransform>().sizeDelta.y / 2;
    }

    public void PointerDown()
    {
        joyStick.transform.position = Input.mousePosition;
        joysStickBG.transform.position = Input.mousePosition;

        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVector = (dragPos - joystickTouchPos).normalized;

        float joystickDistance = Vector2.Distance(dragPos, joystickTouchPos);

        if(joystickDistance < joystickRadius)
        {
            joyStick.transform.position = joystickTouchPos + joystickVector * joystickDistance;
        }

        else
        {
            joyStick.transform.position = joystickTouchPos + joystickVector * joystickRadius;
        }
    }

    public void PointerUp()
    {
        joystickVector = Vector2.zero;
        joyStick.transform.position = joystickOriginalPos;
        joysStickBG.transform.position = joystickOriginalPos;
    }
}
