using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AcceleratorJoystick : JoystickInput
{
    public event Action<Vector2> OnAccelerate;

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        OnAccelerate?.Invoke(new Vector2(0f,offset.y));
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        OnAccelerate?.Invoke(Vector2.zero);
    }
}
