using UnityEngine;

public class CleanFloor : MonoBehaviour
{
    [SerializeField]private LayerMask _trashMask;

    private void OnTriggerEnter(Collider other)
    {
        if((_trashMask.value & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(other.gameObject);
        }
    }
}
