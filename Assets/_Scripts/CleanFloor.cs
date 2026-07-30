using UnityEngine;

public class CleanFloor : MonoBehaviour
{
    private LayerMask _trashMask;

    private void Start()
    {
        _trashMask = 1 << LayerMask.NameToLayer("Trash");
    }

    private void OnTriggerEnter(Collider other)
    {
        if((_trashMask.value & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(other.gameObject);
        }
    }
}
