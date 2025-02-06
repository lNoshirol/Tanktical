using System.Collections;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    [SerializeField] private string _text;
    [SerializeField] private float _time;
    private bool _isActive = true;

    public void OnTypingText()
    {
        if (!_isActive) return;

        TextInteract.Instance.ShowText(_text, _time);

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        _isActive = false;
        yield return new WaitForSeconds(_time);
        _isActive = true;
    }
}
