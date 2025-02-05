using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    public bool gamePaused;

    private void Start()
    {
        gamePaused = Time.timeScale == 0;
    }

    public void OnSetPause(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            PauseTrigger();
        }
    }

    public void PauseTrigger()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0;
            gamePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
    }
}
