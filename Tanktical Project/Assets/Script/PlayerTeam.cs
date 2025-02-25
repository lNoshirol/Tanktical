using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public List<Entity> Team = new();

    // Singleton
    #region Singleton
    [Header("Singleton")]
    [SerializeField] private bool _hasToDebugWhenDestroyed;
    [SerializeField] private bool _hasToDebugWhenCreated;

    private static PlayerTeam _instance;

    public static PlayerTeam Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("PlayerTeam");
                _instance = go.AddComponent<PlayerTeam>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            if (_hasToDebugWhenDestroyed) Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 0f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#eb624d>destroyed</color></b>");
        }
        else
        {
            _instance = this;
            if (_hasToDebugWhenCreated) Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#58ed7d>created</color></b>");
        }
    }
    #endregion
}
