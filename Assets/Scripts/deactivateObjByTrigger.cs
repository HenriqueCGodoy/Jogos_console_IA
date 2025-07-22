using UnityEngine;

[RequireComponent(typeof(Collider))]
public class deactivateObjByTrigger : MonoBehaviour
{
    [SerializeField] private GameObject activatorObject;
    [SerializeField] private GameObject targetObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == activatorObject)
        {
            targetObject.SetActive(false);
        }
    }
}
