using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string _sceneName;

    public void SwitchScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_sceneName);
    }
}
