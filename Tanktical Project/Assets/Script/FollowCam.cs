using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Header("Cible")]
    [SerializeField] private Transform target;

    [Header("Paramètres de position")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed;

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
