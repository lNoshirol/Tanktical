using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    public bool gamePaused;
    [SerializeField] private GameObject _panelPause;

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
            _panelPause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            gamePaused = false;
            _panelPause.SetActive(false);
        }
    }
}
