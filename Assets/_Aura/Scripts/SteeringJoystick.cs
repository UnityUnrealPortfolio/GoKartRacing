using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringJoystick : JoystickInput
{

    //public event Action<Vector2> OnTurn;
   
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        driveable?.HandleTurnInput(new Vector2(offset.x, 0f));
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        driveable?.HandleTurnInput(Vector2.zero);
    }
}
