using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string _sceneName;

    public void SwitchScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
