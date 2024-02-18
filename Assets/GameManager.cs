using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject joystickPanel;
    [SerializeField] GameObject keyboardInputControls;
    // Start is called before the first frame update
    void Awake()
    {
        //ToDo:this code only here to allow for mobile testing
#if UNITY_ANDROID
        joystickPanel.SetActive(true);
        keyboardInputControls.GetComponent<KeyboardInputController>().enabled = false;
#endif

#if UNITY_EDITOR_WIN
        joystickPanel.SetActive(false);
        keyboardInputControls.GetComponent<KeyboardInputController>().enabled = true;
#endif

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
