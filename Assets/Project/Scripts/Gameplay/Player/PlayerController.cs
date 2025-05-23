using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, IMovable
{
    public event Action OnStop;
    
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private RagdollController _ragdollController;
    
    private bool _isMoving;
    private static readonly int Move = Animator.StringToHash("move");

    private void Awake()
    {
        _ragdollController.DisableRagdoll();
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(targetPosition);
        _isMoving = true;
        _animator.SetBool(Move, true);
    }

    private void Update()
    {
        if (_isMoving && !_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _isMoving = false;
                
                _navMeshAgent.isStopped = true;
                _navMeshAgent.ResetPath();
                _animator.SetBool(Move, false);
                
                OnStop?.Invoke();
            }
        }
    }
    
    public void RotateTowards(Vector3 targetPosition, float duration = 0.5f)
    {
        StopAllCoroutines();
        StartCoroutine(RotateCoroutine(targetPosition, duration));
    }
    
    private IEnumerator RotateCoroutine(Vector3 targetPosition, float duration)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f;

        if (direction == Vector3.zero)
            yield break;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
