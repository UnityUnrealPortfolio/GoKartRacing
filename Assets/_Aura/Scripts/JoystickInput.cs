using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickInput : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] protected RectTransform joystickTransform;
    [SerializeField] protected float dragOffsetDistance = 30;
    [SerializeField] protected int dragOffsetScale = 100;
  
    protected Vector2 offset;
    protected IDriveable driveable;

    protected virtual void Awake()
    {
        driveable = GameObject.FindGameObjectWithTag("Car").GetComponent<IDriveable>();
        joystickTransform = (RectTransform)transform;
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickTransform,
            eventData.position,
            null,
            out offset
            );
        offset = Vector2.ClampMagnitude(offset, dragOffsetScale) / dragOffsetScale;
        joystickTransform.anchoredPosition = offset * dragOffsetDistance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        //return joystick to starting center position
        joystickTransform.anchoredPosition = Vector2.zero;
      
    }
}
