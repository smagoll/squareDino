using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody[] _ragdollRigidbodies;
    [SerializeField]
    private Collider[] _ragdollColliders;

    public void DisableRagdoll()
    {
        _animator.enabled = true;
        
        foreach (var rb in _ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
    }
    
    public void EnableRagdoll()
    {
        _animator.enabled = false;

        foreach (var rb in _ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
    }
}