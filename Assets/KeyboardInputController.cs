using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputController : MonoBehaviour
{
    private IDriveable driveable;
    private void Awake()
    {
        driveable= GameObject.FindGameObjectWithTag("Car").GetComponent<IDriveable>();
    }

    private void Update()
    {
        Vector2 forwardBackInput = new Vector2(0f, Input.GetAxis("Vertical"));
        driveable.HandleAccelerateInput(forwardBackInput);

        Vector2 turnInput = new Vector2(Input.GetAxis("Horizontal"), 0f);
        driveable.HandleTurnInput(turnInput);
    }
}
