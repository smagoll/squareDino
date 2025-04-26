using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, IMovable
{
    public event Action OnStop;
    
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    
    private bool _isMoving = false;

    public void MoveTo(Vector3 targetPosition)
    {
        _navMeshAgent.destination = targetPosition;
        _isMoving = true;
    }

    private void Update()
    {
        if (_isMoving && !_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _isMoving = false;
                
                OnStop?.Invoke();
            }
        }
    }
}
