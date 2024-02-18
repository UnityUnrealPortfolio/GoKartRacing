using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDriveable
{
    public void HandleTurnInput(Vector2 _turnInput);
    public void HandleAccelerateInput(Vector2 _accelInput);
}
