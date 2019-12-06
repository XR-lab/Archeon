using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    [SerializeField]
    private string RagdollTag;
    private List<Rigidbody> _ragdollRigidbodies;
    private List<Collider> _ragdollColliders;

    // Start is called before the first frame update
    void Start()
    {
        RagdollTag = (RagdollTag == "") ? "Untagged" : RagdollTag;

        _ragdollColliders = new List<Collider>();
        _ragdollRigidbodies = new List<Rigidbody>();

        GetRagdoll(transform);

        RagdollActiveTo(false);
    }

    public void RagdollActiveTo(bool active)
    {
        foreach (Collider collider in _ragdollColliders)
        {
            collider.enabled = active;
        }
        foreach (Rigidbody rigibody in _ragdollRigidbodies)
        {
            rigibody.isKinematic = !active;
        }
    }

    private void GetRagdoll(Transform parent)
    {
        foreach(Transform child in parent)
        {
            if (child.gameObject.tag == RagdollTag)
            {
                _ragdollColliders.Add(child.gameObject.GetComponent<Collider>());
                _ragdollRigidbodies.Add(child.gameObject.GetComponent<Rigidbody>());
                Debug.LogWarning(child.name);
            }
            GetRagdoll(child);
        }
    }
}
