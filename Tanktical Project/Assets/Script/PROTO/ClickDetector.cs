using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickDetector : MonoBehaviour
{
    public static ClickDetector Instance;

    public Vector3 Pos;
    public GameObject LastEnityHit;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Pos = hit.point;

            GameObject objectHit = hit.collider.gameObject;

            if (objectHit.CompareTag("ally") || objectHit.CompareTag("ennemy"))
            {
                LastEnityHit = objectHit;
            }
        }
    }

    public void OnClickToAttack(InputAction.CallbackContext callbackContext)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && callbackContext.started)
        {
            GameObject objectHit = hit.collider.gameObject;

            if (objectHit.CompareTag("ally") || objectHit.CompareTag("ennemy"))
            {
                SkillSelectorManager.Instance.UseAttack(objectHit);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Pos, 0.5f);
    }
}
