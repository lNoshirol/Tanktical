using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [SerializeField] private ObjectInteract _objectInteract;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && _objectInteract != null)
        {
            _objectInteract.OnTypingText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObjectInteract")
        {
            _objectInteract = other.gameObject.GetComponent<ObjectInteract>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ObjectInteract")
        {
            _objectInteract = null;
        }
    }
}
