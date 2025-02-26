using System.Collections;
using System.Reflection;
using UnityEngine;

public class Wait : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _script;

    public void OnFonction(string functionName)
    {
        StartCoroutine(OnWait(functionName));
    }

    IEnumerator OnWait(string function)
    {
        yield return new WaitForSeconds(1.5f);
        MethodInfo method = _script.GetType().GetMethod(function);

        if (method != null)
        {
            method.Invoke(_script, null);
        }
    }
}
